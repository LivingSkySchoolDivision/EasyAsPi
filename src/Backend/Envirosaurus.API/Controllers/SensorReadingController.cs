using Envirosaurus.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Envirosaurus.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SensorReadingController : ControllerBase
{
    private readonly ILogger<SensorReadingController> _logger;
    private readonly SensorReadingService _readingRepo; 
    private readonly SensorService _sensorRepo;

    // Require that a sensor reading has at least this many fields with data, or reject it.
    private const int _requiredDataPointsBeforeRejection = 1;

    public SensorReadingController(ILogger<SensorReadingController> logger, SensorReadingService sensorReadingService, SensorService sensorService)
    {
        _logger = logger;
        _readingRepo = sensorReadingService;
        _sensorRepo = sensorService;
    }

    [HttpGet("{guid}")]
    public IActionResult Get(Guid guid)
    {
        // Check to see if this sensor exists
        Sensor sensor = _sensorRepo.Get(guid);

        if (sensor != null) 
        {
            return Ok(_readingRepo.Find(x => x.DeviceSerialNumber == sensor.DeviceSerialNumber, 6, x => x.TimestampUTC, true));
        }
        
        return NotFound();        
    }

    [HttpPost]
    public IActionResult Post(SensorReading Reading)
    {
        if (!string.IsNullOrEmpty(Reading.DeviceSerialNumber))
        {
            // Adjust some reading values
            Reading.TimestampUTC = DateTime.UtcNow;

            // Record the reading
            if (validateIncomingReading(Reading))
            {
                _readingRepo.Insert(Reading);

                // Try to find a corresponding sensor entry and update it
                // It's ok if none exists yet
                List<Sensor> detectedSensors = _sensorRepo.Find(x => x.DeviceSerialNumber == Reading.DeviceSerialNumber).ToList();
                foreach(Sensor sensor in detectedSensors) {                    
                    sensor.LastSensorReadingUTC = DateTime.UtcNow;
                    _sensorRepo.Update(sensor);
                }

                return Ok(new SensorReadingResponse());
            }
        }

        return BadRequest();
    }
    
    [HttpGet]
    public IEnumerable<SensorReading> Get()
    {
        List<SensorReading> response = new List<SensorReading>();

        // TODO: This loop is expensive as far as database calls, but because of how we've compartmentalized
        //       the mongo stuff, we can't directly run mongo aggregations here to make this quicker.
        //       It does work, though, so if this becomes an issue, this note might help find the bottleneck.
        foreach(Sensor sensor in _sensorRepo.GetAll())
        {
            SensorReading lastReading = _readingRepo.GetLastReadingForSensor(sensor.DeviceSerialNumber);
            if (lastReading != null) {
                if (lastReading.Id != new Guid()) {
                    response.Add(lastReading);
                }
            }
        }

        return response;
    }

    private bool validateIncomingReading(SensorReading Reading) 
    {
        int fieldsWithData = 0;

        if (Reading.TemperatureCelsius != null) { fieldsWithData++; }
        if (Reading.HumidityPercent != null) { fieldsWithData++; }
        if (Reading.Pressure != null) { fieldsWithData++; }
        if (Reading.Lux != null) { fieldsWithData++; }
        if (Reading.Noise != null) { fieldsWithData++; }
        if (Reading.OxidisingGasLevel != null) { fieldsWithData++; }
        if (Reading.ReducingGasLevel != null) { fieldsWithData++; }
        if (Reading.NH3Level != null) { fieldsWithData++; }
        
        return fieldsWithData >= _requiredDataPointsBeforeRejection;
    }
}

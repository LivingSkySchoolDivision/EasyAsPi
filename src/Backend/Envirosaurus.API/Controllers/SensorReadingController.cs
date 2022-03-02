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
    private const int _requiredDataPointsBeforeRejection = 3;

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
            return Ok(_readingRepo.GetForSensor(sensor));
        }
        
        return NotFound();        
    }

    [HttpPost]
    public IActionResult Post(SensorReading Reading)
    {
        if(Reading.DeviceSerialNumber.ToLower().Trim() == "test")
        {
            return Ok(new SensorReadingResponse() { AssignedNumber = 0});

        } else if (!string.IsNullOrEmpty(Reading.DeviceSerialNumber))
        {
            // Adjust some reading values
            Reading.TimestampUTC = DateTime.UtcNow;

            // Record the reading
            if (validateIncomingReading(Reading))
            {
                _readingRepo.Insert(Reading);
            } else 
            {
                return BadRequest();
            }
                

            // Check to see if this sensor exists already.
            // If it doesn't, create a new one
            List<Sensor> detectedSensors = _sensorRepo.Find(x => x.DeviceSerialNumber == Reading.DeviceSerialNumber).ToList();
            if (detectedSensors.Count > 0)
            {
                foreach(Sensor sensor in detectedSensors) {
                    return Ok(new SensorReadingResponse() { AssignedNumber = sensor.AssignedNumber });
                }

            } else {
                int newSensorID = _sensorRepo.GetNextID();

                Sensor newSensor = new Sensor() 
                {
                    DeviceSerialNumber = Reading.DeviceSerialNumber,
                    
                    AssignedNumber = newSensorID,
                    DeviceModel = Reading.DeviceModel ?? string.Empty,
                    DeviceFriendlyName = $"{newSensorID.ToString("D3")}-{Reading.DeviceSerialNumber}",
                    DeviceDescription = ""                    
                };
                                
                _sensorRepo.Insert(newSensor);

                return Ok(new SensorReadingResponse() { AssignedNumber = newSensor.AssignedNumber });
            }

            return BadRequest();
        }

        return BadRequest();
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

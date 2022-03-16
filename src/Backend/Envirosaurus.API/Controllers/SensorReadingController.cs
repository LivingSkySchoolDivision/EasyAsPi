using Envirosaurus.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Envirosaurus.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SensorReadingController : ControllerBase
{
    private readonly ILogger<SensorReadingController> _logger;
    private readonly SensorReadingService _readingRepo; private readonly SensorService _sensorRepo;

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
            return Ok(_readingRepo.GetForSensor(sensor).OrderByDescending(x => x.TimestampUTC).Take(50));
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
                return Ok(new SensorReadingResponse());
            }
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

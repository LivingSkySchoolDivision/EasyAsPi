using Envirosaurus.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Envirosaurus.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SensorsController : ControllerBase
{
    private readonly ILogger<SensorsController> _logger;
    private readonly SensorService _repo;

    public SensorsController(ILogger<SensorsController> logger, SensorService sensorService)
    {
        _logger = logger;
        _repo = sensorService;
    }

    [HttpGet]
    public IEnumerable<Sensor> Get()
    {
        return _repo.GetAll();
    }

    [HttpGet("{guid}")]
    public IActionResult Get(Guid guid)
    {
        Sensor detectedSensor = _repo.Get(guid);
        if (detectedSensor != null) {
            return Ok(detectedSensor);
        } else {
            return NotFound();
        }
    }

    [HttpGet("BySerial/{serial}")]
    public IActionResult Get(string serial)
    {
        Sensor? detectedSensor = _repo.Find(x => x.DeviceSerialNumber == serial).FirstOrDefault();
        if (detectedSensor != null) {
            return Ok(detectedSensor);
        } else {
            return NotFound();
        }
    }

    [HttpGet("ByNumber/{number}")]
    public IActionResult Get(int number)
    {
        Sensor? detectedSensor = _repo.Find(x => x.AssignedNumber == number).FirstOrDefault();
        if (detectedSensor != null) {
            return Ok(detectedSensor);
        } else {
            return NotFound();
        }
    }
    
}

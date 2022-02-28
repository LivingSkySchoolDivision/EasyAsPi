using Microsoft.AspNetCore.Mvc;

namespace Envirosaurus.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SensorReadingController : ControllerBase
{
    private readonly ILogger<SensorReadingController> _logger;

    public SensorReadingController(ILogger<SensorReadingController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Sensor> Get()
    {
        return new List<Sensor>();
    }
    
    [HttpPost]
    public IActionResult Post(SensorReading Reading)
    {
        if (!string.IsNullOrEmpty(Reading.DeviceSerialNumber))
        {
            SensorReadingResponse response = new SensorReadingResponse()
            {
                AssignedNumber = 123
            };

            // Record the reading

            // Check to see if this sensor exists already.
            // If it doesn't, create a new one

            return Ok(response);
        }        

        return BadRequest();        
    }
}

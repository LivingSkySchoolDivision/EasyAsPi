using Microsoft.AspNetCore.Mvc;

namespace Envirosaurus.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SensorsController : ControllerBase
{
    private readonly ILogger<SensorsController> _logger;

    public SensorsController(ILogger<SensorsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Sensor> Get()
    {
        return new List<Sensor>();
    }

    
}

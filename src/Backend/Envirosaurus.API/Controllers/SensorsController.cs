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

    
}

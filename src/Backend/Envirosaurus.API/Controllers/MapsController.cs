using Envirosaurus.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Envirosaurus.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MapsController : ControllerBase
{
    private readonly ILogger<MapsController> _logger;
    private readonly FacilityMapService _repo;

    public MapsController(ILogger<MapsController> logger, FacilityMapService service)
    {
        _logger = logger;
        _repo = service;
    }

    [HttpGet]
    public IEnumerable<FacilityMap> Get()
    {
        return _repo.GetAll();
    }

    [HttpGet("{guid}")]
    public IActionResult Get(Guid guid)
    {
        FacilityMap detectedSensor = _repo.Get(guid);
        if (detectedSensor != null) {
            return Ok(detectedSensor);
        } else {
            return NotFound();
        }
    }
    
}

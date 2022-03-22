using Envirosaurus.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Envirosaurus.API.Controllers;

[ApiController]
[Route("[controller]")]
public class HeartbeatController : ControllerBase
{
    private readonly ILogger<HeartbeatController> _logger;
    private readonly SensorService _sensorRepo;
    private const int _minSerialNumberLength = 8;

    // Require that a sensor reading has at least this many fields with data, or reject it.
    private const int _requiredDataPointsBeforeRejection = 3;

    public HeartbeatController(ILogger<HeartbeatController> logger, SensorService sensorService)
    {
        _logger = logger;
        _sensorRepo = sensorService;
    }

    
    [HttpPost]
    public IActionResult Post(SensorHeartbeat Heartbeat)
    {
        if (!string.IsNullOrEmpty(Heartbeat.DeviceSerialNumber))
        {
            if (Heartbeat.DeviceSerialNumber.Length >= _minSerialNumberLength) {
                // Adjust some reading values
                Heartbeat.TimestampUTC = DateTime.UtcNow;

                // Create the response
                SensorHeartbeatResponse response = new SensorHeartbeatResponse();

                // Find the current version number to return to the client
                // We don't have a good way to do this right now, so just return a standard string
                response.VersionNumber = "EASYASPI002";


                // Check to see if this sensor exists already.
                // If it doesn't, create a new one
                List<Sensor> detectedSensors = _sensorRepo.Find(x => x.DeviceSerialNumber == Heartbeat.DeviceSerialNumber).ToList();
                if (detectedSensors.Count > 0)
                {
                    foreach(Sensor sensor in detectedSensors) {
                        response.AssignedNumber = sensor.AssignedNumber;                    
                    }

                    return Ok(response);
                } else {
                    int newSensorID = _sensorRepo.GetNextID();

                    Sensor newSensor = new Sensor() 
                    {
                        DeviceSerialNumber = Heartbeat.DeviceSerialNumber,
                        
                        AssignedNumber = newSensorID,
                        DeviceModel = Heartbeat.DeviceModel ?? string.Empty,
                        DeviceFriendlyName = $"{newSensorID.ToString("D3")}-{Heartbeat.DeviceSerialNumber}",
                        DeviceDescription = ""                    
                    };
                                    
                    _sensorRepo.Insert(newSensor);

                    response.AssignedNumber = newSensorID;

                }
                return Ok(response);
            }
        }

        return BadRequest();
    }
}

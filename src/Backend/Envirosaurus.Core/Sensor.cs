namespace Envirosaurus;

public class Sensor : IGUIDable
{
    public Guid Id { get; set; }
    public string DeviceModel { get; set; }
    public string DeviceSerialNumber { get; set; } = string.Empty;
    public int AssignedNumber { get; set; }     
    public string DeviceFriendlyName { get; set; } = string.Empty;
    public string DeviceDescription { get; set; } = string.Empty;
    public DateTime? LastHeartbeatUTC { get; set; }
    public DateTime? LastSensorReadingUTC { get; set; }
    public string? LastSeenClientVersion { get; set; }
    
}
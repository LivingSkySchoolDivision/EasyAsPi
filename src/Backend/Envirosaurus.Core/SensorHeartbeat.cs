namespace Envirosaurus;

public class SensorHeartbeat
{
    public string DeviceSerialNumber { get; set; }
    public string DeviceModel { get; set; }
    public DateTime TimestampUTC { get; set; }
    public string? DeviceVersion { get; set; }
}
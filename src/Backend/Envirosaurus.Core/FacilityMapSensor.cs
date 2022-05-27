namespace Envirosaurus;

public class FacilityMapSensor : IGUIDable
{
    public Guid Id { get; set; }
    public Guid FacilityMapID { get; set; }
    public Guid SensorGuid { get; set; }
    public int XPos { get; set; }
    public int YPos { get; set; }
}
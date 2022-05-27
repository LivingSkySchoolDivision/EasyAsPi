namespace Envirosaurus;

public class FacilityMap : IGUIDable
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    //public Guid MapStoredImageGuid { get; set; }
    public string MapImageBase64 { get; set; }
    public string MapImageContentType { get; set; }

    public string WeatherStationCode { get; set; }
}
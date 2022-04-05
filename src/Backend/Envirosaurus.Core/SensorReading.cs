using System.Text.Json.Serialization;

namespace Envirosaurus;

public class SensorReading : IGUIDable
{    
    // Fields filled in by the API:    
    public Guid Id { get; set; }
    public DateTime TimestampUTC { get; set; }

    // Payload from the sensors:
    public string DeviceSerialNumber { get; set; }
    
    public decimal? TemperatureCelsius { get; set; } 
    public decimal? HumidityPercent { get; set; }
    public decimal? Pressure { get; set; }
    public decimal? Lux { get; set; }
    public decimal? Noise { get; set; }
    public decimal? OxidisingGasLevel { get; set; } // Nitrogen Dioxide
    public decimal? ReducingGasLevel { get; set; } // Carbon Monoxide    
    public decimal? NH3Level { get; set; } // nh3
    public decimal? CPUTemperatureCelsius { get; set; } 
    

    // Alternate names for sensors:
    [JsonIgnore]
    public decimal? NitrogenDioxideLevel { get { return this.OxidisingGasLevel; }}
    [JsonIgnore]
    public decimal? CarbonMonoxideLevel { get { return this.ReducingGasLevel; }}
    [JsonIgnore]
    public decimal? AmmoniaLevel { get { return this.NH3Level; }}
}
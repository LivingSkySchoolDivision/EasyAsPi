namespace Envirosaurus;

public class SensorReading : IGUIDable
{
    
    // Fields filled in by the API:
    
    public Guid Id { get; set; }
    public DateTime TimestampUTC { get; set; }


    // Payload from the sensors:
    public string DeviceSerialNumber { get; set; }

    
    public string DeviceModel { get; set; } = string.Empty;    
    public decimal TemperatureCelsius { get; set; } 
    public decimal HumidityPercent { get; set; }
    public decimal Pressure { get; set; }
    public decimal Lux { get; set; }
    public decimal Noise { get; set; }
    public decimal OxidisingGasLevel { get; set; } // Nitrogen Dioxide
    public decimal ReducingGasLevel { get; set; } // Carbon Monoxide    
    public decimal NH3Level { get; set; } // nh3
    public decimal CPUTemperatureCelsius { get; set; } 

    public bool TemperatureAlarmHigh { get; set;}
    public bool TemperatureAlarmLow { get; set;}
    public bool HumidityAlarmHigh { get;set; }
    public bool HumidityAlarmLow { get;set; }
    public bool OxidisedAlarmHigh { get;set; }
    public bool ReducingGasAlarmHigh { get; set; }
    public bool NH3AlarmHigh { get; set; }
    public bool NoiseAlarmHigh { get; set; }
    

    // Alternate names for sensors:
    
    public decimal NitrogenDioxideLevel { get { return this.OxidisingGasLevel; }}
    public decimal CarbonMonoxideLevel { get { return this.ReducingGasLevel; }}
    public decimal AmmoniaLevel { get { return this.NH3Level; }}
    


}
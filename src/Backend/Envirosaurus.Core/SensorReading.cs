namespace Envirosaurus;

public class SensorReading
{
    public Guid Id { get; set; }

    public decimal TemperatureCelsius { get; set; } 
    public decimal HumidityPercent { get; set; }
    public decimal Pressure { get; set; }
    public decimal Lux { get; set; }
    public decimal Noise { get; set; }
    public decimal OxidisingGasLevel { get; set; } // Nitrogen Dioxide
    public decimal ReducingGasLevel { get; set; } // Carbon Monoxide
    public decimal AmmoniaLevel { get; set; }


    public decimal NitrogenDioxideLevel { get { return this.OxidisingGasLevel; }}
    public decimal CarbonMonoxideLevel { get { return this.ReducingGasLevel; }}


}
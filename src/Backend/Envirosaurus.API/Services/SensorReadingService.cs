using Microsoft.AspNetCore.Mvc;

namespace Envirosaurus.API.Services;

public class SensorReadingService
{
    private readonly IRepository<SensorReading> _repository;
    public SensorReadingService (IRepository<SensorReading> Repository) 
    {
        this._repository = Repository;
    }

    public IEnumerable<SensorReading> GetAll()
    {
        return this._repository.GetAll();
    }

    public SensorReading Get(Guid guid)
    {
        return this._repository.GetById(guid);
    }

    public IEnumerable<SensorReading> GetForSensor(Sensor sensor)
    {
        return this._repository.Find(x => x.DeviceSerialNumber == sensor.DeviceSerialNumber);
    }

    public void Insert(SensorReading Reading)
    {
        this._repository.Insert(Reading);
    }

}

using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;

namespace Envirosaurus.API.Services;

public class SensorReadingService
{
    private readonly IRepository<SensorReading> _repository;
    public SensorReadingService (IRepository<SensorReading> Repository) 
    {
        this._repository = Repository;
    }

    public SensorReading Get(Guid guid)
    {
        return this._repository.GetById(guid);
    }

    public IEnumerable<SensorReading> GetForSensor(Sensor sensor)
    {
        return this._repository.Find(x => x.DeviceSerialNumber == sensor.DeviceSerialNumber);
    }

    public Guid Insert(SensorReading Reading)
    {
        return this._repository.Insert(Reading);
    }

    public IList<SensorReading> Find(Expression<Func<SensorReading, bool>> predicate)
    {
        return this._repository.Find(predicate);
    }

    public IList<SensorReading> Find(Expression<Func<SensorReading, bool>> predicate, int limit, Expression<Func<SensorReading, object>> orderby, bool sortdescending)
    {
        return this._repository.Find(predicate, limit, orderby, sortdescending);
    }

}

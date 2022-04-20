using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;

namespace Envirosaurus.Web.Services;

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

    public IList<SensorReading> Find(Expression<Func<SensorReading, bool>> predicate)
    {
        return this._repository.Find(predicate);
    }

    public IList<SensorReading> Find(Expression<Func<SensorReading, bool>> predicate, int limit, Expression<Func<SensorReading, object>> orderby, bool sortdescending)
    {
        return this._repository.Find(predicate, limit, orderby, sortdescending);
    }

    public SensorReading GetLastReadingForSensor(string sensorSerialNumber)
    {
        return this._repository.Find(x => x.DeviceSerialNumber == sensorSerialNumber, 1, x => x.TimestampUTC, true).FirstOrDefault() ?? new SensorReading();
    }

}

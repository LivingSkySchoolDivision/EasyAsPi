using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;

namespace Envirosaurus.Web.Services;

public class SensorService
{
    private readonly IRepository<Sensor> _repository;
    public SensorService (IRepository<Sensor> Repository)
    {
        this._repository = Repository;
    }

    public IEnumerable<Sensor> GetAll()
    {
        return this._repository.GetAll();
    }

    public Sensor Get(Guid guid)
    {
        return this._repository.GetById(guid);
    }

    public IList<Sensor> Find(Expression<Func<Sensor, bool>> predicate)
    {
        return this._repository.Find(predicate);
    }

    public int GetNextID()
    {
        int newID = 0;
        foreach(Sensor sensor in this._repository.GetAll())
        {
            if (sensor.AssignedNumber > newID)
            {
                newID = sensor.AssignedNumber;
            }
        }
        return newID + 1;
    }

    public void Insert(Sensor Sensor)
    {
        this._repository.Update(Sensor);
    }

    public void Update(Sensor Sensor)
    {
        this._repository.Update(Sensor);
    }

}

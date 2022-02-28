using Microsoft.AspNetCore.Mvc;

namespace Envirosaurus.API.Services;

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

}

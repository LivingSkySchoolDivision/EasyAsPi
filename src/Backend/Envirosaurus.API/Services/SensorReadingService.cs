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

}

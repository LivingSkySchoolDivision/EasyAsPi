using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;

namespace Envirosaurus.Web.Services;

public class FacilityMapService
{
    private readonly IRepository<FacilityMap> _repository;
    public FacilityMapService (IRepository<FacilityMap> Repository)
    {
        this._repository = Repository;
    }

    public IEnumerable<FacilityMap> GetAll()
    {
        return this._repository.GetAll();
    }

    public FacilityMap Get(Guid guid)
    {
        return this._repository.GetById(guid);
    }

    public IList<FacilityMap> Find(Expression<Func<FacilityMap, bool>> predicate)
    {
        return this._repository.Find(predicate);
    }


    public void Insert(FacilityMap FacilityMap)
    {
        this._repository.Update(FacilityMap);
    }

    public void Update(FacilityMap FacilityMap)
    {
        this._repository.Update(FacilityMap);
    }

    public IList<FacilityMap> Find(Expression<Func<FacilityMap, bool>> predicate, int limit, Expression<Func<FacilityMap, object>> orderby, bool sortdescending)
    {
        return this._repository.Find(predicate, limit, orderby, sortdescending);
    }

}

using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IFlatDal
{
    long Add(Flat flat);
    void Delete(long id);
    IEnumerable<Flat> GetByApartmentId(long apartmentId);
    Flat GetByFlatCode(string flatCode);
    Flat GetById(long id);
    FlatExt GetExtById(long id);
    IEnumerable<FlatExt> GetExtsByBusinessId(int businessId);
    void Update(Flat flat);
}

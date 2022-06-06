using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IFlatDal
{
    long Add(Flat flat);
    void Delete(long id);
    List<Flat> GetByApartmentId(long apartmentId);
    Flat GetByFlatCode(string flatCode);
    Flat GetById(long id);
    FlatExt GetExtById(long id);
    List<FlatExt> GetExtsByBusinessId(int businessId);
    void Update(Flat flat);
}

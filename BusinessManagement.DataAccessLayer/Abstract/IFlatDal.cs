using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract
{
    public interface IFlatDal
    {
        Flat Add(Flat flat);
        void Delete(long id);
        List<Flat> GetByApartmentId(long apartmentId);
        Flat GetByFlatCode(string flatCode);
        Flat GetById(long id);
        List<Flat> GetExtsByBusinessId(int businessId);
        void Update(Flat flat);
    }
}

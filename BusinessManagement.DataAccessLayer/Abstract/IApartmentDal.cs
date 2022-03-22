using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract
{
    public interface IApartmentDal
    {
        Apartment Add(Apartment apartment);
        void Delete(long id);
        Apartment GetByApartmentCode(string apartmentCode);
        Apartment GetById(long id);
        List<Apartment> GetBySectionId(int sectionId);
        List<Apartment> GetExtsByBusinessId(int businessId);
        void Update(Apartment apartment);
    }
}

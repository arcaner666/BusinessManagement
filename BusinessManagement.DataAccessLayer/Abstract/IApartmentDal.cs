using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IApartmentDal
{
    long Add(Apartment apartment);
    void Delete(long id);
    Apartment GetByApartmentCode(string apartmentCode);
    IEnumerable<Apartment> GetByBusinessId(int businessId);
    Apartment GetById(long id);
    IEnumerable<Apartment> GetBySectionId(int sectionId);
    ApartmentExt GetExtById(long id);
    IEnumerable<ApartmentExt> GetExtsByBusinessId(int businessId);
    void Update(Apartment apartment);
}

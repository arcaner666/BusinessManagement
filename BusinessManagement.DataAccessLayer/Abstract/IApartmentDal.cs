using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IApartmentDal
{
    long Add(Apartment apartment);
    void Delete(long id);
    Apartment GetByApartmentCode(string apartmentCode);
    List<Apartment> GetByBusinessId(int businessId);
    Apartment GetById(long id);
    List<Apartment> GetBySectionId(int sectionId);
    ApartmentExt GetExtById(long id);
    List<ApartmentExt> GetExtsByBusinessId(int businessId);
    void Update(Apartment apartment);
}

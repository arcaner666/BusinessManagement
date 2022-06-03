using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IApartmentDal
{
    long Add(ApartmentDto apartmentDto);
    void Delete(long id);
    ApartmentDto GetByApartmentCode(string apartmentCode);
    IEnumerable<ApartmentDto> GetByBusinessId(int businessId);
    ApartmentDto GetById(long id);
    IEnumerable<ApartmentDto> GetBySectionId(int sectionId);
    ApartmentExtDto GetExtById(long id);
    IEnumerable<ApartmentExtDto> GetExtsByBusinessId(int businessId);
    void Update(ApartmentDto apartmentDto);
}

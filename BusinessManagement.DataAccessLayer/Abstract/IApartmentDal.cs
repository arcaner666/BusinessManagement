using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IApartmentDal
{
    long Add(ApartmentDto apartmentDto);
    void Delete(long id);
    ApartmentDto GetByApartmentCode(string apartmentCode);
    List<ApartmentDto> GetByBusinessId(int businessId);
    ApartmentDto GetById(long id);
    List<ApartmentDto> GetBySectionId(int sectionId);
    ApartmentExtDto GetExtById(long id);
    List<ApartmentExtDto> GetExtsByBusinessId(int businessId);
    void Update(ApartmentDto apartmentDto);
}

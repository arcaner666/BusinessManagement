using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IApartmentBl
{
    IDataResult<ApartmentDto> Add(ApartmentDto apartmentDto);
    IResult Delete(long id);
    IDataResult<IEnumerable<ApartmentDto>> GetByBusinessId(int businessId);
    IDataResult<ApartmentDto> GetById(long id);
    IDataResult<IEnumerable<ApartmentDto>> GetBySectionId(int sectionId);
    IResult Update(ApartmentDto apartmentDto);
}

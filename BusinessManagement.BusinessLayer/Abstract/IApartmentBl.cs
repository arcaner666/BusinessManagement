using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IApartmentBl
{
    IDataResult<ApartmentDto> Add(ApartmentDto apartmentDto);
    IResult Delete(long id);
    IDataResult<List<ApartmentDto>> GetByBusinessId(int businessId);
    IDataResult<ApartmentDto> GetById(long id);
    IDataResult<List<ApartmentDto>> GetBySectionId(int sectionId);
    IResult Update(ApartmentDto apartmentDto);
}

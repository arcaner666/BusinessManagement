using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IApartmentBl
{
    IDataResult<ApartmentDto> Add(ApartmentDto apartmentDto);
    IResult AddExt(ApartmentExtDto apartmentExtDto);
    IResult Delete(int id);
    IResult DeleteExt(int id);
    IDataResult<ApartmentDto> GetById(int id);
    IDataResult<List<ApartmentDto>> GetBySectionId(int sectionId);
    IDataResult<List<ApartmentExtDto>> GetExtsByBusinessId(int businessId);
    IResult Update(ApartmentDto apartmentDto);
}

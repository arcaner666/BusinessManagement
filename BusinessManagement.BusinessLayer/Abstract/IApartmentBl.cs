using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IApartmentBl
{
    IDataResult<ApartmentDto> Add(ApartmentDto apartmentDto);
    IResult AddExt(ApartmentExtDto apartmentExtDto);
    IResult Delete(long id);
    IResult DeleteExt(long id);
    IDataResult<List<ApartmentDto>> GetByBusinessId(int businessId);
    IDataResult<ApartmentDto> GetById(long id);
    IDataResult<List<ApartmentDto>> GetBySectionId(int sectionId);
    IDataResult<ApartmentExtDto> GetExtById(long id);
    IDataResult<List<ApartmentExtDto>> GetExtsByBusinessId(int businessId);
    IResult Update(ApartmentDto apartmentDto);
    IResult UpdateExt(ApartmentExtDto apartmentExtDto);
}

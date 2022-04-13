using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IApartmentExtBl
{
    IResult AddExt(ApartmentExtDto apartmentExtDto);
    IResult DeleteExt(long id);
    IDataResult<ApartmentExtDto> GetExtById(long id);
    IDataResult<List<ApartmentExtDto>> GetExtsByBusinessId(int businessId);
    IResult UpdateExt(ApartmentExtDto apartmentExtDto);
}

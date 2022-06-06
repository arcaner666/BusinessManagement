using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IFlatBl
{
    IDataResult<FlatDto> Add(FlatDto flatDto);
    IResult Delete(long id);
    IDataResult<List<FlatDto>> GetByApartmentId(long apartmentId);
    IDataResult<FlatDto> GetById(long id);
    IDataResult<FlatExtDto> GetExtById(long id);
    IDataResult<List<FlatExtDto>> GetExtsByBusinessId(int businessId);
    IResult Update(FlatDto flatDto);
}

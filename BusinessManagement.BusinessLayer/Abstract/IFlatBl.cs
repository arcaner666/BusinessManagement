using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IFlatBl
{
    IDataResult<FlatDto> Add(FlatDto flatDto);
    IResult Delete(long id);
    IDataResult<List<FlatDto>> GetByApartmentId(long apartmentId);
    IDataResult<FlatDto> GetById(long id);
    IResult Update(FlatDto flatDto);
}

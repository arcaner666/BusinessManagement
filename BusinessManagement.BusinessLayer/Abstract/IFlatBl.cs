using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IFlatBl
{
    IDataResult<FlatDto> Add(FlatDto flatDto);
    IResult AddExt(FlatExtDto flatExtDto);
    IResult Delete(long id);
    IResult DeleteExt(long id);
    IDataResult<FlatDto> GetById(long id);
    IDataResult<FlatExtDto> GetExtById(long id);
    IDataResult<List<FlatExtDto>> GetExtsByBusinessId(int businessId);
    IResult Update(FlatDto flatDto);
    IResult UpdateExt(FlatExtDto flatExtDto);
}

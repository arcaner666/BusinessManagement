using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IFlatExtBl
{
    IResult AddExt(FlatExtDto flatExtDto);
    IResult DeleteExt(long id);
    IDataResult<FlatExtDto> GetExtById(long id);
    IDataResult<List<FlatExtDto>> GetExtsByBusinessId(int businessId);
    IResult UpdateExt(FlatExtDto flatExtDto);
}

using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface ICashExtBl
{
    IResult AddExt(CashExtDto cashExtDto);
    IResult DeleteExt(long id);
    IResult DeleteExtByAccountId(long accountId);
    IDataResult<CashExtDto> GetExtByAccountId(long accountId);
    IDataResult<CashExtDto> GetExtById(long id);
    IDataResult<List<CashExtDto>> GetExtsByBusinessId(int businessId);
    IResult UpdateExt(CashExtDto cashExtDto);
}

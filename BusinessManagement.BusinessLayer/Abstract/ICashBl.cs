using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface ICashBl
{
    IDataResult<CashDto> Add(CashDto cashDto);
    IResult Delete(long id);
    IDataResult<CashDto> GetByAccountId(long accountId);
    IDataResult<List<CashDto>> GetByBusinessId(int businessId);
    IDataResult<CashDto> GetById(long id);
    IDataResult<CashExtDto> GetExtByAccountId(long accountId);
    IDataResult<CashExtDto> GetExtById(long id);
    IDataResult<List<CashExtDto>> GetExtsByBusinessId(int businessId);
    IResult Update(CashDto cashDto);
}

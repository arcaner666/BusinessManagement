using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IAccountBl
{
    IDataResult<AccountDto> Add(AccountDto accountDto);
    IResult Delete(long id);
    IDataResult<AccountDto> GetById(long id);
    IDataResult<AccountExtDto> GetExtById(long id);
    IDataResult<List<AccountExtDto>> GetExtsByBusinessId(int businessId);
    IDataResult<List<AccountExtDto>> GetExtsByBusinessIdAndAccountGroupCodes(AccountGetByAccountGroupCodesDto accountGetByAccountGroupCodesDto);
    IResult Update(AccountDto accountDto);
}

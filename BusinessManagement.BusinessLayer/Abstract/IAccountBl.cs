using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IAccountBl
{
    IDataResult<AccountDto> Add(AccountDto accountDto);
    IResult AddExt(AccountExtDto accountExtDto);
    IResult Delete(long id);
    IResult DeleteExt(long id);
    IDataResult<AccountCodeDto> GenerateAccountCode(int businessId, long branchId, string accountGroupCode);
    IDataResult<AccountDto> GetById(long id);
    IDataResult<AccountExtDto> GetExtById(long id);
    IDataResult<List<AccountExtDto>> GetExtsByBusinessId(int businessId);
    IDataResult<List<AccountExtDto>> GetExtsByBusinessIdAndAccountGroupCodes(AccountGetByAccountGroupCodesDto accountGetByAccountGroupCodesDto);
    IResult Update(AccountDto accountDto);
    IResult UpdateExt(AccountExtDto accountExtDto);
}

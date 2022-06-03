using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IAccountExtBl
{
    IResult AddExt(AccountExtDto accountExtDto);
    IResult DeleteExt(long id);
    IDataResult<AccountCodeDto> GenerateAccountCode(int businessId, long branchId, string accountGroupCode);
    IDataResult<AccountExtDto> GetExtById(long id);
    IDataResult<IEnumerable<AccountExtDto>> GetExtsByBusinessId(int businessId);
    IDataResult<IEnumerable<AccountExtDto>> GetExtsByBusinessIdAndAccountGroupCodes(AccountGetByAccountGroupCodesDto accountGetByAccountGroupCodesDto);
    IResult UpdateExt(AccountExtDto accountExtDto);
}

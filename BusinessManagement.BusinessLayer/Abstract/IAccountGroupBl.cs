using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IAccountGroupBl
{
    IDataResult<IEnumerable<AccountGroupDto>> GetAll();
    IDataResult<AccountGroupDto> GetByAccountGroupCode(string accountGroupCode);
    IDataResult<IEnumerable<AccountGroupDto>> GetByAccountGroupCodes(AccountGroupCodesDto accountGroupCodesDto);
    IDataResult<AccountGroupDto> GetById(short id);
}

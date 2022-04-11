using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IAccountGroupBl
{
    IDataResult<List<AccountGroupDto>> GetAll();
    IDataResult<AccountGroupDto> GetByAccountGroupCode(string accountGroupCode);
    IDataResult<List<AccountGroupDto>> GetByAccountGroupCodes(AccountGroupCodesDto accountGroupCodesDto);
    IDataResult<AccountGroupDto> GetById(short id);
}

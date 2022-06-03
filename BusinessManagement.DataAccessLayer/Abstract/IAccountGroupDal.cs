using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IAccountGroupDal
{
    IEnumerable<AccountGroupDto> GetAll();
    AccountGroupDto GetByAccountGroupCode(string accountGroupCode);
    IEnumerable<AccountGroupDto> GetByAccountGroupCodes(string[] accountGroupCodes);
    AccountGroupDto GetById(short id);
}

using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IAccountGroupDal
{
    List<AccountGroupDto> GetAll();
    AccountGroupDto GetByAccountGroupCode(string accountGroupCode);
    List<AccountGroupDto> GetByAccountGroupCodes(string[] accountGroupCodes);
    AccountGroupDto GetById(short id);
}

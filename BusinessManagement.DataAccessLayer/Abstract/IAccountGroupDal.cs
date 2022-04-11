using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IAccountGroupDal
{
    List<AccountGroup> GetAll();
    AccountGroup GetByAccountGroupCode(string accountGroupCode);
    List<AccountGroup> GetByAccountGroupCodes(string[] accountGroupCodes);
    AccountGroup GetById(short id);
}

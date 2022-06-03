using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IAccountGroupDal
{
    IEnumerable<AccountGroup> GetAll();
    AccountGroup GetByAccountGroupCode(string accountGroupCode);
    IEnumerable<AccountGroup> GetByAccountGroupCodes(string[] accountGroupCodes);
    AccountGroup GetById(short id);
}

using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract
{
    public interface IAccountGroupDal
    {
        List<AccountGroup> GetAll();
        AccountGroup GetByAccountGroupCode(string accountGroupCode);
        AccountGroup GetById(short id);
    }
}

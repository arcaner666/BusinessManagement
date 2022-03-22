using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract
{
    public interface IAccountDal
    {
        Account Add(Account account);
        void Delete(long id);
        List<Account> GetByAccountGroupId(short accountGroupId);
        Account GetByBusinessIdAndAccountCode(int businessId, string accountCode);
        Account GetById(long id);
        List<Account> GetExtsByBusinessId(int businessId);
        List<Account> GetExtsByBusinessIdAndAccountGroupCode(int businessId, string[] accountGroupCodes);
        Account GetLastAccountOrderForAnAccountGroup(int businessId, long branchId, string accountGroupCode);
        Account GetMaxAccountOrderByBusinessIdAndBranchIdAndAccountGroupId(int businessId, long branchId, short accountGroupId);
        void Update(Account account);
    }
}

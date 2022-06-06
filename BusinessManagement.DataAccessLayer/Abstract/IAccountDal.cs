using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IAccountDal
{
    long Add(Account account);
    void Delete(long id);
    List<Account> GetByAccountGroupId(short accountGroupId);
    Account GetByBusinessIdAndAccountCode(int businessId, string accountCode);
    Account GetById(long id);
    AccountExt GetExtById(long id);
    List<AccountExt> GetExtsByBusinessId(int businessId);
    List<AccountExt> GetExtsByBusinessIdAndAccountGroupCodes(int businessId, string[] accountGroupCodes);
    int GetMaxAccountOrderByBusinessIdAndBranchIdAndAccountGroupId(int businessId, long branchId, short accountGroupId);
    void Update(Account account);
}

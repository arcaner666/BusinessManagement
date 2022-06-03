using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IAccountDal
{
    long Add(AccountDto accountDto);
    void Delete(long id);
    IEnumerable<AccountDto> GetByAccountGroupId(short accountGroupId);
    AccountDto GetByBusinessIdAndAccountCode(int businessId, string accountCode);
    AccountDto GetById(long id);
    AccountExtDto GetExtById(long id);
    IEnumerable<AccountExtDto> GetExtsByBusinessId(int businessId);
    IEnumerable<AccountExtDto> GetExtsByBusinessIdAndAccountGroupCodes(int businessId, string[] accountGroupCodes);
    int GetMaxAccountOrderByBusinessIdAndBranchIdAndAccountGroupId(int businessId, long branchId, short accountGroupId);
    void Update(AccountDto accountDto);
}

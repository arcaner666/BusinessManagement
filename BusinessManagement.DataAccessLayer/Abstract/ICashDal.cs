using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface ICashDal
{
    long Add(Cash cash);
    void Delete(long id);
    Cash GetByAccountId(long accountId);
    IEnumerable<Cash> GetByBusinessId(int businessId);
    Cash GetByBusinessIdAndAccountId(int businessId, long accountId);
    Cash GetById(long id);
    CashExt GetExtByAccountId(long accountId);
    CashExt GetExtById(long id);
    IEnumerable<CashExt> GetExtsByBusinessId(int businessId);
    void Update(Cash cash);
}

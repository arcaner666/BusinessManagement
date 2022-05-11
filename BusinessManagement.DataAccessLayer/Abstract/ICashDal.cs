using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface ICashDal
{
    Cash Add(Cash cash);
    void Delete(long id);
    Cash GetByAccountId(long accountId);
    List<Cash> GetByBusinessId(int businessId);
    Cash GetByBusinessIdAndAccountId(int businessId, long accountId);
    Cash GetById(long id);
    Cash GetExtByAccountId(long accountId);
    CashExtDto GetExtById(long id);
    List<Cash> GetExtsByBusinessId(int businessId);
    void Update(Cash cash);
}

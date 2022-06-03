using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface ICashDal
{
    long Add(CashDto cashDto);
    void Delete(long id);
    CashDto GetByAccountId(long accountId);
    IEnumerable<CashDto> GetByBusinessId(int businessId);
    CashDto GetByBusinessIdAndAccountId(int businessId, long accountId);
    CashDto GetById(long id);
    CashExtDto GetExtByAccountId(long accountId);
    CashExtDto GetExtById(long id);
    IEnumerable<CashExtDto> GetExtsByBusinessId(int businessId);
    void Update(CashDto cashDto);
}

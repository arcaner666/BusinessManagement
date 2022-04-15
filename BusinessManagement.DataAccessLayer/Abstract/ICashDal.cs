using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface ICashDal
{
    Cash Add(Cash cash);
    void Delete(long id);
    List<Cash> GetByBusinessId(int businessId);
    Cash GetByBusinessIdAndAccountId(int businessId, long accountId);
    Cash GetById(long id);
    Cash GetExtById(long id);
    List<Cash> GetExtsByBusinessId(int businessId);
    void Update(Cash cash);
}

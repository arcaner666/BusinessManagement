using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IBankDal
{
    Bank Add(Bank bank);
    void Delete(long id);
    Bank GetByAccountId(long accountId);
    public List<Bank> GetByBusinessId(int businessId);
    Bank GetByBusinessIdAndIban(int businessId, string iban);
    Bank GetById(long id);
    Bank GetExtByAccountId(long accountId);
    Bank GetExtById(long id);
    List<Bank> GetExtsByBusinessId(int businessId);
    void Update(Bank bank);
}

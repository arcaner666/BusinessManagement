using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IBankDal
{
    long Add(Bank bank);
    void Delete(long id);
    Bank GetByAccountId(long accountId);
    public IEnumerable<Bank> GetByBusinessId(int businessId);
    Bank GetByBusinessIdAndIban(int businessId, string iban);
    Bank GetById(long id);
    BankExt GetExtByAccountId(long accountId);
    BankExt GetExtById(long id);
    IEnumerable<BankExt> GetExtsByBusinessId(int businessId);
    void Update(Bank bank);
}

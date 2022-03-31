using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IBankDal
{
    Bank Add(Bank bank);
    void Delete(long id);
    Bank GetById(long id);
    List<Bank> GetExtsByBusinessId(int businessId);
    Bank GetIfAlreadyExist(int businessId, string iban);
    void Update(Bank bank);
}

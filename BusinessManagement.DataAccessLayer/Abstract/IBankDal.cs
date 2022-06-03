using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IBankDal
{
    long Add(BankDto bankDto);
    void Delete(long id);
    BankDto GetByAccountId(long accountId);
    public IEnumerable<BankDto> GetByBusinessId(int businessId);
    BankDto GetByBusinessIdAndIban(int businessId, string iban);
    BankDto GetById(long id);
    BankExtDto GetExtByAccountId(long accountId);
    BankExtDto GetExtById(long id);
    IEnumerable<BankExtDto> GetExtsByBusinessId(int businessId);
    void Update(BankDto bankDto);
}

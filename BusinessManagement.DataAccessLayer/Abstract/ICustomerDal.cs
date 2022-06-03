using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface ICustomerDal
{
    long Add(Customer customer);
    Customer GetByAccountId(long accountId);
    IEnumerable<Customer> GetByBusinessId(int businessId);
    Customer GetByBusinessIdAndSystemUserId(int businessId, long systemUserId);
    Customer GetById(long id);
    IEnumerable<Customer> GetExtsByBusinessId(int businessId);
    void Update(Customer customer);
}

using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface ICustomerDal
{
    Customer Add(Customer customer);
    Customer GetByAccountId(long accountId);
    List<Customer> GetByBusinessId(int businessId);
    Customer GetByBusinessIdAndSystemUserId(int businessId, long systemUserId);
    Customer GetById(long id);
    List<Customer> GetExtsByBusinessId(int businessId);
    void Update(Customer customer);
}

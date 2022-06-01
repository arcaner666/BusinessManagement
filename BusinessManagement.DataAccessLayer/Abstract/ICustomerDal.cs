using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface ICustomerDal
{
    long Add(CustomerDto customerDto);
    CustomerDto GetByAccountId(long accountId);
    List<CustomerDto> GetByBusinessId(int businessId);
    CustomerDto GetByBusinessIdAndSystemUserId(int businessId, long systemUserId);
    CustomerDto GetById(long id);
    List<CustomerDto> GetExtsByBusinessId(int businessId);
    void Update(CustomerDto customerDto);
}

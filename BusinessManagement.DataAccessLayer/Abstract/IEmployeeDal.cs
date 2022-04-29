using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IEmployeeDal
{
    Employee Add(Employee employee);
    void Delete(long id);
    Employee GetByAccountId(long accountId);
    Employee GetByBusinessIdAndAccountId(int businessId, long accountId);
    Employee GetById(long id);
    Employee GetExtById(long id);
    List<Employee> GetExtsByBusinessId(int businessId);
    void Update(Employee employee);
}

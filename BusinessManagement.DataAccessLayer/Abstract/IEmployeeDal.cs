using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IEmployeeDal
{
    Employee Add(Employee employee);
    void Delete(long id);
    Employee GetByAccountId(long accountId);
    Employee GetById(long id);
    List<Employee> GetExtsByBusinessId(int businessId);
    Employee GetIfAlreadyExist(int businessId, long accountId);
    void Update(Employee employee);
}

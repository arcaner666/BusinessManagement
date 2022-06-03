using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IEmployeeDal
{
    long Add(Employee employee);
    void Delete(long id);
    Employee GetByAccountId(long accountId);
    Employee GetByBusinessIdAndAccountId(int businessId, long accountId);
    Employee GetById(long id);
    EmployeeExt GetExtByAccountId(long accountId);
    EmployeeExt GetExtById(long id);
    IEnumerable<EmployeeExt> GetExtsByBusinessId(int businessId);
    void Update(Employee employee);
}

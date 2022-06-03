using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IEmployeeDal
{
    long Add(EmployeeDto employeeDto);
    void Delete(long id);
    EmployeeDto GetByAccountId(long accountId);
    EmployeeDto GetByBusinessIdAndAccountId(int businessId, long accountId);
    EmployeeDto GetById(long id);
    EmployeeExtDto GetExtByAccountId(long accountId);
    EmployeeExtDto GetExtById(long id);
    IEnumerable<EmployeeExtDto> GetExtsByBusinessId(int businessId);
    void Update(EmployeeDto employeeDto);
}

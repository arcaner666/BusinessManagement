using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IEmployeeBl
{
    IDataResult<EmployeeDto> Add(EmployeeDto employeeDto);
    IResult Delete(long id);
    IDataResult<EmployeeDto> GetByAccountId(long accountId);
    IDataResult<EmployeeDto> GetById(long id);
    IDataResult<EmployeeExtDto> GetExtByAccountId(long accountId);
    IDataResult<EmployeeExtDto> GetExtById(long id);
    IDataResult<List<EmployeeExtDto>> GetExtsByBusinessId(int businessId);
    IResult Update(EmployeeDto employeeDto);
}

using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IEmployeeBl
{
    IDataResult<EmployeeDto> Add(EmployeeDto employeeDto);
    IResult Delete(long id);
    IDataResult<EmployeeDto> GetByAccountId(long accountId);
    IDataResult<EmployeeDto> GetById(long id);
    IResult Update(EmployeeDto employeeDto);
}

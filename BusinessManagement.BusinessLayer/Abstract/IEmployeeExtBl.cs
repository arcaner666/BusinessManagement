using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IEmployeeExtBl
{
    IResult AddExt(EmployeeExtDto employeeExtDto);
    IResult DeleteExt(long id);
    IResult DeleteExtByAccountId(long accountId);
    IDataResult<EmployeeExtDto> GetExtByAccountId(long accountId);
    IDataResult<EmployeeExtDto> GetExtById(long id);
    IDataResult<IEnumerable<EmployeeExtDto>> GetExtsByBusinessId(int businessId);
    IResult UpdateExt(EmployeeExtDto employeeExtDto);
}

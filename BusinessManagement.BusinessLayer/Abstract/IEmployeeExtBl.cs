using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IEmployeeExtBl
{
    IResult AddExt(EmployeeExtDto employeeExtDto);
    IResult DeleteExt(long id);
    IDataResult<EmployeeExtDto> GetExtById(long id);
    IDataResult<List<EmployeeExtDto>> GetExtsByBusinessId(int businessId);
    IResult UpdateExt(EmployeeExtDto employeeExtDto);
}

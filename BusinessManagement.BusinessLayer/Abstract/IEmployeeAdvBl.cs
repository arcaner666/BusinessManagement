using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IEmployeeAdvBl
{
    IResult Add(EmployeeExtDto employeeExtDto);
    IResult Delete(long id);
    IResult DeleteByAccountId(long accountId);
    IResult Update(EmployeeExtDto employeeExtDto);
}

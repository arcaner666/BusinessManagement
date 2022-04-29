using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class EmployeeBl : IEmployeeBl
{
    private readonly IEmployeeDal _employeeDal;

    public EmployeeBl(
        IEmployeeDal employeeDal
    )
    {
        _employeeDal = employeeDal;
    }

    public IDataResult<EmployeeDto> Add(EmployeeDto employeeDto)
    {
        Employee searchedEmployee = _employeeDal.GetByBusinessIdAndAccountId(employeeDto.BusinessId, employeeDto.AccountId);
        if (searchedEmployee is not null)
        {
            return new ErrorDataResult<EmployeeDto>(Messages.EmployeeAlreadyExists);
        }

        Employee addedEmployee = new()
        {
            BusinessId = employeeDto.BusinessId,
            BranchId = employeeDto.BranchId,
            AccountId = employeeDto.AccountId,
            EmployeeTypeId = employeeDto.EmployeeTypeId,
            NameSurname = employeeDto.NameSurname,
            Email = employeeDto.Email,
            Phone = employeeDto.Phone,
            DateOfBirth = employeeDto.DateOfBirth,
            Gender = employeeDto.Gender,
            Notes = employeeDto.Notes,
            AvatarUrl = employeeDto.AvatarUrl,
            StillWorking = true,
            StartDate = DateTime.Now,
            QuitDate = null,
            CreatedAt = DateTimeOffset.Now,
            UpdatedAt = DateTimeOffset.Now,
        };
        _employeeDal.Add(addedEmployee);

        EmployeeDto addedEmployeeDto = FillDto(addedEmployee);

        return new SuccessDataResult<EmployeeDto>(addedEmployeeDto, Messages.EmployeeAdded);
    }

    public IResult Delete(long id)
    {
        var getEmployeeResult = GetById(id);
        if (getEmployeeResult is null)
            return getEmployeeResult;

        _employeeDal.Delete(id);

        return new SuccessResult(Messages.EmployeeDeleted);
    }

    public IDataResult<EmployeeDto> GetById(long id)
    {
        Employee searchedEmployee = _employeeDal.GetById(id);
        if (searchedEmployee is null)
            return new ErrorDataResult<EmployeeDto>(Messages.EmployeeNotFound);

        EmployeeDto searchedEmployeeDto = FillDto(searchedEmployee);

        return new SuccessDataResult<EmployeeDto>(searchedEmployeeDto, Messages.EmployeeListedById);
    }

    public IResult Update(EmployeeDto employeeDto)
    {
        Employee searchedEmployee = _employeeDal.GetById(employeeDto.EmployeeId);
        if (searchedEmployee is null)
            return new ErrorDataResult<AccountDto>(Messages.EmployeeNotFound);

        searchedEmployee.EmployeeTypeId = employeeDto.EmployeeTypeId;
        searchedEmployee.NameSurname = employeeDto.NameSurname;
        searchedEmployee.Email = employeeDto.Email;
        searchedEmployee.DateOfBirth = employeeDto.DateOfBirth;
        searchedEmployee.Gender = employeeDto.Gender;
        searchedEmployee.Notes = employeeDto.Notes;
        searchedEmployee.AvatarUrl = employeeDto.AvatarUrl;
        searchedEmployee.StillWorking = employeeDto.StillWorking;
        searchedEmployee.StartDate = employeeDto.StartDate;
        searchedEmployee.QuitDate = employeeDto.QuitDate;
        searchedEmployee.UpdatedAt = DateTimeOffset.Now;
        _employeeDal.Update(searchedEmployee);

        return new SuccessResult(Messages.EmployeeUpdated);
    }

    private EmployeeDto FillDto(Employee employee)
    {
        EmployeeDto employeeDto = new()
        {
            EmployeeId = employee.EmployeeId,
            BusinessId = employee.BusinessId,
            BranchId = employee.BranchId,
            AccountId = employee.AccountId,
            EmployeeTypeId = employee.EmployeeTypeId,
            NameSurname = employee.NameSurname,
            Email = employee.Email,
            Phone = employee.Phone,
            DateOfBirth = employee.DateOfBirth,
            Gender = employee.Gender,
            Notes = employee.Notes,
            AvatarUrl = employee.AvatarUrl,
            StillWorking = employee.StillWorking,
            StartDate = employee.StartDate,
            QuitDate = employee.QuitDate,
            CreatedAt = employee.CreatedAt,
            UpdatedAt = employee.UpdatedAt,
        };

        return employeeDto;
    }

    private List<EmployeeDto> FillDtos(List<Employee> employees)
    {
        List<EmployeeDto> employeeDtos = employees.Select(employee => FillDto(employee)).ToList();

        return employeeDtos;
    }
}

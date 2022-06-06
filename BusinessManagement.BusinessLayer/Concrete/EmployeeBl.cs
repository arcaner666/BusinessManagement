using AutoMapper;
using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Concrete;

public class EmployeeBl : IEmployeeBl
{
    private readonly IEmployeeDal _employeeDal;
    private readonly IMapper _mapper;

    public EmployeeBl(
        IEmployeeDal employeeDal,
        IMapper mapper
    )
    {
        _employeeDal = employeeDal;
        _mapper = mapper;
    }

    public IDataResult<EmployeeDto> Add(EmployeeDto employeeDto)
    {
        Employee searchedEmployee = _employeeDal.GetByBusinessIdAndAccountId(employeeDto.BusinessId, employeeDto.AccountId);
        if (searchedEmployee is not null)
            return new ErrorDataResult<EmployeeDto>(Messages.EmployeeAlreadyExists);

        var addedEmployee = _mapper.Map<Employee>(employeeDto);

        addedEmployee.StillWorking = true;
        addedEmployee.StartDate = DateTime.Now;
        addedEmployee.QuitDate = null;
        addedEmployee.CreatedAt = DateTimeOffset.Now;
        addedEmployee.UpdatedAt = DateTimeOffset.Now;
        long id = _employeeDal.Add(addedEmployee);
        addedEmployee.EmployeeId = id;

        var addedEmployeeDto = _mapper.Map<EmployeeDto>(addedEmployee);

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

    public IDataResult<EmployeeDto> GetByAccountId(long accountId)
    {
        Employee employee = _employeeDal.GetByAccountId(accountId);
        if (employee is null)
            return new ErrorDataResult<EmployeeDto>(Messages.EmployeeNotFound);

        var employeeDto = _mapper.Map<EmployeeDto>(employee);

        return new SuccessDataResult<EmployeeDto>(employeeDto, Messages.EmployeeListedByAccountId);
    }

    public IDataResult<EmployeeDto> GetById(long id)
    {
        Employee employee = _employeeDal.GetById(id);
        if (employee is null)
            return new ErrorDataResult<EmployeeDto>(Messages.EmployeeNotFound);

        var employeeDto = _mapper.Map<EmployeeDto>(employee);

        return new SuccessDataResult<EmployeeDto>(employeeDto, Messages.EmployeeListedById);
    }

    public IDataResult<EmployeeExtDto> GetExtByAccountId(long accountId)
    {
        EmployeeExt employeeExt = _employeeDal.GetExtByAccountId(accountId);
        if (employeeExt is null)
            return new ErrorDataResult<EmployeeExtDto>(Messages.EmployeeNotFound);

        var employeeExtDto = _mapper.Map<EmployeeExtDto>(employeeExt);

        return new SuccessDataResult<EmployeeExtDto>(employeeExtDto, Messages.EmployeeExtListedByAccountId);
    }

    public IDataResult<EmployeeExtDto> GetExtById(long id)
    {
        EmployeeExt employeeExt = _employeeDal.GetExtById(id);
        if (employeeExt is null)
            return new ErrorDataResult<EmployeeExtDto>(Messages.EmployeeNotFound);

        var employeeExtDto = _mapper.Map<EmployeeExtDto>(employeeExt);

        return new SuccessDataResult<EmployeeExtDto>(employeeExtDto, Messages.EmployeeExtListedById);
    }

    public IDataResult<List<EmployeeExtDto>> GetExtsByBusinessId(int businessId)
    {
        List<EmployeeExt> employeeExts = _employeeDal.GetExtsByBusinessId(businessId);
        if (!employeeExts.Any())
            return new ErrorDataResult<List<EmployeeExtDto>>(Messages.EmployeesNotFound);

        var employeeExtDtos = _mapper.Map<List<EmployeeExtDto>>(employeeExts);

        return new SuccessDataResult<List<EmployeeExtDto>>(employeeExtDtos, Messages.EmployeeExtsListedByBusinessId);
    }

    public IResult Update(EmployeeDto employeeDto)
    {
        Employee employee = _employeeDal.GetById(employeeDto.EmployeeId);
        if (employee is null)
            return new ErrorDataResult<EmployeeDto>(Messages.EmployeeNotFound);

        employee.EmployeeTypeId = employeeDto.EmployeeTypeId;
        employee.NameSurname = employeeDto.NameSurname;
        employee.Email = employeeDto.Email;
        employee.DateOfBirth = employeeDto.DateOfBirth;
        employee.Gender = employeeDto.Gender;
        employee.Notes = employeeDto.Notes;
        employee.AvatarUrl = employeeDto.AvatarUrl;
        employee.IdentityNumber = employeeDto.IdentityNumber;
        employee.StillWorking = employeeDto.StillWorking;
        employee.StartDate = employeeDto.StartDate;
        employee.QuitDate = employeeDto.QuitDate;
        employee.UpdatedAt = DateTimeOffset.Now;
        _employeeDal.Update(employee);

        return new SuccessResult(Messages.EmployeeUpdated);
    }
}

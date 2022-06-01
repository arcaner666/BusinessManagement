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
        EmployeeDto searchedEmployeeDto = _employeeDal.GetByBusinessIdAndAccountId(employeeDto.BusinessId, employeeDto.AccountId);
        if (searchedEmployeeDto is not null)
        {
            return new ErrorDataResult<EmployeeDto>(Messages.EmployeeAlreadyExists);
        }

        employeeDto.StillWorking = true;
        employeeDto.StartDate = DateTime.Now;
        employeeDto.QuitDate = null;
        employeeDto.CreatedAt = DateTimeOffset.Now;
        employeeDto.UpdatedAt = DateTimeOffset.Now;
        long id = _employeeDal.Add(employeeDto);
        employeeDto.EmployeeId = id;

        return new SuccessDataResult<EmployeeDto>(employeeDto, Messages.EmployeeAdded);
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
        EmployeeDto employeeDto = _employeeDal.GetByAccountId(accountId);
        if (employeeDto is null)
            return new ErrorDataResult<EmployeeDto>(Messages.EmployeeNotFound);

        return new SuccessDataResult<EmployeeDto>(employeeDto, Messages.EmployeeListedByAccountId);
    }

    public IDataResult<EmployeeDto> GetById(long id)
    {
        EmployeeDto employeeDto = _employeeDal.GetById(id);
        if (employeeDto is null)
            return new ErrorDataResult<EmployeeDto>(Messages.EmployeeNotFound);

        return new SuccessDataResult<EmployeeDto>(employeeDto, Messages.EmployeeListedById);
    }

    public IResult Update(EmployeeDto employeeDto)
    {
        var searchedEmployeeResult = GetById(employeeDto.EmployeeId);
        if (!searchedEmployeeResult.Success)
            return searchedEmployeeResult;

        searchedEmployeeResult.Data.EmployeeTypeId = employeeDto.EmployeeTypeId;
        searchedEmployeeResult.Data.NameSurname = employeeDto.NameSurname;
        searchedEmployeeResult.Data.Email = employeeDto.Email;
        searchedEmployeeResult.Data.DateOfBirth = employeeDto.DateOfBirth;
        searchedEmployeeResult.Data.Gender = employeeDto.Gender;
        searchedEmployeeResult.Data.Notes = employeeDto.Notes;
        searchedEmployeeResult.Data.AvatarUrl = employeeDto.AvatarUrl;
        searchedEmployeeResult.Data.IdentityNumber = employeeDto.IdentityNumber;
        searchedEmployeeResult.Data.StillWorking = employeeDto.StillWorking;
        searchedEmployeeResult.Data.StartDate = employeeDto.StartDate;
        searchedEmployeeResult.Data.QuitDate = employeeDto.QuitDate;
        searchedEmployeeResult.Data.UpdatedAt = DateTimeOffset.Now;
        _employeeDal.Update(searchedEmployeeResult.Data);

        return new SuccessResult(Messages.EmployeeUpdated);
    }
}

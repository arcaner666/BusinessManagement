using AutoMapper;
using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Aspects.Autofac.Transaction;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Concrete;

public class EmployeeAdvBl : IEmployeeAdvBl
{
    private readonly IAccountBl _accountBl;
    private readonly IAccountGroupBl _accountGroupBl;
    private readonly IAccountTypeBl _accountTypeBl;
    private readonly IEmployeeBl _employeeBl;
    private readonly IEmployeeDal _employeeDal;
    private readonly IMapper _mapper;

    public EmployeeAdvBl(
        IAccountBl accountBl,
        IAccountGroupBl accountGroupBl,
        IAccountTypeBl accountTypeBl,
        IEmployeeBl employeeBl,
        IEmployeeDal employeeDal,
        IMapper mapper
    )
    {
        _accountBl = accountBl;
        _accountGroupBl = accountGroupBl;
        _accountTypeBl = accountTypeBl;
        _employeeBl = employeeBl;
        _employeeDal = employeeDal;
        _mapper = mapper;
    }

    [TransactionScopeAspect]
    public IResult Add(EmployeeExtDto employeeExtDto)
    {
        // Personelin hesap grubunun id'si getirilir.
        var getAccountGroupResult = _accountGroupBl.GetByAccountGroupCode("335");
        if (!getAccountGroupResult.Success)
            return getAccountGroupResult;

        // Personelin hesap tipinin id'si getirilir.
        var getAccountTypeResult = _accountTypeBl.GetByAccountTypeName("Personel");
        if (!getAccountTypeResult.Success)
            return getAccountTypeResult;

        // Yeni bir cari hesap oluşturulur.
        AccountDto accountDto = new()
        {
            BusinessId = employeeExtDto.BusinessId,
            BranchId = employeeExtDto.BranchId,
            AccountGroupId = getAccountGroupResult.Data.AccountGroupId,
            AccountTypeId = getAccountTypeResult.Data.AccountTypeId,
            AccountOrder = employeeExtDto.AccountOrder,
            AccountName = employeeExtDto.AccountName,
            AccountCode = employeeExtDto.AccountCode,
            Limit = employeeExtDto.Limit,
        };
        var addAccountResult = _accountBl.Add(accountDto);
        if (!addAccountResult.Success)
            return addAccountResult;

        // Yeni bir personel eklenir.
        EmployeeDto addedEmployeeDto = new()
        {
            BusinessId = employeeExtDto.BusinessId,
            BranchId = employeeExtDto.BranchId,
            AccountId = addAccountResult.Data.AccountId,
            EmployeeTypeId = employeeExtDto.EmployeeTypeId,
            NameSurname = employeeExtDto.NameSurname,
            Email = employeeExtDto.Email,
            Phone = employeeExtDto.Phone,
            DateOfBirth = employeeExtDto.DateOfBirth,
            Gender = employeeExtDto.Gender,
            Notes = employeeExtDto.Notes,
            AvatarUrl = employeeExtDto.AvatarUrl,
            IdentityNumber = employeeExtDto.IdentityNumber,
        };
        var addEmployeeResult = _employeeBl.Add(addedEmployeeDto);
        if (!addEmployeeResult.Success)
            return addEmployeeResult;

        return new SuccessResult(Messages.EmployeeExtAdded);
    }

    [TransactionScopeAspect]
    public IResult Delete(long id)
    {
        var searchedEmployeeResult = _employeeBl.GetById(id);
        if (!searchedEmployeeResult.Success)
            return searchedEmployeeResult;

        var deleteEmployeeResult = _employeeBl.Delete(searchedEmployeeResult.Data.EmployeeId);
        if (!deleteEmployeeResult.Success)
            return deleteEmployeeResult;

        var deleteAccountResult = _accountBl.Delete(searchedEmployeeResult.Data.AccountId);
        if (!deleteAccountResult.Success)
            return deleteAccountResult;

        return new SuccessResult(Messages.EmployeeExtDeleted);
    }

    [TransactionScopeAspect]
    public IResult DeleteByAccountId(long accountId)
    {
        var searchedEmployeeResult = _employeeBl.GetByAccountId(accountId);
        if (!searchedEmployeeResult.Success)
            return searchedEmployeeResult;

        var deleteEmployeeResult = _employeeBl.Delete(searchedEmployeeResult.Data.EmployeeId);
        if (!deleteEmployeeResult.Success)
            return deleteEmployeeResult;

        var deleteAccountResult = _accountBl.Delete(searchedEmployeeResult.Data.AccountId);
        if (!deleteAccountResult.Success)
            return deleteAccountResult;

        return new SuccessResult(Messages.EmployeeExtDeletedByAccountId);
    }

    [TransactionScopeAspect]
    public IResult Update(EmployeeExtDto employeeExtDto)
    {
        // Personelin cari hesabı güncellenir.
        AccountDto updatedAccountDto = new()
        {
            AccountId = employeeExtDto.AccountId,
            AccountName = employeeExtDto.AccountName,
            Limit = employeeExtDto.Limit,
         };
        var updateAccountResult = _accountBl.Update(updatedAccountDto);
        if (!updateAccountResult.Success)
            return updateAccountResult;

        // Personelin güncellenir.
        EmployeeDto updatedEmployeeDto = new()
        {
            EmployeeId = employeeExtDto.EmployeeId,
            EmployeeTypeId = employeeExtDto.EmployeeTypeId,
            NameSurname = employeeExtDto.NameSurname,
            Email = employeeExtDto.Email,
            DateOfBirth = employeeExtDto.DateOfBirth,
            Gender = employeeExtDto.Gender,
            Notes = employeeExtDto.Notes,
            AvatarUrl = employeeExtDto.AvatarUrl,
            IdentityNumber = employeeExtDto.IdentityNumber,
            StillWorking = employeeExtDto.StillWorking,
            StartDate = employeeExtDto.StartDate,
            QuitDate = employeeExtDto.QuitDate,
        };
        var updateEmployeeResult = _employeeBl.Update(updatedEmployeeDto);
        if (!updateEmployeeResult.Success)
            return updateEmployeeResult;

        return new SuccessResult(Messages.EmployeeExtUpdated);
    }
}

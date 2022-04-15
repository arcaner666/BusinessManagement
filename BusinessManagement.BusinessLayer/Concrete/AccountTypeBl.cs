using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class AccountTypeBl : IAccountTypeBl
{
    private readonly IAccountTypeDal _accountTypeDal;

    public AccountTypeBl(
        IAccountTypeDal accountTypeDal
    )
    {
        _accountTypeDal = accountTypeDal;
    }

    public IDataResult<List<AccountTypeDto>> GetAll()
    {
        List<AccountType> allAccountTypes = _accountTypeDal.GetAll();
        if (allAccountTypes.Count == 0)
            return new ErrorDataResult<List<AccountTypeDto>>(Messages.AccountTypesNotFound);

        List<AccountTypeDto> allAccountTypeDtos = FillDtos(allAccountTypes);

        return new SuccessDataResult<List<AccountTypeDto>>(allAccountTypeDtos, Messages.AccountTypesListed);
    }

    public IDataResult<AccountTypeDto> GetById(short id)
    {
        AccountType searchedAccountType = _accountTypeDal.GetById(id);
        if (searchedAccountType is null)
            return new ErrorDataResult<AccountTypeDto>(Messages.AccountTypeNotFound);

        AccountTypeDto searchedAccountTypeDto = FillDto(searchedAccountType);

        return new SuccessDataResult<AccountTypeDto>(searchedAccountTypeDto, Messages.AccountTypeListedById);
    }

    public IDataResult<AccountTypeDto> GetByAccountTypeName(string accountTypeName)
    {
        AccountType searchedAccountType = _accountTypeDal.GetByAccountTypeName(accountTypeName);
        if (searchedAccountType is null)
            return new ErrorDataResult<AccountTypeDto>(Messages.AccountTypeNotFound);

        AccountTypeDto searchedAccountTypeDto = FillDto(searchedAccountType);

        return new SuccessDataResult<AccountTypeDto>(searchedAccountTypeDto, Messages.AccountTypeListedByAccountTypeName);
    }

    private AccountTypeDto FillDto(AccountType accountType)
    {
        AccountTypeDto accountTypeDto = new()
        {
            AccountTypeId = accountType.AccountTypeId,
            AccountTypeName = accountType.AccountTypeName,
        };

        return accountTypeDto;
    }

    private List<AccountTypeDto> FillDtos(List<AccountType> accountTypes)
    {
        List<AccountTypeDto> accountTypeDtos = accountTypes.Select(accountType => FillDto(accountType)).ToList();

        return accountTypeDtos;
    }
}

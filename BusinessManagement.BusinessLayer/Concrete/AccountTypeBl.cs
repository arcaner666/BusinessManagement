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

    public IDataResult<IEnumerable<AccountTypeDto>> GetAll()
    {
        IEnumerable<AccountTypeDto> accountTypeDtos = _accountTypeDal.GetAll();
        if (!accountTypeDtos.Any())
            return new ErrorDataResult<IEnumerable<AccountTypeDto>>(Messages.AccountTypesNotFound);

        return new SuccessDataResult<IEnumerable<AccountTypeDto>>(accountTypeDtos, Messages.AccountTypesListed);
    }

    public IDataResult<AccountTypeDto> GetById(short id)
    {
        AccountTypeDto accountTypeDto = _accountTypeDal.GetById(id);
        if (accountTypeDto is null)
            return new ErrorDataResult<AccountTypeDto>(Messages.AccountTypeNotFound);

        return new SuccessDataResult<AccountTypeDto>(accountTypeDto, Messages.AccountTypeListedById);
    }

    public IDataResult<AccountTypeDto> GetByAccountTypeName(string accountTypeName)
    {
        AccountTypeDto accountTypeDto = _accountTypeDal.GetByAccountTypeName(accountTypeName);
        if (accountTypeDto is null)
            return new ErrorDataResult<AccountTypeDto>(Messages.AccountTypeNotFound);

        return new SuccessDataResult<AccountTypeDto>(accountTypeDto, Messages.AccountTypeListedByAccountTypeName);
    }

    public IDataResult<IEnumerable<AccountTypeDto>> GetByAccountTypeNames(AccountTypeNamesDto accountTypeNamesDto)
    {
        IEnumerable<AccountTypeDto> accountTypeDtos = _accountTypeDal.GetByAccountTypeNames(accountTypeNamesDto.AccountTypeNames);
        if (accountTypeDtos.Count() == 0)
            return new ErrorDataResult<IEnumerable<AccountTypeDto>>(Messages.AccountTypesNotFound);

        return new SuccessDataResult<IEnumerable<AccountTypeDto>>(accountTypeDtos, Messages.AccountTypesListedByAccountTypeNames);
    }
}

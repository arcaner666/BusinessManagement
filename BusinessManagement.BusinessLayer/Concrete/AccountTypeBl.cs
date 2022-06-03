using AutoMapper;
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
    private readonly IMapper _mapper;

    public AccountTypeBl(
        IAccountTypeDal accountTypeDal,
        IMapper mapper
    )
    {
        _accountTypeDal = accountTypeDal;
        _mapper = mapper;
    }

    public IDataResult<IEnumerable<AccountTypeDto>> GetAll()
    {
        IEnumerable<AccountType> accountTypes = _accountTypeDal.GetAll();
        if (!accountTypes.Any())
            return new ErrorDataResult<IEnumerable<AccountTypeDto>>(Messages.AccountTypesNotFound);

        var accountTypeDtos = _mapper.Map<IEnumerable<AccountTypeDto>>(accountTypes);

        return new SuccessDataResult<IEnumerable<AccountTypeDto>>(accountTypeDtos, Messages.AccountTypesListed);
    }

    public IDataResult<AccountTypeDto> GetById(short id)
    {
        AccountType accountType = _accountTypeDal.GetById(id);
        if (accountType is null)
            return new ErrorDataResult<AccountTypeDto>(Messages.AccountTypeNotFound);

        var accountTypeDto = _mapper.Map<AccountTypeDto>(accountType);

        return new SuccessDataResult<AccountTypeDto>(accountTypeDto, Messages.AccountTypeListedById);
    }

    public IDataResult<AccountTypeDto> GetByAccountTypeName(string accountTypeName)
    {
        AccountType accountType = _accountTypeDal.GetByAccountTypeName(accountTypeName);
        if (accountType is null)
            return new ErrorDataResult<AccountTypeDto>(Messages.AccountTypeNotFound);

        var accountTypeDto = _mapper.Map<AccountTypeDto>(accountType);

        return new SuccessDataResult<AccountTypeDto>(accountTypeDto, Messages.AccountTypeListedByAccountTypeName);
    }

    public IDataResult<IEnumerable<AccountTypeDto>> GetByAccountTypeNames(AccountTypeNamesDto accountTypeNamesDto)
    {
        IEnumerable<AccountType> accountTypes = _accountTypeDal.GetByAccountTypeNames(accountTypeNamesDto.AccountTypeNames);
        if (accountTypes.Count() == 0)
            return new ErrorDataResult<IEnumerable<AccountTypeDto>>(Messages.AccountTypesNotFound);

        var accountTypeDtos = _mapper.Map<IEnumerable<AccountTypeDto>>(accountTypes);

        return new SuccessDataResult<IEnumerable<AccountTypeDto>>(accountTypeDtos, Messages.AccountTypesListedByAccountTypeNames);
    }
}

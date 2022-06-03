using AutoMapper;
using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class AccountBl : IAccountBl
{
    private readonly IAccountDal _accountDal;
    private readonly IMapper _mapper;

    public AccountBl(
        IAccountDal accountDal,
        IMapper mapper
    )
    {
        _accountDal = accountDal;
        _mapper = mapper;
    }

    public IDataResult<AccountDto> Add(AccountDto accountDto)
    {
        Account searchedAccount = _accountDal.GetByBusinessIdAndAccountCode(accountDto.BusinessId, accountDto.AccountCode);
        if (searchedAccount is not null)
            return new ErrorDataResult<AccountDto>(Messages.AccountAlreadyExists);

        var addedAccount = _mapper.Map<Account>(accountDto);

        addedAccount.DebitBalance = 0;
        addedAccount.CreditBalance = 0;
        addedAccount.Balance = 0;
        addedAccount.CreatedAt = DateTimeOffset.Now;
        addedAccount.UpdatedAt = DateTimeOffset.Now;
        long id = _accountDal.Add(addedAccount);
        addedAccount.AccountId = id;

        var addedAccountDto = _mapper.Map<AccountDto>(addedAccount);

        return new SuccessDataResult<AccountDto>(addedAccountDto, Messages.AccountAdded);
    }

    public IResult Delete(long id)
    {
        var getAccountResult = GetById(id);
        if (!getAccountResult.Success)
            return getAccountResult;

        _accountDal.Delete(id);

        return new SuccessResult(Messages.AccountDeleted);
    }

    public IDataResult<AccountDto> GetById(long id)
    {
        Account account = _accountDal.GetById(id);
        if (account is null)
            return new ErrorDataResult<AccountDto>(Messages.AccountNotFound);

        var accountDto = _mapper.Map<AccountDto>(account);

        return new SuccessDataResult<AccountDto>(accountDto, Messages.AccountListedById);
    }

    public IResult Update(AccountDto accountDto)
    {
        Account account = _accountDal.GetById(accountDto.AccountId);
        if (account is null)
            return new ErrorDataResult<AccountDto>(Messages.AccountNotFound);

        account.AccountName = accountDto.AccountName;
        account.DebitBalance = accountDto.DebitBalance;
        account.CreditBalance = accountDto.CreditBalance;
        account.Balance = accountDto.Balance;
        account.Limit = accountDto.Limit;
        account.UpdatedAt = DateTimeOffset.Now;
        _accountDal.Update(account);

        return new SuccessResult(Messages.AccountUpdated);
    }
}

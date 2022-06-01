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

    public AccountBl(
        IAccountDal accountDal
    )
    {
        _accountDal = accountDal;
    }

    public IDataResult<AccountDto> Add(AccountDto accountDto)
    {
        AccountDto searchedAccountDto = _accountDal.GetByBusinessIdAndAccountCode(accountDto.BusinessId, accountDto.AccountCode);
        if (searchedAccountDto is not null)
        {
            return new ErrorDataResult<AccountDto>(Messages.AccountAlreadyExists);
        }

        accountDto.DebitBalance = 0;
        accountDto.CreditBalance = 0;
        accountDto.Balance = 0;
        accountDto.CreatedAt = DateTimeOffset.Now;
        accountDto.UpdatedAt = DateTimeOffset.Now;
        long id = _accountDal.Add(accountDto);
        accountDto.AccountId = id;

        return new SuccessDataResult<AccountDto>(accountDto, Messages.AccountAdded);
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
        AccountDto accountDto = _accountDal.GetById(id);
        if (accountDto is null)
            return new ErrorDataResult<AccountDto>(Messages.AccountNotFound);

        return new SuccessDataResult<AccountDto>(accountDto, Messages.AccountListedById);
    }

    public IResult Update(AccountDto accountDto)
    {
        var searchedAccountResult = GetById(accountDto.AccountId);
        if (!searchedAccountResult.Success)
            return searchedAccountResult;

        searchedAccountResult.Data.AccountName = accountDto.AccountName;
        searchedAccountResult.Data.DebitBalance = accountDto.DebitBalance;
        searchedAccountResult.Data.CreditBalance = accountDto.CreditBalance;
        searchedAccountResult.Data.Balance = accountDto.Balance;
        searchedAccountResult.Data.Limit = accountDto.Limit;
        searchedAccountResult.Data.UpdatedAt = DateTimeOffset.Now;
        _accountDal.Update(searchedAccountResult.Data);

        return new SuccessResult(Messages.AccountUpdated);
    }
}

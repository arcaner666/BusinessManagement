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
        Account searchedAccount = _accountDal.GetByBusinessIdAndAccountCode(accountDto.BusinessId, accountDto.AccountCode);
        if (searchedAccount is not null)
        {
            return new ErrorDataResult<AccountDto>(Messages.AccountAlreadyExists);
        }

        Account addedAccount = new()
        {
            BusinessId = accountDto.BusinessId,
            BranchId = accountDto.BranchId,
            AccountGroupId = accountDto.AccountGroupId,
            AccountTypeId = accountDto.AccountTypeId,
            AccountOrder = accountDto.AccountOrder,
            AccountName = accountDto.AccountName,
            AccountCode = accountDto.AccountCode,
            TaxOffice = accountDto.TaxOffice,
            TaxNumber = accountDto.TaxNumber,
            IdentityNumber = accountDto.IdentityNumber,
            DebitBalance = 0,
            CreditBalance = 0,
            Balance = 0,
            Limit = accountDto.Limit,
            StandartMaturity = accountDto.StandartMaturity,
            CreatedAt = DateTimeOffset.Now,
            UpdatedAt = DateTimeOffset.Now,
        };
        _accountDal.Add(addedAccount);

        AccountDto addedAccountDto = FillDto(addedAccount);

        return new SuccessDataResult<AccountDto>(addedAccountDto, Messages.AccountAdded);
    }

    public IResult Delete(long id)
    {
        var getAccountResult = GetById(id);
        if (getAccountResult is null)
            return getAccountResult;

        _accountDal.Delete(id);

        return new SuccessResult(Messages.AccountDeleted);
    }

    public IDataResult<AccountDto> GetById(long id)
    {
        Account searchedAccount = _accountDal.GetById(id);
        if (searchedAccount is null)
            return new ErrorDataResult<AccountDto>(Messages.AccountNotFound);

        AccountDto searchedAccountDto = FillDto(searchedAccount);

        return new SuccessDataResult<AccountDto>(searchedAccountDto, Messages.AccountListedById);
    }

    public IResult Update(AccountDto accountDto)
    {
        Account searchedAccount = _accountDal.GetById(accountDto.AccountId);
        if (searchedAccount is null)
            return new ErrorDataResult<AccountDto>(Messages.AccountNotFound);

        searchedAccount.AccountName = accountDto.AccountName;
        searchedAccount.TaxOffice = accountDto.TaxOffice;
        searchedAccount.TaxNumber = accountDto.TaxNumber;
        searchedAccount.IdentityNumber = accountDto.IdentityNumber;
        searchedAccount.DebitBalance = accountDto.DebitBalance;
        searchedAccount.CreditBalance = accountDto.CreditBalance;
        searchedAccount.Balance = accountDto.Balance;
        searchedAccount.Limit = accountDto.Limit;
        searchedAccount.StandartMaturity = accountDto.StandartMaturity;
        searchedAccount.UpdatedAt = DateTimeOffset.Now;
        _accountDal.Update(searchedAccount);

        return new SuccessResult(Messages.AccountUpdated);
    }

    private AccountDto FillDto(Account account)
    {
        AccountDto accountDto = new()
        {
            AccountId = account.AccountId,
            BusinessId = account.BusinessId,
            BranchId = account.BranchId,
            AccountGroupId = account.AccountGroupId,
            AccountTypeId = account.AccountTypeId,
            AccountOrder = account.AccountOrder,
            AccountName = account.AccountName,
            AccountCode = account.AccountCode,
            TaxOffice = account.TaxOffice,
            TaxNumber = account.TaxNumber,
            IdentityNumber = account.IdentityNumber,
            DebitBalance = account.DebitBalance,
            CreditBalance = account.CreditBalance,
            Balance = account.Balance,
            Limit = account.Limit,
            StandartMaturity = account.StandartMaturity,
            CreatedAt = account.CreatedAt,
            UpdatedAt = account.UpdatedAt,
        };

        return accountDto;
    }

    private List<AccountDto> FillDtos(List<Account> accounts)
    {
        List<AccountDto> accountDtos = accounts.Select(account => FillDto(account)).ToList();

        return accountDtos;
    }
}

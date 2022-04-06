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
            CurrencyId = accountDto.CurrencyId,
            AccountOrder = accountDto.AccountOrder,
            AccountName = accountDto.AccountName,
            AccountCode = accountDto.AccountCode,
            TaxOffice = "",
            TaxNumber = 0,
            IdentityNumber = 0,
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

    private AccountDto FillDto(Account account)
    {
        AccountDto accountDto = new()
        {
            AccountId = account.AccountId,
            BusinessId = account.BusinessId,
            BranchId = account.BranchId,
            AccountGroupId = account.AccountGroupId,
            CurrencyId = account.CurrencyId,
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

    private AccountExtDto FillExtDto(Account account)
    {
        AccountExtDto accountExtDto = new()
        {
            AccountId = account.AccountId,
            BusinessId = account.BusinessId,
            BranchId = account.BranchId,
            AccountGroupId = account.AccountGroupId,
            CurrencyId = account.CurrencyId,
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

            BranchName = account.Branch.BranchName,
            AccountGroupName = account.AccountGroup.AccountGroupName,
            CurrencyName = account.Currency.CurrencyName,
        };
        return accountExtDto;
    }

    private List<AccountExtDto> FillExtDtos(List<Account> accounts)
    {
        List<AccountExtDto> accountExtDtos = accounts.Select(account => FillExtDto(account)).ToList();

        return accountExtDtos;
    }
}

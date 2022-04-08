using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Aspects.Autofac.Transaction;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class AccountBl : IAccountBl
{
    private readonly IAccountDal _accountDal;
    private readonly IAccountGroupBl _accountGroupBl;
    private readonly IBranchBl _branchBl;
    private readonly ICurrencyBl _currencyBl;

    public AccountBl(
        IAccountDal accountDal,
        IAccountGroupBl accountGroupBl,
        IBranchBl branchBl,
        ICurrencyBl currencyBl
    )
    {
        _accountDal = accountDal;
        _accountGroupBl = accountGroupBl;
        _branchBl = branchBl;
        _currencyBl = currencyBl;
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

    public IResult AddExt(AccountExtDto accountExtDto)
    {
        // Doviz cinsi getirilir.
        var getCurrencyResult = _currencyBl.GetByCurrencyName("TL");
        if (!getCurrencyResult.Success)
        {
            return getCurrencyResult;
        }

        // Yeni bir cari hesap oluşturulur.
        AccountDto accountDto = new()
        {
            BusinessId = accountExtDto.BusinessId,
            BranchId = accountExtDto.BranchId,
            AccountGroupId = accountExtDto.AccountGroupId,
            CurrencyId = getCurrencyResult.Data.CurrencyId,
            AccountOrder = accountExtDto.AccountOrder,
            AccountName = accountExtDto.AccountName,
            AccountCode = accountExtDto.AccountCode,
            TaxOffice = accountExtDto.TaxOffice,
            TaxNumber = accountExtDto.TaxNumber,
            IdentityNumber = accountExtDto.IdentityNumber,
            Limit = accountExtDto.Limit,
            StandartMaturity = accountExtDto.StandartMaturity,
        };
        var addAccountResult = Add(accountDto);
        if (!addAccountResult.Success)
            return addAccountResult;

        return new SuccessResult(Messages.AccountExtAdded);
    }

    public IResult Delete(long id)
    {
        var getAccountResult = GetById(id);
        if (getAccountResult is null)
            return getAccountResult;

        _accountDal.Delete(id);

        return new SuccessResult(Messages.AccountDeleted);
    }

    public IResult DeleteExt(long id)
    {
        var deleteAccountResult = Delete(id);
        if (!deleteAccountResult.Success)
            return deleteAccountResult;

        return new SuccessResult(Messages.AccountExtDeleted);
    }

    public IDataResult<AccountCodeDto> GenerateAccountCode(int businessId, long branchId, string accountGroupCode)
    {
        // Kodun bir kısmını oluşturacak olan şube kodu getirilir.
        var getBranchResult = _branchBl.GetById(branchId);
        if (!getBranchResult.Success)
            return new ErrorDataResult<AccountCodeDto>(getBranchResult.Message);

        // Kodun bir kısmını oluşturacak olan hesap grubu kodu getirilir.
        var getAccountGroupResult = _accountGroupBl.GetByAccountGroupCode(accountGroupCode);
        if (!getAccountGroupResult.Success)
            return new ErrorDataResult<AccountCodeDto>(getAccountGroupResult.Message);

        // İlgili hesap grubundaki en son hesap sırası getirilir.
        int accountOrder = 1;
        Account getLastAccountOrderResult = _accountDal.GetMaxAccountOrderByBusinessIdAndBranchIdAndAccountGroupId(businessId, branchId, getAccountGroupResult.Data.AccountGroupId);
        if (getLastAccountOrderResult is not null)
        {
            accountOrder = getLastAccountOrderResult.AccountOrder + 1;
        }

        // Hesap kodu oluşturulur.
        AccountCodeDto accountCodeDto = new();
        accountCodeDto.AccountOrder = accountOrder;

        if (accountCodeDto.AccountOrder < 10)
            accountCodeDto.AccountCode = $"{getAccountGroupResult.Data.AccountGroupCode}{getBranchResult.Data.BranchCode}0000000{accountOrder}";
        else if (accountCodeDto.AccountOrder < 100)
            accountCodeDto.AccountCode = $"{getAccountGroupResult.Data.AccountGroupCode}{getBranchResult.Data.BranchCode}000000{accountOrder}";
        else if (accountCodeDto.AccountOrder < 1000)
            accountCodeDto.AccountCode = $"{getAccountGroupResult.Data.AccountGroupCode}{getBranchResult.Data.BranchCode}00000{accountOrder}";
        else if (accountCodeDto.AccountOrder < 10000)
            accountCodeDto.AccountCode = $"{getAccountGroupResult.Data.AccountGroupCode}{getBranchResult.Data.BranchCode}0000{accountOrder}";
        else if (accountCodeDto.AccountOrder < 100000)
            accountCodeDto.AccountCode = $"{getAccountGroupResult.Data.AccountGroupCode}{getBranchResult.Data.BranchCode}000{accountOrder}";
        else if (accountCodeDto.AccountOrder < 1000000)
            accountCodeDto.AccountCode = $"{getAccountGroupResult.Data.AccountGroupCode}{getBranchResult.Data.BranchCode}00{accountOrder}";
        else if (accountCodeDto.AccountOrder < 10000000)
            accountCodeDto.AccountCode = $"{getAccountGroupResult.Data.AccountGroupCode}{getBranchResult.Data.BranchCode}0{accountOrder}";
        else
            accountCodeDto.AccountCode = $"{getAccountGroupResult.Data.AccountGroupCode}{getBranchResult.Data.BranchCode}{accountOrder}";

        return new SuccessDataResult<AccountCodeDto>(accountCodeDto, Messages.AccountOrderAndCodeGenerated);
    }

    public IDataResult<AccountDto> GetById(long id)
    {
        Account searchedAccount = _accountDal.GetById(id);
        if (searchedAccount is null)
            return new ErrorDataResult<AccountDto>(Messages.AccountNotFound);

        AccountDto searchedAccountDto = FillDto(searchedAccount);

        return new SuccessDataResult<AccountDto>(searchedAccountDto, Messages.AccountListedById);
    }

    public IDataResult<AccountExtDto> GetExtById(long id)
    {
        Account searchedAccount = _accountDal.GetExtById(id);
        if (searchedAccount is null)
            return new ErrorDataResult<AccountExtDto>(Messages.AccountNotFound);

        AccountExtDto searchedAccountExtDto = FillExtDto(searchedAccount);

        return new SuccessDataResult<AccountExtDto>(searchedAccountExtDto, Messages.AccountExtListedById);
    }

    public IDataResult<List<AccountExtDto>> GetExtsByBusinessId(int businessId)
    {
        List<Account> searchedAccounts = _accountDal.GetExtsByBusinessId(businessId);
        if (searchedAccounts.Count == 0)
            return new ErrorDataResult<List<AccountExtDto>>(Messages.AccountsNotFound);

        List<AccountExtDto> searchedAccountExtDtos = FillExtDtos(searchedAccounts);

        return new SuccessDataResult<List<AccountExtDto>>(searchedAccountExtDtos, Messages.AccountExtsListedByBusinessId);
    }

    public IDataResult<List<AccountExtDto>> GetExtsByBusinessIdAndAccountGroupCode(AccountGetByAccountGroupCodesDto accountGetByAccountGroupCodesDto)
    {
        List<Account> searchedAccounts = _accountDal.GetExtsByBusinessIdAndAccountGroupCode(accountGetByAccountGroupCodesDto.BusinessId, accountGetByAccountGroupCodesDto.AccountGroupCodes);
        if (searchedAccounts.Count == 0)
            return new ErrorDataResult<List<AccountExtDto>>(Messages.AccountsNotFound);

        List<AccountExtDto> searchedAccountExtDtos = FillExtDtos(searchedAccounts);

        return new SuccessDataResult<List<AccountExtDto>>(searchedAccountExtDtos, Messages.AccountExtsListedByBusinessId);
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

    public IResult UpdateExt(AccountExtDto accountExtDto)
    {
        AccountDto accountDto = new()
        {
            AccountId = accountExtDto.AccountId,
            AccountName = accountExtDto.AccountName,
            TaxOffice = accountExtDto.TaxOffice,
            TaxNumber = accountExtDto.TaxNumber,
            IdentityNumber = accountExtDto.IdentityNumber,
            DebitBalance = accountExtDto.DebitBalance,
            CreditBalance = accountExtDto.CreditBalance,
            Balance = accountExtDto.Balance,
            Limit = accountExtDto.Limit,
            StandartMaturity = accountExtDto.StandartMaturity,
        };
        var updateAccountResult = Update(accountDto);
        if (!updateAccountResult.Success)
            return updateAccountResult;

        return new SuccessResult(Messages.AccountExtUpdated);
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

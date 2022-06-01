using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class AccountExtBl : IAccountExtBl
{
    private readonly IAccountBl _accountBl;
    private readonly IAccountDal _accountDal;
    private readonly IAccountGroupBl _accountGroupBl;
    private readonly IBranchBl _branchBl;

    public AccountExtBl(
        IAccountBl accountBl,
        IAccountDal accountDal,
        IAccountGroupBl accountGroupBl,
        IBranchBl branchBl
    )
    {
        _accountBl = accountBl;
        _accountDal = accountDal;
        _accountGroupBl = accountGroupBl;
        _branchBl = branchBl;
    }

    public IResult AddExt(AccountExtDto accountExtDto)
    {
        AccountDto accountDto = new()
        {
            BusinessId = accountExtDto.BusinessId,
            BranchId = accountExtDto.BranchId,
            AccountGroupId = accountExtDto.AccountGroupId,
            AccountTypeId = accountExtDto.AccountTypeId,
            AccountOrder = accountExtDto.AccountOrder,
            AccountName = accountExtDto.AccountName,
            AccountCode = accountExtDto.AccountCode,
            Limit = accountExtDto.Limit,
        };
        var addAccountResult = _accountBl.Add(accountDto);
        if (!addAccountResult.Success)
            return addAccountResult;

        return new SuccessResult(Messages.AccountExtAdded);
    }

    public IResult DeleteExt(long id)
    {
        var deleteAccountResult = _accountBl.Delete(id);
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
        int maxAccountOrder = _accountDal.GetMaxAccountOrderByBusinessIdAndBranchIdAndAccountGroupId(businessId, branchId, getAccountGroupResult.Data.AccountGroupId);
        if (maxAccountOrder != 0)
        {
            accountOrder = maxAccountOrder + 1;
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

    public IDataResult<AccountExtDto> GetExtById(long id)
    {
        AccountExtDto accountExtDto = _accountDal.GetExtById(id);
        if (accountExtDto is null)
            return new ErrorDataResult<AccountExtDto>(Messages.AccountNotFound);

        return new SuccessDataResult<AccountExtDto>(accountExtDto, Messages.AccountExtListedById);
    }

    public IDataResult<List<AccountExtDto>> GetExtsByBusinessId(int businessId)
    {
        List<AccountExtDto> accountExtDtos = _accountDal.GetExtsByBusinessId(businessId);
        if (accountExtDtos.Count == 0)
            return new ErrorDataResult<List<AccountExtDto>>(Messages.AccountsNotFound);

        return new SuccessDataResult<List<AccountExtDto>>(accountExtDtos, Messages.AccountExtsListedByBusinessId);
    }

    public IDataResult<List<AccountExtDto>> GetExtsByBusinessIdAndAccountGroupCodes(AccountGetByAccountGroupCodesDto accountGetByAccountGroupCodesDto)
    {
        List<AccountExtDto> accountExtDtos = _accountDal.GetExtsByBusinessIdAndAccountGroupCodes(accountGetByAccountGroupCodesDto.BusinessId, accountGetByAccountGroupCodesDto.AccountGroupCodes);
        if (accountExtDtos.Count == 0)
            return new ErrorDataResult<List<AccountExtDto>>(Messages.AccountsNotFound);

        return new SuccessDataResult<List<AccountExtDto>>(accountExtDtos, Messages.AccountExtsListedByBusinessId);
    }

    public IResult UpdateExt(AccountExtDto accountExtDto)
    {
        AccountDto accountDto = new()
        {
            AccountId = accountExtDto.AccountId,
            AccountName = accountExtDto.AccountName,
            DebitBalance = accountExtDto.DebitBalance,
            CreditBalance = accountExtDto.CreditBalance,
            Balance = accountExtDto.Balance,
            Limit = accountExtDto.Limit,
        };
        var updateAccountResult = _accountBl.Update(accountDto);
        if (!updateAccountResult.Success)
            return updateAccountResult;

        return new SuccessResult(Messages.AccountExtUpdated);
    }
}

using AutoMapper;
using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Concrete;

public record AccountAdvBl : IAccountAdvBl
{
    private readonly IAccountBl _accountBl;
    private readonly IAccountDal _accountDal;
    private readonly IAccountGroupBl _accountGroupBl;
    private readonly IBranchBl _branchBl;
    private readonly IMapper _mapper;

    public AccountAdvBl(
        IAccountBl accountBl,
        IAccountDal accountDal,
        IAccountGroupBl accountGroupBl,
        IBranchBl branchBl,
        IMapper mapper
    )
    {
        _accountBl = accountBl;
        _accountDal = accountDal;
        _accountGroupBl = accountGroupBl;
        _branchBl = branchBl;
        _mapper = mapper;
    }

    public IResult Add(AccountExtDto accountExtDto)
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

    public IResult Delete(long id)
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
            accountOrder = maxAccountOrder + 1;

        // Hesap kodu oluşturulur.
        string accountCode;
        if (accountOrder < 10)
            accountCode = $"{getAccountGroupResult.Data.AccountGroupCode}{getBranchResult.Data.BranchCode}0000000{accountOrder}";
        else if (accountOrder < 100)
            accountCode = $"{getAccountGroupResult.Data.AccountGroupCode}{getBranchResult.Data.BranchCode}000000{accountOrder}";
        else if (accountOrder < 1000)
            accountCode = $"{getAccountGroupResult.Data.AccountGroupCode}{getBranchResult.Data.BranchCode}00000{accountOrder}";
        else if (accountOrder < 10000)
            accountCode = $"{getAccountGroupResult.Data.AccountGroupCode}{getBranchResult.Data.BranchCode}0000{accountOrder}";
        else if (accountOrder < 100000)
            accountCode = $"{getAccountGroupResult.Data.AccountGroupCode}{getBranchResult.Data.BranchCode}000{accountOrder}";
        else if (accountOrder < 1000000)
            accountCode = $"{getAccountGroupResult.Data.AccountGroupCode}{getBranchResult.Data.BranchCode}00{accountOrder}";
        else if (accountOrder < 10000000)
            accountCode = $"{getAccountGroupResult.Data.AccountGroupCode}{getBranchResult.Data.BranchCode}0{accountOrder}";
        else
            accountCode = $"{getAccountGroupResult.Data.AccountGroupCode}{getBranchResult.Data.BranchCode}{accountOrder}";

        AccountCodeDto accountCodeDto = new()
        {
            AccountOrder = accountOrder,
            AccountCode = accountCode,
        };

        return new SuccessDataResult<AccountCodeDto>(accountCodeDto, Messages.AccountOrderAndCodeGenerated);
    }

    public IResult Update(AccountExtDto accountExtDto)
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

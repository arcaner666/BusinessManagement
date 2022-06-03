using AutoMapper;
using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Aspects.Autofac.Transaction;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class CashExtBl : ICashExtBl
{
    private readonly IAccountBl _accountBl;
    private readonly IAccountGroupBl _accountGroupBl;
    private readonly IAccountTypeBl _accountTypeBl;
    private readonly ICashBl _cashBl;
    private readonly ICashDal _cashDal;
    private readonly IMapper _mapper;

    public CashExtBl(
        IAccountBl accountBl,
        IAccountGroupBl accountGroupBl,
        IAccountTypeBl accountTypeBl,
        ICashBl cashBl,
        ICashDal cashDal,
        IMapper mapper
    )
    {
        _accountBl = accountBl;
        _accountGroupBl = accountGroupBl;
        _accountTypeBl = accountTypeBl;
        _cashBl = cashBl;
        _cashDal = cashDal;
        _mapper = mapper;
    }

    [TransactionScopeAspect]
    public IResult AddExt(CashExtDto cashExtDto)
    {
        // Kasanın hesap grubunun id'si getirilir.
        var getAccountGroupResult = _accountGroupBl.GetByAccountGroupCode("100");
        if (!getAccountGroupResult.Success)
            return getAccountGroupResult;

        // Kasanın hesap tipinin id'si getirilir.
        var getAccountTypeResult = _accountTypeBl.GetByAccountTypeName("Kasa");
        if (!getAccountTypeResult.Success)
            return getAccountTypeResult;

        // Yeni bir cari hesap oluşturulur.
        AccountDto accountDto = new()
        {
            BusinessId = cashExtDto.BusinessId,
            BranchId = cashExtDto.BranchId,
            AccountGroupId = getAccountGroupResult.Data.AccountGroupId,
            AccountTypeId = getAccountTypeResult.Data.AccountTypeId,
            AccountOrder = cashExtDto.AccountOrder,
            AccountName = cashExtDto.AccountName,
            AccountCode = cashExtDto.AccountCode,
            Limit = cashExtDto.Limit,
        };
        var addAccountResult = _accountBl.Add(accountDto);
        if (!addAccountResult.Success)
            return addAccountResult;

        // Yeni bir kasa eklenir.
        CashDto addedCashDto = new()
        {
            BusinessId = cashExtDto.BusinessId,
            BranchId = cashExtDto.BranchId,
            AccountId = addAccountResult.Data.AccountId,
            CurrencyId = cashExtDto.CurrencyId,
        };
        var addCashResult = _cashBl.Add(addedCashDto);
        if (!addCashResult.Success)
            return addCashResult;

        return new SuccessResult(Messages.CashExtAdded);
    }

    [TransactionScopeAspect]
    public IResult DeleteExt(long id)
    {
        var searchedCashResult = _cashBl.GetById(id);
        if (!searchedCashResult.Success)
            return searchedCashResult;

        var deleteCashResult = _cashBl.Delete(searchedCashResult.Data.CashId);
        if (!deleteCashResult.Success)
            return deleteCashResult;

        var deleteAccountResult = _accountBl.Delete(searchedCashResult.Data.AccountId);
        if (!deleteAccountResult.Success)
            return deleteAccountResult;

        return new SuccessResult(Messages.CashExtDeleted);
    }

    [TransactionScopeAspect]
    public IResult DeleteExtByAccountId(long accountId)
    {
        var searchedCashResult = _cashBl.GetByAccountId(accountId);
        if (!searchedCashResult.Success)
            return searchedCashResult;

        var deleteCashResult = _cashBl.Delete(searchedCashResult.Data.CashId);
        if (!deleteCashResult.Success)
            return deleteCashResult;

        var deleteAccountResult = _accountBl.Delete(searchedCashResult.Data.AccountId);
        if (!deleteAccountResult.Success)
            return deleteAccountResult;

        return new SuccessResult(Messages.CashExtDeletedByAccountId);
    }

    public IDataResult<CashExtDto> GetExtByAccountId(long accountId)
    {
        CashExtDto cashExtDto = _cashDal.GetExtByAccountId(accountId);
        if (cashExtDto is null)
            return new ErrorDataResult<CashExtDto>(Messages.CashNotFound);

        return new SuccessDataResult<CashExtDto>(cashExtDto, Messages.CashExtListedByAccountId);
    }

    public IDataResult<CashExtDto> GetExtById(long id)
    {
        CashExtDto cashExtDto = _cashDal.GetExtById(id);
        if (cashExtDto is null)
            return new ErrorDataResult<CashExtDto>(Messages.CashNotFound);

        return new SuccessDataResult<CashExtDto>(cashExtDto, Messages.CashExtListedById);
    }

    public IDataResult<IEnumerable<CashExtDto>> GetExtsByBusinessId(int businessId)
    {
        IEnumerable<CashExtDto> cashExtDtos = _cashDal.GetExtsByBusinessId(businessId);
        if (!cashExtDtos.Any())
            return new ErrorDataResult<IEnumerable<CashExtDto>>(Messages.CashNotFound);

        return new SuccessDataResult<IEnumerable<CashExtDto>>(cashExtDtos, Messages.CashExtsListedByBusinessId);
    }

    [TransactionScopeAspect]
    public IResult UpdateExt(CashExtDto cashExtDto)
    {
        // Mülk sahibinin cari hesabı güncellenir.
        AccountDto updatedAccountDto = new()
        {
            AccountId = cashExtDto.AccountId,
            AccountName = cashExtDto.AccountName,
            Limit = cashExtDto.Limit,
        };
        var updateAccountResult = _accountBl.Update(updatedAccountDto);
        if (!updateAccountResult.Success)
            return updateAccountResult;

        // Kasa güncellenir.
        CashDto updatedCashDto = new()
        {
            CashId = cashExtDto.CashId,
        };
        var updateCashResult = _cashBl.Update(updatedCashDto);
        if (!updateCashResult.Success)
            return updateCashResult;

        return new SuccessResult(Messages.CashExtUpdated);
    }
}

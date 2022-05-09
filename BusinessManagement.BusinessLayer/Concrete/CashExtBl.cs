using BusinessManagement.BusinessLayer.Abstract;
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

    public CashExtBl(
        IAccountBl accountBl,
        IAccountGroupBl accountGroupBl,
        IAccountTypeBl accountTypeBl,
        ICashBl cashBl,
        ICashDal cashDal
    )
    {
        _accountBl = accountBl;
        _accountGroupBl = accountGroupBl;
        _accountTypeBl = accountTypeBl;
        _cashBl = cashBl;
        _cashDal = cashDal;
    }

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

    public IDataResult<CashExtDto> GetExtById(long id)
    {
        Cash searchedCash = _cashDal.GetExtById(id);
        if (searchedCash is null)
            return new ErrorDataResult<CashExtDto>(Messages.CashNotFound);

        CashExtDto searchedCashExtDto = FillExtDto(searchedCash);

        return new SuccessDataResult<CashExtDto>(searchedCashExtDto, Messages.CashExtListedById);
    }

    public IDataResult<List<CashExtDto>> GetExtsByBusinessId(int businessId)
    {
        List<Cash> searchedCash = _cashDal.GetExtsByBusinessId(businessId);
        if (searchedCash.Count == 0)
            return new ErrorDataResult<List<CashExtDto>>(Messages.CashNotFound);

        List<CashExtDto> searchedCashExtDtos = FillExtDtos(searchedCash);

        return new SuccessDataResult<List<CashExtDto>>(searchedCashExtDtos, Messages.CashExtsListedByBusinessId);
    }

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

    private CashExtDto FillExtDto(Cash cash)
    {
        CashExtDto cashExtDto = new()
        {
            CashId = cash.CashId,
            BusinessId = cash.BusinessId,
            BranchId = cash.BranchId,
            AccountId = cash.AccountId,
            CurrencyId = cash.CurrencyId,
            CreatedAt = cash.CreatedAt,
            UpdatedAt = cash.UpdatedAt,
        };
        return cashExtDto;
    }

    private List<CashExtDto> FillExtDtos(List<Cash> cash)
    {
        List<CashExtDto> cashExtDtos = cash.Select(cash => FillExtDto(cash)).ToList();

        return cashExtDtos;
    }
}

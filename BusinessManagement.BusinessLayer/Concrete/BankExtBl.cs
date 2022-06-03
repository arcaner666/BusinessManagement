using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Aspects.Autofac.Transaction;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class BankExtBl : IBankExtBl
{
    private readonly IAccountBl _accountBl;
    private readonly IAccountGroupBl _accountGroupBl;
    private readonly IAccountTypeBl _accountTypeBl;
    private readonly IBankBl _bankBl;
    private readonly IBankDal _bankDal;
    private readonly IFullAddressBl _fullAddressBl;

    public BankExtBl(
        IAccountBl accountBl,
        IAccountGroupBl accountGroupBl,
        IAccountTypeBl accountTypeBl,
        IBankBl bankBl,
        IBankDal bankDal,
        IFullAddressBl fullAddressBl
    )
    {
        _accountBl = accountBl;
        _accountGroupBl = accountGroupBl;
        _accountTypeBl = accountTypeBl;
        _bankBl = bankBl;
        _bankDal = bankDal;
        _fullAddressBl = fullAddressBl;
    }

    [TransactionScopeAspect]
    public IResult AddExt(BankExtDto bankExtDto)
    {
        // Bankanın hesap grubunun id'si getirilir.
        var getAccountGroupResult = _accountGroupBl.GetByAccountGroupCode("102");
        if (!getAccountGroupResult.Success)
            return getAccountGroupResult;

        // Bankanın hesap tipinin id'si getirilir.
        var getAccountTypeResult = _accountTypeBl.GetByAccountTypeName("Banka");
        if (!getAccountTypeResult.Success)
            return getAccountTypeResult;

        // Yeni bir cari hesap oluşturulur.
        AccountDto accountDto = new()
        {
            BusinessId = bankExtDto.BusinessId,
            BranchId = bankExtDto.BranchId,
            AccountGroupId = getAccountGroupResult.Data.AccountGroupId,
            AccountTypeId = getAccountTypeResult.Data.AccountTypeId,
            AccountOrder = bankExtDto.AccountOrder,
            AccountName = bankExtDto.AccountName,
            AccountCode = bankExtDto.AccountCode,
            Limit = bankExtDto.Limit,
        };
        var addAccountResult = _accountBl.Add(accountDto);
        if (!addAccountResult.Success)
            return addAccountResult;

        // Yeni bir adres oluşturulur.
        FullAddressDto fullAddressDto = new()
        {
            CityId = bankExtDto.CityId,
            DistrictId = bankExtDto.DistrictId,
            AddressTitle = "Banka Adresi",
            PostalCode = 0,
            AddressText = bankExtDto.AddressText,
        };
        var addFullAddressResult = _fullAddressBl.Add(fullAddressDto);
        if (!addFullAddressResult.Success)
            return addFullAddressResult;

        // Yeni bir banka eklenir.
        BankDto addedBankDto = new()
        {
            BusinessId = bankExtDto.BusinessId,
            BranchId = bankExtDto.BranchId,
            AccountId = addAccountResult.Data.AccountId,
            FullAddressId = addFullAddressResult.Data.FullAddressId,
            CurrencyId = bankExtDto.CurrencyId,
            BankName = bankExtDto.BankName,
            BankBranchName = bankExtDto.BankBranchName,
            BankCode = bankExtDto.BankCode,
            BankBranchCode = bankExtDto.BankBranchCode,
            BankAccountCode = bankExtDto.BankAccountCode,
            Iban = bankExtDto.Iban,
            OfficerName = bankExtDto.OfficerName,
            StandartMaturity = bankExtDto.StandartMaturity,
        };
        var addBankResult = _bankBl.Add(addedBankDto);
        if (!addBankResult.Success)
            return addBankResult;

        return new SuccessResult(Messages.BankExtAdded);
    }

    [TransactionScopeAspect]
    public IResult DeleteExt(long id)
    {
        var searchedBankResult = _bankBl.GetById(id);
        if (!searchedBankResult.Success)
            return searchedBankResult;

        var deleteBankResult = _bankBl.Delete(searchedBankResult.Data.BankId);
        if (!deleteBankResult.Success)
            return deleteBankResult;

        var deleteAccountResult = _accountBl.Delete(searchedBankResult.Data.AccountId);
        if (!deleteAccountResult.Success)
            return deleteAccountResult;

        var deleteFullAddressResult = _fullAddressBl.Delete(searchedBankResult.Data.FullAddressId);
        if (!deleteFullAddressResult.Success)
            return deleteFullAddressResult;

        return new SuccessResult(Messages.BankExtDeleted);
    }

    [TransactionScopeAspect]
    public IResult DeleteExtByAccountId(long accountId)
    {
        var searchedBankResult = _bankBl.GetByAccountId(accountId);
        if (!searchedBankResult.Success)
            return searchedBankResult;

        var deleteBankResult = _bankBl.Delete(searchedBankResult.Data.BankId);
        if (!deleteBankResult.Success)
            return deleteBankResult;

        var deleteAccountResult = _accountBl.Delete(searchedBankResult.Data.AccountId);
        if (!deleteAccountResult.Success)
            return deleteAccountResult;

        var deleteFullAddressResult = _fullAddressBl.Delete(searchedBankResult.Data.FullAddressId);
        if (!deleteFullAddressResult.Success)
            return deleteFullAddressResult;
        return new SuccessResult(Messages.BankExtDeletedByAccountId);
    }

    public IDataResult<BankExtDto> GetExtByAccountId(long accountId)
    {
        BankExtDto bankExtDto = _bankDal.GetExtByAccountId(accountId);
        if (bankExtDto is null)
            return new ErrorDataResult<BankExtDto>(Messages.BankNotFound);

        return new SuccessDataResult<BankExtDto>(bankExtDto, Messages.BankExtListedByAccountId);
    }

    public IDataResult<BankExtDto> GetExtById(long id)
    {
        BankExtDto bankExtDto = _bankDal.GetExtById(id);
        if (bankExtDto is null)
            return new ErrorDataResult<BankExtDto>(Messages.BankNotFound);

        return new SuccessDataResult<BankExtDto>(bankExtDto, Messages.BankExtListedById);
    }

    public IDataResult<IEnumerable<BankExtDto>> GetExtsByBusinessId(int businessId)
    {
        IEnumerable<BankExtDto> bankExtDtos = _bankDal.GetExtsByBusinessId(businessId);
        if (!bankExtDtos.Any())
            return new ErrorDataResult<IEnumerable<BankExtDto>>(Messages.BankNotFound);

        return new SuccessDataResult<IEnumerable<BankExtDto>>(bankExtDtos, Messages.BankExtsListedByBusinessId);
    }

    [TransactionScopeAspect]
    public IResult UpdateExt(BankExtDto bankExtDto)
    {
        // Bankanın cari hesabı güncellenir.
        AccountDto updatedAccountDto = new()
        {
            AccountId = bankExtDto.AccountId,
            AccountName = bankExtDto.AccountName,
            Limit = bankExtDto.Limit,
         };
        var updateAccountResult = _accountBl.Update(updatedAccountDto);
        if (!updateAccountResult.Success)
            return updateAccountResult;

        // Banka güncellenir.
        BankDto updatedBankDto = new()
        {
            BankId = bankExtDto.BankId,
            BankName = bankExtDto.BankName,
            BankBranchName = bankExtDto.BankBranchName,
            BankCode = bankExtDto.BankCode,
            BankBranchCode = bankExtDto.BankBranchCode,
            BankAccountCode = bankExtDto.BankAccountCode,
            Iban = bankExtDto.Iban,
            OfficerName = bankExtDto.OfficerName,
            StandartMaturity = bankExtDto.StandartMaturity,
        };
        var updateBankResult = _bankBl.Update(updatedBankDto);
        if (!updateBankResult.Success)
            return updateBankResult;

        return new SuccessResult(Messages.BankExtUpdated);
    }
}

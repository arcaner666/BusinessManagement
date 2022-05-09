using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Aspects.Autofac.Transaction;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class HouseOwnerExtBl : IHouseOwnerExtBl
{
    private readonly IAccountBl _accountBl;
    private readonly IAccountGroupBl _accountGroupBl;
    private readonly IAccountTypeBl _accountTypeBl;
    private readonly IHouseOwnerBl _houseOwnerBl;
    private readonly IHouseOwnerDal _houseOwnerDal;

    public HouseOwnerExtBl(
        IAccountBl accountBl,
        IAccountGroupBl accountGroupBl,
        IAccountTypeBl accountTypeBl,
        IHouseOwnerBl houseOwnerBl,
        IHouseOwnerDal houseOwnerDal
    )
    {
        _accountBl = accountBl;
        _accountGroupBl = accountGroupBl;
        _accountTypeBl = accountTypeBl;
        _houseOwnerBl = houseOwnerBl;
        _houseOwnerDal = houseOwnerDal;
    }

    [TransactionScopeAspect]
    public IResult AddExt(HouseOwnerExtDto houseOwnerExtDto)
    {
        // Mülk sahibinin hesap grubunun id'si getirilir.
        var getAccountGroupResult = _accountGroupBl.GetByAccountGroupCode("120");
        if (!getAccountGroupResult.Success)
            return getAccountGroupResult;

        // Mülk sahibinin hesap tipinin id'si getirilir.
        var getAccountTypeResult = _accountTypeBl.GetByAccountTypeName("Mülk Sahibi");
        if (!getAccountTypeResult.Success)
            return getAccountTypeResult;

        // Yeni bir cari hesap oluşturulur.
        AccountDto accountDto = new()
        {
            BusinessId = houseOwnerExtDto.BusinessId,
            BranchId = houseOwnerExtDto.BranchId,
            AccountGroupId = getAccountGroupResult.Data.AccountGroupId,
            AccountTypeId = getAccountTypeResult.Data.AccountTypeId,
            AccountOrder = houseOwnerExtDto.AccountOrder,
            AccountName = houseOwnerExtDto.AccountName,
            AccountCode = houseOwnerExtDto.AccountCode,
            Limit = houseOwnerExtDto.Limit,
        };
        var addAccountResult = _accountBl.Add(accountDto);
        if (!addAccountResult.Success)
            return addAccountResult;

        // Yeni bir mülk sahibi eklenir.
        HouseOwnerDto addedHouseOwnerDto = new()
        {
            BusinessId = houseOwnerExtDto.BusinessId,
            BranchId = houseOwnerExtDto.BranchId,
            AccountId = addAccountResult.Data.AccountId,
            NameSurname = houseOwnerExtDto.NameSurname,
            Email = houseOwnerExtDto.Email,
            Phone = houseOwnerExtDto.Phone,
            DateOfBirth = houseOwnerExtDto.DateOfBirth,
            Gender = houseOwnerExtDto.Gender,
            Notes = houseOwnerExtDto.Notes,
            AvatarUrl = houseOwnerExtDto.AvatarUrl,
            TaxOffice = houseOwnerExtDto.TaxOffice,
            TaxNumber = houseOwnerExtDto.TaxNumber,
            IdentityNumber = houseOwnerExtDto.IdentityNumber,
            StandartMaturity = houseOwnerExtDto.StandartMaturity,
        };
        var addHouseOwnerResult = _houseOwnerBl.Add(addedHouseOwnerDto);
        if (!addHouseOwnerResult.Success)
            return addHouseOwnerResult;

        return new SuccessResult(Messages.HouseOwnerExtAdded);
    }

    [TransactionScopeAspect]
    public IResult DeleteExt(long id)
    {
        var searchedHouseOwnerResult = _houseOwnerBl.GetById(id);
        if (!searchedHouseOwnerResult.Success)
            return searchedHouseOwnerResult;

        var deleteHouseOwnerResult = _houseOwnerBl.Delete(searchedHouseOwnerResult.Data.HouseOwnerId);
        if (!deleteHouseOwnerResult.Success)
            return deleteHouseOwnerResult;

        var deleteAccountResult = _accountBl.Delete(searchedHouseOwnerResult.Data.AccountId);
        if (!deleteAccountResult.Success)
            return deleteAccountResult;

        return new SuccessResult(Messages.HouseOwnerExtDeleted);
    }

    public IDataResult<HouseOwnerExtDto> GetExtById(long id)
    {
        HouseOwner searchedHouseOwner = _houseOwnerDal.GetExtById(id);
        if (searchedHouseOwner is null)
            return new ErrorDataResult<HouseOwnerExtDto>(Messages.HouseOwnerNotFound);

        HouseOwnerExtDto searchedHouseOwnerExtDto = FillExtDto(searchedHouseOwner);

        return new SuccessDataResult<HouseOwnerExtDto>(searchedHouseOwnerExtDto, Messages.HouseOwnerExtListedById);
    }

    public IDataResult<List<HouseOwnerExtDto>> GetExtsByBusinessId(int businessId)
    {
        List<HouseOwner> searchedHouseOwners = _houseOwnerDal.GetExtsByBusinessId(businessId);
        if (searchedHouseOwners.Count == 0)
            return new ErrorDataResult<List<HouseOwnerExtDto>>(Messages.HouseOwnersNotFound);

        List<HouseOwnerExtDto> searchedHouseOwnerExtDtos = FillExtDtos(searchedHouseOwners);

        return new SuccessDataResult<List<HouseOwnerExtDto>>(searchedHouseOwnerExtDtos, Messages.HouseOwnerExtsListedByBusinessId);
    }

    [TransactionScopeAspect]
    public IResult UpdateExt(HouseOwnerExtDto houseOwnerExtDto)
    {
        // Mülk sahibinin cari hesabı güncellenir.
        AccountDto updatedAccountDto = new()
        {
            AccountId = houseOwnerExtDto.AccountId,
            AccountName = houseOwnerExtDto.AccountName,
            Limit = houseOwnerExtDto.Limit,
         };
        var updateAccountResult = _accountBl.Update(updatedAccountDto);
        if (!updateAccountResult.Success)
            return updateAccountResult;

        // Mülk sahibi güncellenir.
        HouseOwnerDto updatedHouseOwnerDto = new()
        {
            HouseOwnerId = houseOwnerExtDto.HouseOwnerId,
            NameSurname = houseOwnerExtDto.NameSurname,
            Email = houseOwnerExtDto.Email,
            DateOfBirth = houseOwnerExtDto.DateOfBirth,
            Gender = houseOwnerExtDto.Gender,
            Notes = houseOwnerExtDto.Notes,
            AvatarUrl = houseOwnerExtDto.AvatarUrl,
            TaxOffice = houseOwnerExtDto.TaxOffice,
            TaxNumber = houseOwnerExtDto.TaxNumber,
            IdentityNumber = houseOwnerExtDto.IdentityNumber,
            StandartMaturity = houseOwnerExtDto.StandartMaturity,
        };
        var updateHouseOwnerResult = _houseOwnerBl.Update(updatedHouseOwnerDto);
        if (!updateHouseOwnerResult.Success)
            return updateHouseOwnerResult;

        return new SuccessResult(Messages.HouseOwnerExtUpdated);
    }

    private HouseOwnerExtDto FillExtDto(HouseOwner houseOwner)
    {
        HouseOwnerExtDto houseOwnerExtDto = new()
        {
            HouseOwnerId = houseOwner.HouseOwnerId,
            BusinessId = houseOwner.BusinessId,
            BranchId = houseOwner.BranchId,
            AccountId = houseOwner.AccountId,
            NameSurname = houseOwner.NameSurname,
            Email = houseOwner.Email,
            Phone = houseOwner.Phone,
            DateOfBirth = houseOwner.DateOfBirth,
            Gender = houseOwner.Gender,
            Notes = houseOwner.Notes,
            AvatarUrl = houseOwner.AvatarUrl,
            TaxOffice = houseOwner.TaxOffice,
            TaxNumber = houseOwner.TaxNumber,
            IdentityNumber = houseOwner.IdentityNumber,
            StandartMaturity = houseOwner.StandartMaturity,
            CreatedAt = houseOwner.CreatedAt,
            UpdatedAt = houseOwner.UpdatedAt,

            // Extended With Account
            AccountGroupId = houseOwner.Account.AccountGroupId,
            AccountOrder = houseOwner.Account.AccountOrder,
            AccountName = houseOwner.Account.AccountName,
            AccountCode = houseOwner.Account.AccountCode,
            Limit = houseOwner.Account.Limit,
        };
        return houseOwnerExtDto;
    }

    private List<HouseOwnerExtDto> FillExtDtos(List<HouseOwner> houseOwners)
    {
        List<HouseOwnerExtDto> houseOwnerExtDtos = houseOwners.Select(houseOwner => FillExtDto(houseOwner)).ToList();

        return houseOwnerExtDtos;
    }
}

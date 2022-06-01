using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class HouseOwnerBl : IHouseOwnerBl
{
    private readonly IHouseOwnerDal _houseOwnerDal;

    public HouseOwnerBl(
        IHouseOwnerDal houseOwnerDal
    )
    {
        _houseOwnerDal = houseOwnerDal;
    }

    public IDataResult<HouseOwnerDto> Add(HouseOwnerDto houseOwnerDto)
    {
        HouseOwnerDto searchedHouseOwnerDto = _houseOwnerDal.GetByBusinessIdAndAccountId(houseOwnerDto.BusinessId, houseOwnerDto.AccountId);
        if (searchedHouseOwnerDto is not null)
        {
            return new ErrorDataResult<HouseOwnerDto>(Messages.HouseOwnerAlreadyExists);
        }

        houseOwnerDto.CreatedAt = DateTimeOffset.Now;
        houseOwnerDto.UpdatedAt = DateTimeOffset.Now;
        long id = _houseOwnerDal.Add(houseOwnerDto);
        houseOwnerDto.HouseOwnerId = id;

        return new SuccessDataResult<HouseOwnerDto>(houseOwnerDto, Messages.HouseOwnerAdded);
    }

    public IResult Delete(long id)
    {
        var getHouseOwnerResult = GetById(id);
        if (getHouseOwnerResult is null)
            return getHouseOwnerResult;

        _houseOwnerDal.Delete(id);

        return new SuccessResult(Messages.HouseOwnerDeleted);
    }

    public IDataResult<HouseOwnerDto> GetByAccountId(long accountId)
    {
        HouseOwnerDto houseOwnerDto = _houseOwnerDal.GetByAccountId(accountId);
        if (houseOwnerDto is null)
            return new ErrorDataResult<HouseOwnerDto>(Messages.HouseOwnerNotFound);

        return new SuccessDataResult<HouseOwnerDto>(houseOwnerDto, Messages.HouseOwnerListedByAccountId);
    }

    public IDataResult<List<HouseOwnerDto>> GetByBusinessId(int businessId)
    {
        List<HouseOwnerDto> houseOwnerDtos = _houseOwnerDal.GetByBusinessId(businessId);
        if (houseOwnerDtos.Count == 0)
            return new ErrorDataResult<List<HouseOwnerDto>>(Messages.HouseOwnersNotFound);

        return new SuccessDataResult<List<HouseOwnerDto>>(houseOwnerDtos, Messages.HouseOwnersListedByBusinessId);
    }

    public IDataResult<HouseOwnerDto> GetById(long id)
    {
        HouseOwnerDto houseOwnerDto = _houseOwnerDal.GetById(id);
        if (houseOwnerDto is null)
            return new ErrorDataResult<HouseOwnerDto>(Messages.HouseOwnerNotFound);

        return new SuccessDataResult<HouseOwnerDto>(houseOwnerDto, Messages.HouseOwnerListedById);
    }

    public IResult Update(HouseOwnerDto houseOwnerDto)
    {
        var searchedHouseOwnerResult = GetById(houseOwnerDto.HouseOwnerId);
        if (!searchedHouseOwnerResult.Success)
            return searchedHouseOwnerResult;

        searchedHouseOwnerResult.Data.NameSurname = houseOwnerDto.NameSurname;
        searchedHouseOwnerResult.Data.Email = houseOwnerDto.Email;
        searchedHouseOwnerResult.Data.DateOfBirth = houseOwnerDto.DateOfBirth;
        searchedHouseOwnerResult.Data.Gender = houseOwnerDto.Gender;
        searchedHouseOwnerResult.Data.Notes = houseOwnerDto.Notes;
        searchedHouseOwnerResult.Data.AvatarUrl = houseOwnerDto.AvatarUrl;
        searchedHouseOwnerResult.Data.TaxOffice = houseOwnerDto.TaxOffice;
        searchedHouseOwnerResult.Data.TaxNumber = houseOwnerDto.TaxNumber;
        searchedHouseOwnerResult.Data.IdentityNumber = houseOwnerDto.IdentityNumber;
        searchedHouseOwnerResult.Data.StandartMaturity = houseOwnerDto.StandartMaturity;
        searchedHouseOwnerResult.Data.UpdatedAt = DateTimeOffset.Now;
        _houseOwnerDal.Update(searchedHouseOwnerResult.Data);

        return new SuccessResult(Messages.HouseOwnerUpdated);
    }
}

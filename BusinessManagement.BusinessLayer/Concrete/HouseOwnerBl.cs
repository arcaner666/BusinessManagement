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
        HouseOwner searchedHouseOwner = _houseOwnerDal.GetByBusinessIdAndAccountId(houseOwnerDto.BusinessId, houseOwnerDto.AccountId);
        if (searchedHouseOwner is not null)
        {
            return new ErrorDataResult<HouseOwnerDto>(Messages.HouseOwnerAlreadyExists);
        }

        HouseOwner addedHouseOwner = new()
        {
            BusinessId = houseOwnerDto.BusinessId,
            BranchId = houseOwnerDto.BranchId,
            AccountId = houseOwnerDto.AccountId,
            NameSurname = houseOwnerDto.NameSurname,
            Email = houseOwnerDto.Email,
            Phone = houseOwnerDto.Phone,
            DateOfBirth = houseOwnerDto.DateOfBirth,
            Gender = houseOwnerDto.Gender,
            Notes = houseOwnerDto.Notes,
            AvatarUrl = houseOwnerDto.AvatarUrl,
            CreatedAt = DateTimeOffset.Now,
            UpdatedAt = DateTimeOffset.Now,
        };
        _houseOwnerDal.Add(addedHouseOwner);

        HouseOwnerDto addedHouseOwnerDto = FillDto(addedHouseOwner);

        return new SuccessDataResult<HouseOwnerDto>(addedHouseOwnerDto, Messages.HouseOwnerAdded);
    }

    public IResult Delete(long id)
    {
        var getHouseOwnerResult = GetById(id);
        if (getHouseOwnerResult is null)
            return getHouseOwnerResult;

        _houseOwnerDal.Delete(id);

        return new SuccessResult(Messages.HouseOwnerDeleted);
    }

    public IDataResult<List<HouseOwnerDto>> GetByBusinessId(int businessId)
    {
        List<HouseOwner> searchedHouseOwners = _houseOwnerDal.GetByBusinessId(businessId);
        if (searchedHouseOwners.Count == 0)
            return new ErrorDataResult<List<HouseOwnerDto>>(Messages.HouseOwnersNotFound);

        List<HouseOwnerDto> searchedHouseOwnerDtos = FillDtos(searchedHouseOwners);

        return new SuccessDataResult<List<HouseOwnerDto>>(searchedHouseOwnerDtos, Messages.HouseOwnersListedByBusinessId);
    }

    public IDataResult<HouseOwnerDto> GetById(long id)
    {
        HouseOwner searchedHouseOwner = _houseOwnerDal.GetById(id);
        if (searchedHouseOwner is null)
            return new ErrorDataResult<HouseOwnerDto>(Messages.HouseOwnerNotFound);

        HouseOwnerDto searchedHouseOwnerDto = FillDto(searchedHouseOwner);

        return new SuccessDataResult<HouseOwnerDto>(searchedHouseOwnerDto, Messages.HouseOwnerListedById);
    }

    public IResult Update(HouseOwnerDto houseOwnerDto)
    {
        HouseOwner searchedHouseOwner = _houseOwnerDal.GetById(houseOwnerDto.HouseOwnerId);
        if (searchedHouseOwner is null)
            return new ErrorDataResult<AccountDto>(Messages.HouseOwnerNotFound);

        searchedHouseOwner.NameSurname = houseOwnerDto.NameSurname;
        searchedHouseOwner.Email = houseOwnerDto.Email;
        searchedHouseOwner.DateOfBirth = houseOwnerDto.DateOfBirth;
        searchedHouseOwner.Gender = houseOwnerDto.Gender;
        searchedHouseOwner.Notes = houseOwnerDto.Notes;
        searchedHouseOwner.AvatarUrl = houseOwnerDto.AvatarUrl;
        searchedHouseOwner.UpdatedAt = DateTimeOffset.Now;
        _houseOwnerDal.Update(searchedHouseOwner);

        return new SuccessResult(Messages.HouseOwnerUpdated);
    }

    private HouseOwnerDto FillDto(HouseOwner houseOwner)
    {
        HouseOwnerDto houseOwnerDto = new()
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
            CreatedAt = houseOwner.CreatedAt,
            UpdatedAt = houseOwner.UpdatedAt,
        };

        return houseOwnerDto;
    }

    private List<HouseOwnerDto> FillDtos(List<HouseOwner> houseOwners)
    {
        List<HouseOwnerDto> houseOwnerDtos = houseOwners.Select(houseOwner => FillDto(houseOwner)).ToList();

        return houseOwnerDtos;
    }
}

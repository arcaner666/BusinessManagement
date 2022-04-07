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

    public IDataResult<List<HouseOwnerDto>> GetByBusinessId(int businessId)
    {
        List<HouseOwner> searchedHouseOwners = _houseOwnerDal.GetByBusinessId(businessId);
        if (searchedHouseOwners.Count == 0)
            return new ErrorDataResult<List<HouseOwnerDto>>(Messages.HouseOwnersNotFound);

        List<HouseOwnerDto> searchedHouseOwnerDtos = FillDtos(searchedHouseOwners);

        return new SuccessDataResult<List<HouseOwnerDto>>(searchedHouseOwnerDtos, Messages.HouseOwnersListedByBusinessId);
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
            CreatedAt = houseOwner.CreatedAt,
            UpdatedAt = houseOwner.UpdatedAt,
        };
        return houseOwnerExtDto;
    }

    private List<HouseOwnerExtDto> FillExtDtos(List<HouseOwner> houseOwners)
    {
        List<HouseOwnerExtDto> houseOwnerExtDtos = houseOwners.Select(houseOwner => FillExtDto(houseOwner)).ToList();

        return houseOwnerExtDtos;
    }
}

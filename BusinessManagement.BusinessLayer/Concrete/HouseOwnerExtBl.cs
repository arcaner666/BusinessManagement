using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class HouseOwnerExtBl : IHouseOwnerExtBl
{
    private readonly IHouseOwnerBl _houseOwnerBl;
    private readonly IHouseOwnerDal _houseOwnerDal;

    public HouseOwnerExtBl(
        IHouseOwnerBl houseOwnerBl,
        IHouseOwnerDal houseOwnerDal
    )
    {
        _houseOwnerBl = houseOwnerBl;
        _houseOwnerDal = houseOwnerDal;
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

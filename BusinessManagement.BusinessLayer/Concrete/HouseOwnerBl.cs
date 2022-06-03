using AutoMapper;
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
    private readonly IMapper _mapper;

    public HouseOwnerBl(
        IHouseOwnerDal houseOwnerDal,
        IMapper mapper
    )
    {
        _houseOwnerDal = houseOwnerDal;
        _mapper = mapper;
    }

    public IDataResult<HouseOwnerDto> Add(HouseOwnerDto houseOwnerDto)
    {
        HouseOwner searchedHouseOwner = _houseOwnerDal.GetByBusinessIdAndAccountId(houseOwnerDto.BusinessId, houseOwnerDto.AccountId);
        if (searchedHouseOwner is not null)
            return new ErrorDataResult<HouseOwnerDto>(Messages.HouseOwnerAlreadyExists);

        var addedHouseOwner = _mapper.Map<HouseOwner>(houseOwnerDto);

        addedHouseOwner.CreatedAt = DateTimeOffset.Now;
        addedHouseOwner.UpdatedAt = DateTimeOffset.Now;
        long id = _houseOwnerDal.Add(addedHouseOwner);
        addedHouseOwner.HouseOwnerId = id;

        var addedHouseOwnerDto = _mapper.Map<HouseOwnerDto>(addedHouseOwner);

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

    public IDataResult<HouseOwnerDto> GetByAccountId(long accountId)
    {
        HouseOwner houseOwner = _houseOwnerDal.GetByAccountId(accountId);
        if (houseOwner is null)
            return new ErrorDataResult<HouseOwnerDto>(Messages.HouseOwnerNotFound);

        var houseOwnerDto = _mapper.Map<HouseOwnerDto>(houseOwner);

        return new SuccessDataResult<HouseOwnerDto>(houseOwnerDto, Messages.HouseOwnerListedByAccountId);
    }

    public IDataResult<IEnumerable<HouseOwnerDto>> GetByBusinessId(int businessId)
    {
        IEnumerable<HouseOwner> houseOwners = _houseOwnerDal.GetByBusinessId(businessId);
        if (!houseOwners.Any())
            return new ErrorDataResult<IEnumerable<HouseOwnerDto>>(Messages.HouseOwnersNotFound);

        var houseOwnerDtos = _mapper.Map<IEnumerable<HouseOwnerDto>>(houseOwners);

        return new SuccessDataResult<IEnumerable<HouseOwnerDto>>(houseOwnerDtos, Messages.HouseOwnersListedByBusinessId);
    }

    public IDataResult<HouseOwnerDto> GetById(long id)
    {
        HouseOwner houseOwner = _houseOwnerDal.GetById(id);
        if (houseOwner is null)
            return new ErrorDataResult<HouseOwnerDto>(Messages.HouseOwnerNotFound);

        var houseOwnerDto = _mapper.Map<HouseOwnerDto>(houseOwner);

        return new SuccessDataResult<HouseOwnerDto>(houseOwnerDto, Messages.HouseOwnerListedById);
    }

    public IResult Update(HouseOwnerDto houseOwnerDto)
    {
        HouseOwner houseOwner = _houseOwnerDal.GetById(houseOwnerDto.HouseOwnerId);
        if (houseOwner is null)
            return new ErrorDataResult<HouseOwnerDto>(Messages.HouseOwnerNotFound);

        houseOwner.NameSurname = houseOwnerDto.NameSurname;
        houseOwner.Email = houseOwnerDto.Email;
        houseOwner.DateOfBirth = houseOwnerDto.DateOfBirth;
        houseOwner.Gender = houseOwnerDto.Gender;
        houseOwner.Notes = houseOwnerDto.Notes;
        houseOwner.AvatarUrl = houseOwnerDto.AvatarUrl;
        houseOwner.TaxOffice = houseOwnerDto.TaxOffice;
        houseOwner.TaxNumber = houseOwnerDto.TaxNumber;
        houseOwner.IdentityNumber = houseOwnerDto.IdentityNumber;
        houseOwner.StandartMaturity = houseOwnerDto.StandartMaturity;
        houseOwner.UpdatedAt = DateTimeOffset.Now;
        _houseOwnerDal.Update(houseOwner);

        return new SuccessResult(Messages.HouseOwnerUpdated);
    }
}

using AutoMapper;
using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
using BusinessManagement.Entities.ExtendedDatabaseModels;

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

    public IDataResult<List<HouseOwnerDto>> GetByBusinessId(int businessId)
    {
        List<HouseOwner> houseOwners = _houseOwnerDal.GetByBusinessId(businessId);
        if (!houseOwners.Any())
            return new ErrorDataResult<List<HouseOwnerDto>>(Messages.HouseOwnersNotFound);

        var houseOwnerDtos = _mapper.Map<List<HouseOwnerDto>>(houseOwners);

        return new SuccessDataResult<List<HouseOwnerDto>>(houseOwnerDtos, Messages.HouseOwnersListedByBusinessId);
    }

    public IDataResult<HouseOwnerDto> GetById(long id)
    {
        HouseOwner houseOwner = _houseOwnerDal.GetById(id);
        if (houseOwner is null)
            return new ErrorDataResult<HouseOwnerDto>(Messages.HouseOwnerNotFound);

        var houseOwnerDto = _mapper.Map<HouseOwnerDto>(houseOwner);

        return new SuccessDataResult<HouseOwnerDto>(houseOwnerDto, Messages.HouseOwnerListedById);
    }

    public IDataResult<HouseOwnerExtDto> GetExtByAccountId(long accountId)
    {
        HouseOwnerExt houseOwnerExt = _houseOwnerDal.GetExtByAccountId(accountId);
        if (houseOwnerExt is null)
            return new ErrorDataResult<HouseOwnerExtDto>(Messages.HouseOwnerNotFound);

        var houseOwnerExtDto = _mapper.Map<HouseOwnerExtDto>(houseOwnerExt);

        return new SuccessDataResult<HouseOwnerExtDto>(houseOwnerExtDto, Messages.HouseOwnerExtListedByAccountId);
    }

    public IDataResult<HouseOwnerExtDto> GetExtById(long id)
    {
        HouseOwnerExt houseOwnerExt = _houseOwnerDal.GetExtById(id);
        if (houseOwnerExt is null)
            return new ErrorDataResult<HouseOwnerExtDto>(Messages.HouseOwnerNotFound);

        var houseOwnerExtDto = _mapper.Map<HouseOwnerExtDto>(houseOwnerExt);

        return new SuccessDataResult<HouseOwnerExtDto>(houseOwnerExtDto, Messages.HouseOwnerExtListedById);
    }

    public IDataResult<List<HouseOwnerExtDto>> GetExtsByBusinessId(int businessId)
    {
        List<HouseOwnerExt> houseOwnerExts = _houseOwnerDal.GetExtsByBusinessId(businessId);
        if (!houseOwnerExts.Any())
            return new ErrorDataResult<List<HouseOwnerExtDto>>(Messages.HouseOwnersNotFound);

        var houseOwnerExtDtos = _mapper.Map<List<HouseOwnerExtDto>>(houseOwnerExts);

        return new SuccessDataResult<List<HouseOwnerExtDto>>(houseOwnerExtDtos, Messages.HouseOwnerExtsListedByBusinessId);
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

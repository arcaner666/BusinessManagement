using AutoMapper;
using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class FullAddressBl : IFullAddressBl
{
    private readonly IFullAddressDal _fullAddressDal;
    private readonly IMapper _mapper;

    public FullAddressBl(
        IFullAddressDal fullAddressDal,
        IMapper mapper
    )
    {
        _fullAddressDal = fullAddressDal;
        _mapper = mapper;
    }

    public IDataResult<FullAddressDto> Add(FullAddressDto fullAddressDto)
    {
        FullAddress searchedFullAddress = _fullAddressDal.GetByAddressText(fullAddressDto.AddressText);
        if (searchedFullAddress is not null)
            return new ErrorDataResult<FullAddressDto>(Messages.FullAddressAlreadyExists);

        var addedFullAddress = _mapper.Map<FullAddress>(fullAddressDto);

        addedFullAddress.CreatedAt = DateTimeOffset.Now;
        addedFullAddress.UpdatedAt = DateTimeOffset.Now;
        long id = _fullAddressDal.Add(addedFullAddress);
        addedFullAddress.FullAddressId = id;

        var addedFullAddressDto = _mapper.Map<FullAddressDto>(addedFullAddress);

        return new SuccessDataResult<FullAddressDto>(addedFullAddressDto, Messages.FullAddressAdded);
    }

    public IResult Delete(long id)
    {
        var getFullAddressResult = GetById(id);
        if (!getFullAddressResult.Success)
            return getFullAddressResult;

        _fullAddressDal.Delete(id);

        return new SuccessResult(Messages.FullAddressDeleted);
    }

    public IDataResult<FullAddressDto> GetById(long id)
    {
        FullAddress fullAddress = _fullAddressDal.GetById(id);
        if (fullAddress is null)
            return new ErrorDataResult<FullAddressDto>(Messages.FullAddressNotFound);

        var fullAddressDto = _mapper.Map<FullAddressDto>(fullAddress);

        return new SuccessDataResult<FullAddressDto>(fullAddressDto, Messages.FullAddressListedById);
    }

    public IResult Update(FullAddressDto fullAddressDto)
    {
        FullAddress fullAddress = _fullAddressDal.GetById(fullAddressDto.FullAddressId);
        if (fullAddress is null)
            return new ErrorDataResult<FullAddressDto>(Messages.FullAddressNotFound);

        fullAddress.CityId = fullAddressDto.CityId;
        fullAddress.DistrictId = fullAddressDto.DistrictId;
        fullAddress.AddressTitle = fullAddressDto.AddressTitle;
        fullAddress.PostalCode = fullAddressDto.PostalCode;
        fullAddress.AddressText = fullAddressDto.AddressText;
        fullAddress.UpdatedAt = DateTimeOffset.Now;
        _fullAddressDal.Update(fullAddress);

        return new SuccessResult(Messages.FullAddressUpdated);
    }
}

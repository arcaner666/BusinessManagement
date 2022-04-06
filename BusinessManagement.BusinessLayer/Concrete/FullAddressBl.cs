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

    public FullAddressBl(
        IFullAddressDal fullAddressDal
    )
    {
        _fullAddressDal = fullAddressDal;
    }

    public IDataResult<FullAddressDto> Add(FullAddressDto fullAddressDto)
    {
        FullAddress searchedFullAddress = _fullAddressDal.GetByAddressText(fullAddressDto.AddressText);
        if (searchedFullAddress is not null)
            return new ErrorDataResult<FullAddressDto>(Messages.FullAddressAlreadyExists);

        FullAddress addedFullAddress = new()
        {
            CityId = fullAddressDto.CityId,
            DistrictId = fullAddressDto.DistrictId,
            AddressTitle = fullAddressDto.AddressTitle,
            PostalCode = fullAddressDto.PostalCode,
            AddressText = fullAddressDto.AddressText,
            CreatedAt = DateTimeOffset.Now,
            UpdatedAt = DateTimeOffset.Now,
        };
        _fullAddressDal.Add(addedFullAddress);

        FullAddressDto addedFullAddressDto = FillDto(addedFullAddress);

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
        FullAddress searchedFullAddress = _fullAddressDal.GetById(id);
        if (searchedFullAddress is null)
            return new ErrorDataResult<FullAddressDto>(Messages.FullAddressNotFound);

        FullAddressDto searchedFullAddressDto = FillDto(searchedFullAddress);

        return new SuccessDataResult<FullAddressDto>(searchedFullAddressDto, Messages.FullAddressListedById);
    }

    public IResult Update(FullAddressDto fullAddressDto)
    {
        FullAddress searchedFullAddress = _fullAddressDal.GetById(fullAddressDto.FullAddressId);
        if (searchedFullAddress is null)
            return new ErrorDataResult<FullAddressDto>(Messages.FullAddressNotFound);

        searchedFullAddress.CityId = fullAddressDto.CityId;
        searchedFullAddress.DistrictId = fullAddressDto.DistrictId;
        searchedFullAddress.AddressTitle = fullAddressDto.AddressTitle;
        searchedFullAddress.PostalCode = fullAddressDto.PostalCode;
        searchedFullAddress.AddressText = fullAddressDto.AddressText;
        searchedFullAddress.UpdatedAt = DateTimeOffset.Now;

        _fullAddressDal.Update(searchedFullAddress);

        return new SuccessResult(Messages.FullAddressUpdated);
    }

    private FullAddressDto FillDto(FullAddress fullAddress)
    {
        FullAddressDto fullAddressDto = new()
        {
            FullAddressId = fullAddress.FullAddressId,
            CityId = fullAddress.CityId,
            DistrictId = fullAddress.DistrictId,
            AddressTitle = fullAddress.AddressTitle,
            PostalCode = fullAddress.PostalCode,
            AddressText = fullAddress.AddressText,
            CreatedAt = fullAddress.CreatedAt,
            UpdatedAt = fullAddress.UpdatedAt,
        };

        return fullAddressDto;
    }

    private List<FullAddressDto> FillDtos(List<FullAddress> fullAddresses)
    {
        List<FullAddressDto> fullAddressDtos = fullAddresses.Select(fullAddress => FillDto(fullAddress)).ToList();

        return fullAddressDtos;
    }
}

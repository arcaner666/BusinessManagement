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
        FullAddressDto searchedFullAddressDto = _fullAddressDal.GetByAddressText(fullAddressDto.AddressText);
        if (searchedFullAddressDto is not null)
            return new ErrorDataResult<FullAddressDto>(Messages.FullAddressAlreadyExists);

        fullAddressDto.CreatedAt = DateTimeOffset.Now;
        fullAddressDto.UpdatedAt = DateTimeOffset.Now;
        long id = _fullAddressDal.Add(fullAddressDto);
        fullAddressDto.FullAddressId = id;

        return new SuccessDataResult<FullAddressDto>(fullAddressDto, Messages.FullAddressAdded);
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
        FullAddressDto fullAddressDto = _fullAddressDal.GetById(id);
        if (fullAddressDto is null)
            return new ErrorDataResult<FullAddressDto>(Messages.FullAddressNotFound);

        return new SuccessDataResult<FullAddressDto>(fullAddressDto, Messages.FullAddressListedById);
    }

    public IResult Update(FullAddressDto fullAddressDto)
    {
        var searchedFullAddressResult = GetById(fullAddressDto.FullAddressId);
        if (!searchedFullAddressResult.Success)
            return searchedFullAddressResult;

        searchedFullAddressResult.Data.CityId = fullAddressDto.CityId;
        searchedFullAddressResult.Data.DistrictId = fullAddressDto.DistrictId;
        searchedFullAddressResult.Data.AddressTitle = fullAddressDto.AddressTitle;
        searchedFullAddressResult.Data.PostalCode = fullAddressDto.PostalCode;
        searchedFullAddressResult.Data.AddressText = fullAddressDto.AddressText;
        searchedFullAddressResult.Data.UpdatedAt = DateTimeOffset.Now;
        _fullAddressDal.Update(searchedFullAddressResult.Data);

        return new SuccessResult(Messages.FullAddressUpdated);
    }
}

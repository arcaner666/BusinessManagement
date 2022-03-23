using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete
{
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
            FullAddress getFullAddress = _fullAddressDal.GetByAddressText(fullAddressDto.AddressText);
            if (getFullAddress != null)
                return new ErrorDataResult<FullAddressDto>(Messages.FullAddressAlreadyExists);

            FullAddress addFullAddress = new()
            {
                CityId = fullAddressDto.CityId,
                DistrictId = fullAddressDto.DistrictId,
                AddressTitle = fullAddressDto.AddressTitle,
                PostalCode = fullAddressDto.PostalCode,
                AddressText = fullAddressDto.AddressText,
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = DateTimeOffset.Now,
            };
            _fullAddressDal.Add(addFullAddress);

            FullAddressDto addFullAddressDto = FillDto(addFullAddress);

            return new SuccessDataResult<FullAddressDto>(addFullAddressDto, Messages.FullAddressAdded);
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
            FullAddress getFullAddress = _fullAddressDal.GetById(id);
            if (getFullAddress == null)
                return new ErrorDataResult<FullAddressDto>(Messages.FullAddressNotFound);

            FullAddressDto getFullAddressDto = FillDto(getFullAddress);

            return new SuccessDataResult<FullAddressDto>(getFullAddressDto, Messages.FullAddressListedById);
        }

        public IResult Update(FullAddressDto fullAddressDto)
        {
            FullAddress getFullAddress = _fullAddressDal.GetById(fullAddressDto.FullAddressId);
            if (getFullAddress == null)
                return new ErrorDataResult<FullAddressDto>(Messages.FullAddressNotFound);

            getFullAddress.CityId = fullAddressDto.CityId;
            getFullAddress.DistrictId = fullAddressDto.DistrictId;
            getFullAddress.AddressTitle = fullAddressDto.AddressTitle;
            getFullAddress.PostalCode = fullAddressDto.PostalCode;
            getFullAddress.AddressText = fullAddressDto.AddressText;
            getFullAddress.UpdatedAt = DateTimeOffset.Now;

            _fullAddressDal.Update(getFullAddress);

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
}

using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete
{
    public class FullAddressBl
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
            {
                return new ErrorDataResult<FullAddressDto>(Messages.FullAddressAlreadyExists);
            }

            FullAddress addFullAddress = new()
            {
                CityId = fullAddressDto.CityId,
                DistrictId = fullAddressDto.DistrictId,
                AddressTitle = "Merkez",
                PostalCode = 0,
                AddressText = fullAddressDto.AddressText,
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = DateTimeOffset.Now,
            };
            _fullAddressDal.Add(addFullAddress);

            FullAddressDto addFullAddressDto = FillDto(addFullAddress);

            return new SuccessDataResult<FullAddressDto>(addFullAddressDto, Messages.FullAddressAdded);
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

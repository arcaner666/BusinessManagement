using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete
{
    public class DistrictBl : IDistrictBl
    {
        private readonly IDistrictDal _districtDal;

        public DistrictBl(
            IDistrictDal districtDal
        )
        {
            _districtDal = districtDal;
        }

        public IDataResult<List<DistrictDto>> GetAll()
        {
            List<District> getDistricts = _districtDal.GetAll();
            if (getDistricts.Count == 0)
                return new ErrorDataResult<List<DistrictDto>>(Messages.DistrictsNotFound);

            List<DistrictDto> getDistrictDtos = FillDtos(getDistricts);

            return new SuccessDataResult<List<DistrictDto>>(getDistrictDtos, Messages.DistrictsListed);
        }

        public IDataResult<List<DistrictDto>> GetByCityId(short cityId)
        {
            List<District> getDistricts = _districtDal.GetByCityId(cityId);
            if (getDistricts.Count == 0) 
                return new ErrorDataResult<List<DistrictDto>>(Messages.DistrictsNotFound);

            List<DistrictDto> getDistrictDtos = FillDtos(getDistricts);

            return new SuccessDataResult<List<DistrictDto>>(getDistrictDtos, Messages.DistrictsListedByCityId);
        }

        private DistrictDto FillDto(District district)
        {
            DistrictDto districtDto = new()
            {
                DistrictId = district.DistrictId,
                CityId = district.CityId,
                DistrictName = district.DistrictName,
            };

            return districtDto;
        }

        private List<DistrictDto> FillDtos(List<District> districts)
        {
            List<DistrictDto> districtDtos = districts.Select(district => FillDto(district)).ToList();

            return districtDtos;
        }
    }
}

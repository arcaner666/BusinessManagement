using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

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
        List<District> allDistricts = _districtDal.GetAll();
        if (allDistricts.Count == 0)
            return new ErrorDataResult<List<DistrictDto>>(Messages.DistrictsNotFound);

        List<DistrictDto> allDistrictDtos = FillDtos(allDistricts);

        return new SuccessDataResult<List<DistrictDto>>(allDistrictDtos, Messages.DistrictsListed);
    }

    public IDataResult<List<DistrictDto>> GetByCityId(short cityId)
    {
        List<District> searchedDistricts = _districtDal.GetByCityId(cityId);
        if (searchedDistricts.Count == 0) 
            return new ErrorDataResult<List<DistrictDto>>(Messages.DistrictsNotFound);

        List<DistrictDto> searchedDistrictDtos = FillDtos(searchedDistricts);

        return new SuccessDataResult<List<DistrictDto>>(searchedDistrictDtos, Messages.DistrictsListedByCityId);
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

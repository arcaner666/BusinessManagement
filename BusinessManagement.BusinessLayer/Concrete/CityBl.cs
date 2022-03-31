using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class CityBl : ICityBl
{
    private readonly ICityDal _cityDal;

    public CityBl(
        ICityDal cityDal
    )
    {
        _cityDal = cityDal;
    }

    public IDataResult<List<CityDto>> GetAll()
    {
        List<City> getCities = _cityDal.GetAll();
        if (getCities.Count == 0)
            return new ErrorDataResult<List<CityDto>>(Messages.CitiesNotFound);

        List<CityDto> getCityDtos = FillDtos(getCities);

        return new SuccessDataResult<List<CityDto>>(getCityDtos, Messages.CitiesListed);
    }

    private CityDto FillDto(City city)
    {
        CityDto cityDto = new()
        {
            CityId = city.CityId,
            PlateCode = city.PlateCode,
            CityName = city.CityName,
        };

        return cityDto;
    }

    private List<CityDto> FillDtos(List<City> cities)
    {
        List<CityDto> cityDtos = cities.Select(city => FillDto(city)).ToList();

        return cityDtos;
    }
}

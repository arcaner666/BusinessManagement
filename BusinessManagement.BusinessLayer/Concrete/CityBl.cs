using AutoMapper;
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
    private readonly IMapper _mapper;

    public CityBl(
        ICityDal cityDal,
        IMapper mapper
    )
    {
        _cityDal = cityDal;
        _mapper = mapper;
    }

    public IDataResult<IEnumerable<CityDto>> GetAll()
    {
        IEnumerable<City> cities = _cityDal.GetAll();
        if (!cities.Any())
            return new ErrorDataResult<IEnumerable<CityDto>>(Messages.CitiesNotFound);

        var cityDtos = _mapper.Map<IEnumerable<CityDto>>(cities);

        return new SuccessDataResult<IEnumerable<CityDto>>(cityDtos, Messages.CitiesListed);
    }
}

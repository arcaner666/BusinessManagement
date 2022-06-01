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
        List<CityDto> cityDtos = _cityDal.GetAll();
        if (cityDtos.Count == 0)
            return new ErrorDataResult<List<CityDto>>(Messages.CitiesNotFound);

        return new SuccessDataResult<List<CityDto>>(cityDtos, Messages.CitiesListed);
    }
}

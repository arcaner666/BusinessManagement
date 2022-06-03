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

    public IDataResult<IEnumerable<CityDto>> GetAll()
    {
        IEnumerable<CityDto> cityDtos = _cityDal.GetAll();
        if (!cityDtos.Any())
            return new ErrorDataResult<IEnumerable<CityDto>>(Messages.CitiesNotFound);

        return new SuccessDataResult<IEnumerable<CityDto>>(cityDtos, Messages.CitiesListed);
    }
}

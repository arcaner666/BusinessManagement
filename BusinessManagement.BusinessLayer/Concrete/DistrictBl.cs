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

    public IDataResult<IEnumerable<DistrictDto>> GetAll()
    {
        IEnumerable<DistrictDto> districtDtos = _districtDal.GetAll();
        if (!districtDtos.Any())
            return new ErrorDataResult<IEnumerable<DistrictDto>>(Messages.DistrictsNotFound);

        return new SuccessDataResult<IEnumerable<DistrictDto>>(districtDtos, Messages.DistrictsListed);
    }

    public IDataResult<IEnumerable<DistrictDto>> GetByCityId(short cityId)
    {
        IEnumerable<DistrictDto> districtDto = _districtDal.GetByCityId(cityId);
        if (!districtDto.Any()) 
            return new ErrorDataResult<IEnumerable<DistrictDto>>(Messages.DistrictsNotFound);

        return new SuccessDataResult<IEnumerable<DistrictDto>>(districtDto, Messages.DistrictsListedByCityId);
    }
}

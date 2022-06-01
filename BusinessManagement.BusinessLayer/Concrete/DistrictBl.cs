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
        List<DistrictDto> districtDtos = _districtDal.GetAll();
        if (districtDtos.Count == 0)
            return new ErrorDataResult<List<DistrictDto>>(Messages.DistrictsNotFound);

        return new SuccessDataResult<List<DistrictDto>>(districtDtos, Messages.DistrictsListed);
    }

    public IDataResult<List<DistrictDto>> GetByCityId(short cityId)
    {
        List<DistrictDto> districtDto = _districtDal.GetByCityId(cityId);
        if (districtDto.Count == 0) 
            return new ErrorDataResult<List<DistrictDto>>(Messages.DistrictsNotFound);

        return new SuccessDataResult<List<DistrictDto>>(districtDto, Messages.DistrictsListedByCityId);
    }
}

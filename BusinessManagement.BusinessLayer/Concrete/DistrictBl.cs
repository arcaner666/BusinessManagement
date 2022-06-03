using AutoMapper;
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
    private readonly IMapper _mapper;

    public DistrictBl(
        IDistrictDal districtDal,
        IMapper mapper
    )
    {
        _districtDal = districtDal;
        _mapper = mapper;
    }

    public IDataResult<IEnumerable<DistrictDto>> GetAll()
    {
        IEnumerable<District> districts = _districtDal.GetAll();
        if (!districts.Any())
            return new ErrorDataResult<IEnumerable<DistrictDto>>(Messages.DistrictsNotFound);

        var districtDtos = _mapper.Map<IEnumerable<DistrictDto>>(districts);

        return new SuccessDataResult<IEnumerable<DistrictDto>>(districtDtos, Messages.DistrictsListed);
    }

    public IDataResult<IEnumerable<DistrictDto>> GetByCityId(short cityId)
    {
        IEnumerable<District> district = _districtDal.GetByCityId(cityId);
        if (!district.Any()) 
            return new ErrorDataResult<IEnumerable<DistrictDto>>(Messages.DistrictsNotFound);

        var districtDto = _mapper.Map<IEnumerable<DistrictDto>>(district);

        return new SuccessDataResult<IEnumerable<DistrictDto>>(districtDto, Messages.DistrictsListedByCityId);
    }
}

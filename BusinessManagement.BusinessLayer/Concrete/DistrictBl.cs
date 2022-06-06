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

    public IDataResult<List<DistrictDto>> GetAll()
    {
        List<District> districts = _districtDal.GetAll();
        if (!districts.Any())
            return new ErrorDataResult<List<DistrictDto>>(Messages.DistrictsNotFound);

        var districtDtos = _mapper.Map<List<DistrictDto>>(districts);

        return new SuccessDataResult<List<DistrictDto>>(districtDtos, Messages.DistrictsListed);
    }

    public IDataResult<List<DistrictDto>> GetByCityId(short cityId)
    {
        List<District> district = _districtDal.GetByCityId(cityId);
        if (!district.Any()) 
            return new ErrorDataResult<List<DistrictDto>>(Messages.DistrictsNotFound);

        var districtDto = _mapper.Map<List<DistrictDto>>(district);

        return new SuccessDataResult<List<DistrictDto>>(districtDto, Messages.DistrictsListedByCityId);
    }
}

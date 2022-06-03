using AutoMapper;
using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class BusinessBl : IBusinessBl
{
    private readonly IBusinessDal _businessDal;
    private readonly IMapper _mapper;

    public BusinessBl(
        IBusinessDal businessDal,
        IMapper mapper
    )
    {
        _businessDal = businessDal;
        _mapper = mapper;
    }

    public IDataResult<BusinessDto> Add(BusinessDto businessDto)
    {
        Business searchedBusiness = _businessDal.GetByBusinessName(businessDto.BusinessName);
        if (searchedBusiness is not null)
            return new ErrorDataResult<BusinessDto>(Messages.BusinessAlreadyExists);

        var addedBusiness = _mapper.Map<Business>(businessDto);

        addedBusiness.BusinessOrder = 0; // Her işletmeye özel bir kod üretilecek.
        addedBusiness.BusinessCode = ""; // Her işletmeye özel bir kod üretilecek.
        addedBusiness.CreatedAt = DateTimeOffset.Now;
        addedBusiness.UpdatedAt = DateTimeOffset.Now;
        int id = _businessDal.Add(addedBusiness);
        addedBusiness.BusinessId = id;

        var addedBusinessDto = _mapper.Map<BusinessDto>(addedBusiness);

        return new SuccessDataResult<BusinessDto>(addedBusinessDto, Messages.BusinessAdded);
    }
}

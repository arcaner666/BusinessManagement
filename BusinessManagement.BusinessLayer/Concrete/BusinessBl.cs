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

    public BusinessBl(
        IBusinessDal businessDal
    )
    {
        _businessDal = businessDal;
    }

    public IDataResult<BusinessDto> Add(BusinessDto businessDto)
    {
        Business searchedBusiness = _businessDal.GetByBusinessName(businessDto.BusinessName);
        if (searchedBusiness is not null)
            return new ErrorDataResult<BusinessDto>(Messages.BusinessAlreadyExists);

        Business addedBusiness = new()
        {
            OwnerSystemUserId = businessDto.OwnerSystemUserId,
            BusinessOrder = 0, // Her işletmeye özel bir kod üretilecek.
            BusinessName = businessDto.BusinessName,
            BusinessCode = "", // Her işletmeye özel bir kod üretilecek.
            CreatedAt = DateTimeOffset.Now,
            UpdatedAt = DateTimeOffset.Now,
        };
        _businessDal.Add(addedBusiness);

        BusinessDto addedBusinessDto = FillDto(addedBusiness);

        return new SuccessDataResult<BusinessDto>(addedBusinessDto, Messages.BusinessAdded);
    }
    
    private BusinessDto FillDto(Business business)
    {
        BusinessDto businessDto = new()
        {
            BusinessId = business.BusinessId,
            OwnerSystemUserId = business.OwnerSystemUserId,
            BusinessOrder = business.BusinessOrder,
            BusinessName = business.BusinessName,
            BusinessCode = business.BusinessCode,
            CreatedAt = business.CreatedAt,
            UpdatedAt = business.UpdatedAt,
        };

        return businessDto;
    }

    private List<BusinessDto> FillDtos(List<Business> businesses)
    {
        List<BusinessDto> businessDtos = businesses.Select(business => FillDto(business)).ToList();

        return businessDtos;
    }
}

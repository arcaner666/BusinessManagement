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
        BusinessDto searchedBusinessDto = _businessDal.GetByBusinessName(businessDto.BusinessName);
        if (searchedBusinessDto is not null)
            return new ErrorDataResult<BusinessDto>(Messages.BusinessAlreadyExists);

        businessDto.BusinessOrder = 0; // Her işletmeye özel bir kod üretilecek.
        businessDto.BusinessCode = ""; // Her işletmeye özel bir kod üretilecek.
        businessDto.CreatedAt = DateTimeOffset.Now;
        businessDto.UpdatedAt = DateTimeOffset.Now;
        int id = _businessDal.Add(businessDto);
        businessDto.BusinessId = id;

        return new SuccessDataResult<BusinessDto>(businessDto, Messages.BusinessAdded);
    }
}

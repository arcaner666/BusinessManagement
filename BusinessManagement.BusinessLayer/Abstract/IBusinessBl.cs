using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IBusinessBl
{
    IDataResult<BusinessDto> Add(BusinessDto businessDto);
}

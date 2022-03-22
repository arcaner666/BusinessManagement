using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract
{
    public interface IFullAddressBl
    {
        IDataResult<FullAddressDto> Add(FullAddressDto fullAddressDto);
    }
}

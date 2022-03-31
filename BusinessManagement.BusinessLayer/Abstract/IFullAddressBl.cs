using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IFullAddressBl
{
    IDataResult<FullAddressDto> Add(FullAddressDto fullAddressDto);
    IResult Delete(long id);
    IDataResult<FullAddressDto> GetById(long id);
    IResult Update(FullAddressDto fullAddressDto);
}

using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IDistrictBl
{
    IDataResult<IEnumerable<DistrictDto>> GetAll();
    IDataResult<IEnumerable<DistrictDto>> GetByCityId(short cityId);
}

using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract
{
    public interface IDistrictBl
    {
        IDataResult<List<DistrictDto>> GetAll();
        IDataResult<List<DistrictDto>> GetByCityId(short cityId);
    }
}

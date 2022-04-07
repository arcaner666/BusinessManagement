using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IHouseOwnerBl
{
    IDataResult<List<HouseOwnerDto>> GetByBusinessId(int businessId);
}

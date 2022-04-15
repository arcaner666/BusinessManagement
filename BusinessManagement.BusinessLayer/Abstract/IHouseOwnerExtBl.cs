using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IHouseOwnerExtBl
{
    IDataResult<HouseOwnerExtDto> GetExtById(long id);
    IDataResult<List<HouseOwnerExtDto>> GetExtsByBusinessId(int businessId);
}

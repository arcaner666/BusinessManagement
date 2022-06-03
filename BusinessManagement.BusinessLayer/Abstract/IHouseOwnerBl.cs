using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IHouseOwnerBl
{
    IDataResult<HouseOwnerDto> Add(HouseOwnerDto houseOwnerDto);
    IResult Delete(long id);
    IDataResult<HouseOwnerDto> GetByAccountId(long accountId);
    IDataResult<IEnumerable<HouseOwnerDto>> GetByBusinessId(int businessId);
    IDataResult<HouseOwnerDto> GetById(long id);
    IResult Update(HouseOwnerDto houseOwnerDto);
}

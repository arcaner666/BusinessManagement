using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IHouseOwnerBl
{
    IDataResult<HouseOwnerDto> Add(HouseOwnerDto houseOwnerDto);
    IResult Delete(long id);
    IDataResult<HouseOwnerDto> GetById(long id);
    IDataResult<HouseOwnerExtDto> GetExtById(long id);
    IDataResult<List<HouseOwnerExtDto>> GetExtsByBusinessId(int businessId);
    IResult Update(HouseOwnerDto houseOwnerDto);
}

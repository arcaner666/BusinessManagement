using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IHouseOwnerExtBl
{
    IResult AddExt(HouseOwnerExtDto houseOwnerExtDto);
    IResult DeleteExt(long id);
    IDataResult<HouseOwnerExtDto> GetExtById(long id);
    IDataResult<List<HouseOwnerExtDto>> GetExtsByBusinessId(int businessId);
    IResult UpdateExt(HouseOwnerExtDto houseOwnerExtDto);
}

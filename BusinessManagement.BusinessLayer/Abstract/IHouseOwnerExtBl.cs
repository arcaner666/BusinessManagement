using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IHouseOwnerExtBl
{
    IResult AddExt(HouseOwnerExtDto houseOwnerExtDto);
    IResult DeleteExt(long id);
    IResult DeleteExtByAccountId(long accountId);
    IDataResult<HouseOwnerExtDto> GetExtByAccountId(long accountId);
    IDataResult<HouseOwnerExtDto> GetExtById(long id);
    IDataResult<IEnumerable<HouseOwnerExtDto>> GetExtsByBusinessId(int businessId);
    IResult UpdateExt(HouseOwnerExtDto houseOwnerExtDto);
}

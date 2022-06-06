using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IHouseOwnerBl
{
    IDataResult<HouseOwnerDto> Add(HouseOwnerDto houseOwnerDto);
    IResult Delete(long id);
    IDataResult<HouseOwnerDto> GetByAccountId(long accountId);
    IDataResult<List<HouseOwnerDto>> GetByBusinessId(int businessId);
    IDataResult<HouseOwnerDto> GetById(long id);
    IDataResult<HouseOwnerExtDto> GetExtByAccountId(long accountId);
    IDataResult<HouseOwnerExtDto> GetExtById(long id);
    IDataResult<List<HouseOwnerExtDto>> GetExtsByBusinessId(int businessId);
    IResult Update(HouseOwnerDto houseOwnerDto);
}

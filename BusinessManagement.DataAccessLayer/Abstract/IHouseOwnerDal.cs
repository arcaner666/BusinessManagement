using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IHouseOwnerDal
{
    long Add(HouseOwnerDto houseOwnerDto);
    void Delete(long id);
    HouseOwnerDto GetByAccountId(long accountId);
    List<HouseOwnerDto> GetByBusinessId(int businessId);
    HouseOwnerDto GetByBusinessIdAndAccountId(int businessId, long accountId);
    HouseOwnerDto GetById(long id);
    HouseOwnerExtDto GetExtByAccountId(long accountId);
    HouseOwnerExtDto GetExtById(long id);
    List<HouseOwnerExtDto> GetExtsByBusinessId(int businessId);
    void Update(HouseOwnerDto houseOwnerDto);
}

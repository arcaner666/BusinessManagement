using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IHouseOwnerDal
{
    HouseOwner Add(HouseOwner houseOwner);
    void Delete(long id);
    HouseOwner GetByAccountId(long accountId);
    List<HouseOwner> GetByBusinessId(int businessId);
    HouseOwner GetByBusinessIdAndAccountId(int businessId, long accountId);
    HouseOwner GetById(long id);
    HouseOwner GetExtByAccountId(long accountId);
    HouseOwner GetExtById(long id);
    List<HouseOwner> GetExtsByBusinessId(int businessId);
    void Update(HouseOwner houseOwner);
}

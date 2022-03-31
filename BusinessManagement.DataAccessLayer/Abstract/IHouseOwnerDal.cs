using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IHouseOwnerDal
{
    HouseOwner Add(HouseOwner houseOwner);
    void Delete(long id);
    HouseOwner GetById(long id);
    List<HouseOwner> GetExtsByBusinessId(int businessId);
    HouseOwner GetIfAlreadyExist(int businessId, long accountId);
    void Update(HouseOwner houseOwner);
}

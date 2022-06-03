using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IHouseOwnerDal
{
    long Add(HouseOwner houseOwner);
    void Delete(long id);
    HouseOwner GetByAccountId(long accountId);
    IEnumerable<HouseOwner> GetByBusinessId(int businessId);
    HouseOwner GetByBusinessIdAndAccountId(int businessId, long accountId);
    HouseOwner GetById(long id);
    HouseOwnerExt GetExtByAccountId(long accountId);
    HouseOwnerExt GetExtById(long id);
    IEnumerable<HouseOwnerExt> GetExtsByBusinessId(int businessId);
    void Update(HouseOwner houseOwner);
}

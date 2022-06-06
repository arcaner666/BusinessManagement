using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IManagerDal
{
    long Add(Manager manager);
    void Delete(long id);
    List<Manager> GetByBusinessId(int businessId);
    Manager GetByBusinessIdAndPhone(int businessId, string phone);
    Manager GetById(long id);
    List<ManagerExt> GetExtsByBusinessId(int businessId);
    void Update(Manager manager);
}

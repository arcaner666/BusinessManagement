using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IManagerDal
{
    Manager Add(Manager manager);
    void Delete(long id);
    List<Manager> GetByBusinessId(int businessId);
    Manager GetByBusinessIdAndPhone(int businessId, string phone);
    Manager GetById(long id);
    List<Manager> GetExtsByBusinessId(int businessId);
    void Update(Manager manager);
}

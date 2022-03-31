using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IManagerDal
{
    Manager Add(Manager manager);
    List<Manager> GetByBusinessId(int businessId);
    Manager GetByBusinessIdAndPhone(int businessId, string phone);
    List<Manager> GetExtsByBusinessId(int businessId);
}

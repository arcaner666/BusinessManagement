using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract
{
    public interface IManagerDal
    {
        Manager Add(Manager manager);
        Manager GetByBusinessIdAndPhone(int businessId, string phone);
        List<Manager> GetExtsByBusinessId(int businessId);
    }
}

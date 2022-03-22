using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract
{
    public interface IManagerDal
    {
        Manager Add(Manager manager);
        List<Manager> GetExtsByBusinessId(int businessId);
        Manager GetIfAlreadyExist(int businessId, string phone);
    }
}

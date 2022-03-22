using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract
{
    public interface IEmployeeTypeDal
    {
        List<EmployeeType> GetAll();
    }
}

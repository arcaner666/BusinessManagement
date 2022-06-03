using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IEmployeeTypeDal
{
    IEnumerable<EmployeeType> GetAll();
}

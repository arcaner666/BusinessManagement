using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IEmployeeTypeDal
{
    List<EmployeeTypeDto> GetAll();
}

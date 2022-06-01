using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class EmployeeTypeBl : IEmployeeTypeBl
{
    private readonly IEmployeeTypeDal _employeeTypeDal;

    public EmployeeTypeBl(
        IEmployeeTypeDal employeeTypeDal
    )
    {
        _employeeTypeDal = employeeTypeDal;
    }

    public IDataResult<List<EmployeeTypeDto>> GetAll()
    {
        List<EmployeeTypeDto> employeeTypeDtos = _employeeTypeDal.GetAll();
        if (employeeTypeDtos.Count == 0)
            return new ErrorDataResult<List<EmployeeTypeDto>>(Messages.EmployeeTypesNotFound);

        return new SuccessDataResult<List<EmployeeTypeDto>>(employeeTypeDtos, Messages.EmployeeTypesListed);
    }
}

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
        List<EmployeeType> allEmployeeTypes = _employeeTypeDal.GetAll();
        if (allEmployeeTypes.Count == 0)
            return new ErrorDataResult<List<EmployeeTypeDto>>(Messages.EmployeeTypesNotFound);

        List<EmployeeTypeDto> allEmployeeTypeDtos = FillDtos(allEmployeeTypes);

        return new SuccessDataResult<List<EmployeeTypeDto>>(allEmployeeTypeDtos, Messages.EmployeeTypesListed);
    }

    private EmployeeTypeDto FillDto(EmployeeType employeeType)
    {
        EmployeeTypeDto employeeTypeDto = new()
        {
            EmployeeTypeId = employeeType.EmployeeTypeId,
            EmployeeTypeName = employeeType.EmployeeTypeName,
        };

        return employeeTypeDto;
    }

    private List<EmployeeTypeDto> FillDtos(List<EmployeeType> employeeTypes)
    {
        List<EmployeeTypeDto> employeeTypeDtos = employeeTypes.Select(employeeType => FillDto(employeeType)).ToList();

        return employeeTypeDtos;
    }
}

using AutoMapper;
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
    private readonly IMapper _mapper;

    public EmployeeTypeBl(
        IEmployeeTypeDal employeeTypeDal,
        IMapper mapper
    )
    {
        _employeeTypeDal = employeeTypeDal;
        _mapper = mapper;
    }

    public IDataResult<IEnumerable<EmployeeTypeDto>> GetAll()
    {
        IEnumerable<EmployeeType> employeeTypes = _employeeTypeDal.GetAll();
        if (!employeeTypes.Any())
            return new ErrorDataResult<IEnumerable<EmployeeTypeDto>>(Messages.EmployeeTypesNotFound);

        var employeeTypeDtos = _mapper.Map<IEnumerable<EmployeeTypeDto>>(employeeTypes);

        return new SuccessDataResult<IEnumerable<EmployeeTypeDto>>(employeeTypeDtos, Messages.EmployeeTypesListed);
    }
}

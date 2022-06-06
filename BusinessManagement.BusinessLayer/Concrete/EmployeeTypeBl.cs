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

    public IDataResult<List<EmployeeTypeDto>> GetAll()
    {
        List<EmployeeType> employeeTypes = _employeeTypeDal.GetAll();
        if (!employeeTypes.Any())
            return new ErrorDataResult<List<EmployeeTypeDto>>(Messages.EmployeeTypesNotFound);

        var employeeTypeDtos = _mapper.Map<List<EmployeeTypeDto>>(employeeTypes);

        return new SuccessDataResult<List<EmployeeTypeDto>>(employeeTypeDtos, Messages.EmployeeTypesListed);
    }
}

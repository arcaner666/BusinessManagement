using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class ManagerBl : IManagerBl
{
    private readonly IManagerDal _managerDal;

    public ManagerBl(
        IManagerDal managerDal
    )
    {
        _managerDal = managerDal;
    }

    public IDataResult<ManagerDto> Add(ManagerDto managerDto)
    {
        ManagerDto searchedManagerDto = _managerDal.GetByBusinessIdAndPhone(managerDto.BusinessId, managerDto.Phone);
        if (searchedManagerDto is not null)
            return new ErrorDataResult<ManagerDto>(Messages.ManagerAlreadyExists);

        managerDto.CreatedAt = DateTimeOffset.Now;
        managerDto.UpdatedAt = DateTimeOffset.Now;

        long id = _managerDal.Add(managerDto);
        managerDto.ManagerId = id;

        return new SuccessDataResult<ManagerDto>(managerDto, Messages.ManagerAdded);
    }

    public IDataResult<IEnumerable<ManagerDto>> GetByBusinessId(int businessId)
    {
        IEnumerable<ManagerDto> managerDtos = _managerDal.GetByBusinessId(businessId);
        if (!managerDtos.Any())
            return new ErrorDataResult<IEnumerable<ManagerDto>>(Messages.ManagersNotFound);

        return new SuccessDataResult<IEnumerable<ManagerDto>>(managerDtos, Messages.ManagersListedByBusinessId);
    }
}

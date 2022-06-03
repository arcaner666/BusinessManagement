using AutoMapper;
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
    private readonly IMapper _mapper;

    public ManagerBl(
        IManagerDal managerDal,
        IMapper mapper
    )
    {
        _managerDal = managerDal;
        _mapper = mapper;
    }

    public IDataResult<ManagerDto> Add(ManagerDto managerDto)
    {
        Manager searchedManager = _managerDal.GetByBusinessIdAndPhone(managerDto.BusinessId, managerDto.Phone);
        if (searchedManager is not null)
            return new ErrorDataResult<ManagerDto>(Messages.ManagerAlreadyExists);

        var addedManager = _mapper.Map<Manager>(managerDto);

        addedManager.CreatedAt = DateTimeOffset.Now;
        addedManager.UpdatedAt = DateTimeOffset.Now;
        long id = _managerDal.Add(addedManager);
        addedManager.ManagerId = id;

        var addedManagerDto = _mapper.Map<ManagerDto>(addedManager);

        return new SuccessDataResult<ManagerDto>(addedManagerDto, Messages.ManagerAdded);
    }

    public IDataResult<IEnumerable<ManagerDto>> GetByBusinessId(int businessId)
    {
        IEnumerable<Manager> managers = _managerDal.GetByBusinessId(businessId);
        if (!managers.Any())
            return new ErrorDataResult<IEnumerable<ManagerDto>>(Messages.ManagersNotFound);

        var managerDtos = _mapper.Map<IEnumerable<ManagerDto>>(managers);

        return new SuccessDataResult<IEnumerable<ManagerDto>>(managerDtos, Messages.ManagersListedByBusinessId);
    }
}

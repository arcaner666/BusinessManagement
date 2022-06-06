using AutoMapper;
using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
using BusinessManagement.Entities.ExtendedDatabaseModels;

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

    public IDataResult<List<ManagerDto>> GetByBusinessId(int businessId)
    {
        List<Manager> managers = _managerDal.GetByBusinessId(businessId);
        if (!managers.Any())
            return new ErrorDataResult<List<ManagerDto>>(Messages.ManagersNotFound);

        var managerDtos = _mapper.Map<List<ManagerDto>>(managers);

        return new SuccessDataResult<List<ManagerDto>>(managerDtos, Messages.ManagersListedByBusinessId);
    }

    public IDataResult<List<ManagerExtDto>> GetExtsByBusinessId(int businessId)
    {
        List<ManagerExt> managerExts = _managerDal.GetExtsByBusinessId(businessId);
        if (!managerExts.Any())
            return new ErrorDataResult<List<ManagerExtDto>>(Messages.ManagerNotFound);

        var managerExtDtos = _mapper.Map<List<ManagerExtDto>>(managerExts);

        return new SuccessDataResult<List<ManagerExtDto>>(managerExtDtos, Messages.ManagerExtsListedByBusinessId);
    }
}

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
        Manager searchedManager = _managerDal.GetByBusinessIdAndPhone(managerDto.BusinessId, managerDto.Phone);
        if (searchedManager is not null)
            return new ErrorDataResult<ManagerDto>(Messages.ManagerAlreadyExists);

        Manager addedManager = new()
        {
            BusinessId = managerDto.BusinessId,
            BranchId = managerDto.BranchId,
            NameSurname = managerDto.NameSurname,
            Email = managerDto.Email,
            Phone = managerDto.Phone,
            Gender = managerDto.Gender,
            Notes = managerDto.Notes,
            AvatarUrl = managerDto.AvatarUrl,
            TaxOffice = managerDto.TaxOffice,
            TaxNumber = managerDto.TaxNumber,
            IdentityNumber = managerDto.IdentityNumber,
            CreatedAt = DateTimeOffset.Now,
            UpdatedAt = DateTimeOffset.Now,
        };
        _managerDal.Add(addedManager);

        ManagerDto addedManagerDto = FillDto(addedManager);

        return new SuccessDataResult<ManagerDto>(addedManagerDto, Messages.ManagerAdded);
    }

    public IDataResult<List<ManagerDto>> GetByBusinessId(int businessId)
    {
        List<Manager> searchedManagers = _managerDal.GetByBusinessId(businessId);
        if (searchedManagers.Count == 0)
            return new ErrorDataResult<List<ManagerDto>>(Messages.ManagersNotFound);

        List<ManagerDto> searchedManagerDtos = FillDtos(searchedManagers);

        return new SuccessDataResult<List<ManagerDto>>(searchedManagerDtos, Messages.ManagersListedByBusinessId);
    }

    private ManagerDto FillDto(Manager manager)
    {
        ManagerDto managerDto = new()
        {
            ManagerId = manager.ManagerId,
            BusinessId = manager.BusinessId,
            BranchId = manager.BranchId,
            NameSurname = manager.NameSurname,
            Email = manager.Email,
            Phone = manager.Phone,
            DateOfBirth = manager.DateOfBirth,
            Gender = manager.Gender,
            Notes = manager.Notes,
            AvatarUrl = manager.AvatarUrl,
            TaxOffice = manager.TaxOffice,
            TaxNumber = manager.TaxNumber,
            IdentityNumber = manager.IdentityNumber,
            CreatedAt = manager.CreatedAt,
            UpdatedAt = manager.UpdatedAt,
        };

        return managerDto;
    }

    private List<ManagerDto> FillDtos(List<Manager> managers)
    {
        List<ManagerDto> managerDtos = managers.Select(manager => FillDto(manager)).ToList();

        return managerDtos;
    }
}

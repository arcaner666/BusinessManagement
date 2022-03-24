using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete
{
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
            Manager getManager = _managerDal.GetByBusinessIdAndPhone(managerDto.BusinessId, managerDto.Phone);
            if (getManager != null)
                return new ErrorDataResult<ManagerDto>(Messages.ManagerAlreadyExists);

            Manager addManager = new()
            {
                BusinessId = managerDto.BusinessId,
                BranchId = managerDto.BranchId,
                NameSurname = managerDto.NameSurname,
                Email = "",
                Phone = managerDto.Phone,
                Gender = "",
                Notes = "",
                AvatarUrl = "",
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = DateTimeOffset.Now,
            };
            _managerDal.Add(addManager);

            ManagerDto addManagerDto = FillDto(addManager);

            return new SuccessDataResult<ManagerDto>(addManagerDto, Messages.ManagerAdded);
        }

        public IDataResult<List<ManagerDto>> GetByBusinessId(int businessId)
        {
            List<Manager> managers = _managerDal.GetByBusinessId(businessId);
            if (managers.Count == 0)
                return new ErrorDataResult<List<ManagerDto>>(Messages.ManagersNotFound);

            List<ManagerDto> managerDtos = FillDtos(managers);

            return new SuccessDataResult<List<ManagerDto>>(managerDtos, Messages.ManagersListedByBusinessId);
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

        private ManagerExtDto FillExtDto(Manager manager)
        {
            ManagerExtDto managerExtDto = new()
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
                CreatedAt = manager.CreatedAt,
                UpdatedAt = manager.UpdatedAt,

                BusinessName = manager.Business.BusinessName,
                CityId = manager.Branch.FullAddress.CityId,
                DistrictId = manager.Branch.FullAddress.DistrictId,
                AddressText = manager.Branch.FullAddress.AddressText,
            };
            return managerExtDto;
        }

        private List<ManagerExtDto> FillExtDtos(List<Manager> managers)
        {
            List<ManagerExtDto> managerExtDtos = managers.Select(manager => FillExtDto(manager)).ToList();

            return managerExtDtos;
        }
    }
}

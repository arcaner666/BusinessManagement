using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.BusinessLayer.Utilities.Security.Hashing;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete
{
    public class SystemUserBl : ISystemUserBl
    {
        private readonly ISystemUserDal _systemUserDal;

        public SystemUserBl(
            ISystemUserDal systemUserDal
        )
        {
            _systemUserDal = systemUserDal;
        }

        public IDataResult<SystemUserDto> Add(SystemUserDto systemUserDto)
        {
            SystemUser getSystemUser = _systemUserDal.GetByPhone(systemUserDto.Phone);
            if (getSystemUser != null)
                return new ErrorDataResult<SystemUserDto>(Messages.SystemUserAlreadyExists);

            // Şu aşamada, kayıt olan kullanıcıya SMS ile şifre gönderilemediği için şifre 123456 yapıldı.
            HashingHelper.CreatePasswordHash("123456", out byte[] passwordHash, out byte[] passwordSalt);

            SystemUser addSystemUser = new()
            {
                Email = "",
                Phone = systemUserDto.Phone,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = systemUserDto.Role,
                BusinessId = 0,
                BranchId = 0,
                Blocked = false,
                RefreshToken = "",
                RefreshTokenExpiryTime = DateTime.Now,
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = DateTimeOffset.Now
            };
            _systemUserDal.Add(addSystemUser);

            SystemUserDto addSystemUserDto = FillDto(addSystemUser);

            return new SuccessDataResult<SystemUserDto>(addSystemUserDto);
        }
        
        public IResult Update(SystemUserDto systemUserDto)
        {
            SystemUser getSystemUser = _systemUserDal.GetById(systemUserDto.SystemUserId);
            if (getSystemUser == null)
                return new ErrorDataResult<SystemUserDto>(Messages.SystemUserNotFound);

            getSystemUser.Email = systemUserDto.Email;
            getSystemUser.Role = systemUserDto.Role;
            getSystemUser.BusinessId = systemUserDto.BusinessId;
            getSystemUser.BranchId = systemUserDto.BranchId;
            getSystemUser.Blocked = false;
            getSystemUser.UpdatedAt = DateTimeOffset.Now;
            _systemUserDal.Update(getSystemUser);

            return new SuccessResult(Messages.SystemUserUpdated);
        }

        private SystemUserDto FillDto(SystemUser systemUser)
        {
            SystemUserDto systemUserDto = new()
            {
                SystemUserId = systemUser.SystemUserId,
                Email = systemUser.Email,
                Phone = systemUser.Phone,
                PasswordHash = systemUser.PasswordHash,
                PasswordSalt = systemUser.PasswordSalt,
                Role = systemUser.Role,
                BusinessId = systemUser.BusinessId,
                BranchId = systemUser.BranchId,
                Blocked = systemUser.Blocked,
                RefreshToken = systemUser.RefreshToken,
                RefreshTokenExpiryTime = systemUser.RefreshTokenExpiryTime,
                CreatedAt = systemUser.CreatedAt,
                UpdatedAt = systemUser.UpdatedAt,
            };

            return systemUserDto;
        }

        private List<SystemUserDto> FillDtos(List<SystemUser> systemUsers)
        {
            List<SystemUserDto> systemUserDtos = systemUsers.Select(systemUser => FillDto(systemUser)).ToList();

            return systemUserDtos;
        }
    }
}

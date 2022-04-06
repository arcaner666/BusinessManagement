using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.BusinessLayer.Utilities.Security.Hashing;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

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
        SystemUser searchedSystemUser = _systemUserDal.GetByPhone(systemUserDto.Phone);
        if (searchedSystemUser is not null)
            return new ErrorDataResult<SystemUserDto>(Messages.SystemUserAlreadyExists);

        // Şu aşamada, kayıt olan kullanıcıya SMS ile şifre gönderilemediği için şifre 123456 yapıldı.
        HashingHelper.CreatePasswordHash("123456", out byte[] passwordHash, out byte[] passwordSalt);

        SystemUser addedSystemUser = new()
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
        _systemUserDal.Add(addedSystemUser);

        SystemUserDto addedSystemUserDto = FillDto(addedSystemUser);

        return new SuccessDataResult<SystemUserDto>(addedSystemUserDto, Messages.SystemUserAdded);
    }

    public IDataResult<SystemUserDto> GetByEmail(string email)
    {
        SystemUser searchedSystemUser = _systemUserDal.GetByEmail(email);
        if (searchedSystemUser is null)
            return new ErrorDataResult<SystemUserDto>(Messages.SystemUserNotFound);

        SystemUserDto searchedSystemUserDto = FillDto(searchedSystemUser);

        return new SuccessDataResult<SystemUserDto>(searchedSystemUserDto, Messages.SystemUserListedByEmail);
    }

    public IDataResult<SystemUserDto> GetById(long id)
    {
        SystemUser searchedSystemUser = _systemUserDal.GetById(id);
        if (searchedSystemUser is null)
            return new ErrorDataResult<SystemUserDto>(Messages.SystemUserNotFound);

        SystemUserDto searchedSystemUserDto = FillDto(searchedSystemUser);

        return new SuccessDataResult<SystemUserDto>(searchedSystemUserDto, Messages.SystemUserListedById);
    }

    public IDataResult<SystemUserDto> GetByPhone(string phone)
    {
        SystemUser searchedSystemUser = _systemUserDal.GetByPhone(phone);
        if (searchedSystemUser is null)
            return new ErrorDataResult<SystemUserDto>(Messages.SystemUserNotFound);

        SystemUserDto searchedSystemUserDto = FillDto(searchedSystemUser);

        return new SuccessDataResult<SystemUserDto>(searchedSystemUserDto, Messages.SystemUserListedByPhone);
    }

    public IResult Update(SystemUserDto systemUserDto)
    {
        SystemUser searchedSystemUser = _systemUserDal.GetById(systemUserDto.SystemUserId);
        if (searchedSystemUser is null)
            return new ErrorDataResult<SystemUserDto>(Messages.SystemUserNotFound);

        searchedSystemUser.Email = systemUserDto.Email;
        searchedSystemUser.Role = systemUserDto.Role;
        searchedSystemUser.BusinessId = systemUserDto.BusinessId;
        searchedSystemUser.BranchId = systemUserDto.BranchId;
        searchedSystemUser.Blocked = systemUserDto.Blocked;
        searchedSystemUser.RefreshToken = systemUserDto.RefreshToken;
        searchedSystemUser.RefreshTokenExpiryTime = systemUserDto.RefreshTokenExpiryTime;
        searchedSystemUser.UpdatedAt = DateTimeOffset.Now;
        _systemUserDal.Update(searchedSystemUser);

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

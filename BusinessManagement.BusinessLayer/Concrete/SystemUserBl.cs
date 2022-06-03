using AutoMapper;
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
    private readonly IMapper _mapper;
    private readonly ISystemUserDal _systemUserDal;

    public SystemUserBl(
        IMapper mapper,
        ISystemUserDal systemUserDal
    )
    {
        _mapper = mapper;
        _systemUserDal = systemUserDal;
    }

    public IDataResult<SystemUserDto> Add(SystemUserDto systemUserDto)
    {
        SystemUser searchedSystemUser = _systemUserDal.GetByPhone(systemUserDto.Phone);
        if (searchedSystemUser is not null)
            return new ErrorDataResult<SystemUserDto>(Messages.SystemUserAlreadyExists);

        // Şu aşamada, kayıt olan kullanıcıya SMS ile şifre gönderilemediği için şifre 123456 yapıldı.
        HashingHelper.CreatePasswordHash("123456", out byte[] passwordHash, out byte[] passwordSalt);

        var addedSystemUser = _mapper.Map<SystemUser>(systemUserDto);

        addedSystemUser.Email = "";
        addedSystemUser.PasswordHash = passwordHash;
        addedSystemUser.PasswordSalt = passwordSalt;
        addedSystemUser.BusinessId = 0;
        addedSystemUser.BranchId = 0;
        addedSystemUser.Blocked = false;
        addedSystemUser.RefreshToken = "";
        addedSystemUser.RefreshTokenExpiryTime = DateTime.Now;
        addedSystemUser.CreatedAt = DateTimeOffset.Now;
        addedSystemUser.UpdatedAt = DateTimeOffset.Now;
        long id = _systemUserDal.Add(addedSystemUser);
        addedSystemUser.SystemUserId = id;

        var addedSystemUserDto = _mapper.Map<SystemUserDto>(addedSystemUser);

        return new SuccessDataResult<SystemUserDto>(addedSystemUserDto, Messages.SystemUserAdded);
    }

    public IDataResult<SystemUserDto> GetByEmail(string email)
    {
        SystemUser systemUser = _systemUserDal.GetByEmail(email);
        if (systemUser is null)
            return new ErrorDataResult<SystemUserDto>(Messages.SystemUserNotFound);

        var systemUserDto = _mapper.Map<SystemUserDto>(systemUser);

        return new SuccessDataResult<SystemUserDto>(systemUserDto, Messages.SystemUserListedByEmail);
    }

    public IDataResult<SystemUserDto> GetById(long id)
    {
        SystemUser systemUser = _systemUserDal.GetById(id);
        if (systemUser is null)
            return new ErrorDataResult<SystemUserDto>(Messages.SystemUserNotFound);

        var systemUserDto = _mapper.Map<SystemUserDto>(systemUser);

        return new SuccessDataResult<SystemUserDto>(systemUserDto, Messages.SystemUserListedById);
    }

    public IDataResult<SystemUserDto> GetByPhone(string phone)
    {
        SystemUser systemUser = _systemUserDal.GetByPhone(phone);
        if (systemUser is null)
            return new ErrorDataResult<SystemUserDto>(Messages.SystemUserNotFound);

        var systemUserDto = _mapper.Map<SystemUserDto>(systemUser);

        return new SuccessDataResult<SystemUserDto>(systemUserDto, Messages.SystemUserListedByPhone);
    }

    public IResult Update(SystemUserDto systemUserDto)
    {
        SystemUser systemUser = _systemUserDal.GetById(systemUserDto.SystemUserId);
        if (systemUser is null)
            return new ErrorDataResult<SystemUserDto>(Messages.SystemUserNotFound);

        systemUser.Email = systemUserDto.Email;
        systemUser.Role = systemUserDto.Role;
        systemUser.BusinessId = systemUserDto.BusinessId;
        systemUser.BranchId = systemUserDto.BranchId;
        systemUser.Blocked = systemUserDto.Blocked;
        systemUser.RefreshToken = systemUserDto.RefreshToken;
        systemUser.RefreshTokenExpiryTime = systemUserDto.RefreshTokenExpiryTime;
        systemUser.UpdatedAt = DateTimeOffset.Now;
        _systemUserDal.Update(systemUser);

        return new SuccessResult(Messages.SystemUserUpdated);
    }
}

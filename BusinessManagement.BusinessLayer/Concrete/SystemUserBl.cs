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
        SystemUserDto searchedSystemUserDto = _systemUserDal.GetByPhone(systemUserDto.Phone);
        if (searchedSystemUserDto is not null)
            return new ErrorDataResult<SystemUserDto>(Messages.SystemUserAlreadyExists);

        // Şu aşamada, kayıt olan kullanıcıya SMS ile şifre gönderilemediği için şifre 123456 yapıldı.
        HashingHelper.CreatePasswordHash("123456", out byte[] passwordHash, out byte[] passwordSalt);

        systemUserDto.Email = "";
        systemUserDto.PasswordHash = passwordHash;
        systemUserDto.PasswordSalt = passwordSalt;
        systemUserDto.BusinessId = 0;
        systemUserDto.BranchId = 0;
        systemUserDto.Blocked = false;
        systemUserDto.RefreshToken = "";
        systemUserDto.RefreshTokenExpiryTime = DateTime.Now;
        systemUserDto.CreatedAt = DateTimeOffset.Now;
        systemUserDto.UpdatedAt = DateTimeOffset.Now;

        long id = _systemUserDal.Add(systemUserDto);
        systemUserDto.SystemUserId = id;

        return new SuccessDataResult<SystemUserDto>(systemUserDto, Messages.SystemUserAdded);
    }

    public IDataResult<SystemUserDto> GetByEmail(string email)
    {
        SystemUserDto systemUserDto = _systemUserDal.GetByEmail(email);
        if (systemUserDto is null)
            return new ErrorDataResult<SystemUserDto>(Messages.SystemUserNotFound);

        return new SuccessDataResult<SystemUserDto>(systemUserDto, Messages.SystemUserListedByEmail);
    }

    public IDataResult<SystemUserDto> GetById(long id)
    {
        SystemUserDto systemUserDto = _systemUserDal.GetById(id);
        if (systemUserDto is null)
            return new ErrorDataResult<SystemUserDto>(Messages.SystemUserNotFound);

        return new SuccessDataResult<SystemUserDto>(systemUserDto, Messages.SystemUserListedById);
    }

    public IDataResult<SystemUserDto> GetByPhone(string phone)
    {
        SystemUserDto systemUserDto = _systemUserDal.GetByPhone(phone);
        if (systemUserDto is null)
            return new ErrorDataResult<SystemUserDto>(Messages.SystemUserNotFound);

        return new SuccessDataResult<SystemUserDto>(systemUserDto, Messages.SystemUserListedByPhone);
    }

    public IResult Update(SystemUserDto systemUserDto)
    {
        var searchedSystemUserResult = GetById(systemUserDto.SystemUserId);
        if (searchedSystemUserResult is null)
            return new ErrorDataResult<SystemUserDto>(Messages.SystemUserNotFound);

        searchedSystemUserResult.Data.Email = systemUserDto.Email;
        searchedSystemUserResult.Data.Role = systemUserDto.Role;
        searchedSystemUserResult.Data.BusinessId = systemUserDto.BusinessId;
        searchedSystemUserResult.Data.BranchId = systemUserDto.BranchId;
        searchedSystemUserResult.Data.Blocked = systemUserDto.Blocked;
        searchedSystemUserResult.Data.RefreshToken = systemUserDto.RefreshToken;
        searchedSystemUserResult.Data.RefreshTokenExpiryTime = systemUserDto.RefreshTokenExpiryTime;
        searchedSystemUserResult.Data.UpdatedAt = DateTimeOffset.Now;
        _systemUserDal.Update(searchedSystemUserResult.Data);

        return new SuccessResult(Messages.SystemUserUpdated);
    }
}

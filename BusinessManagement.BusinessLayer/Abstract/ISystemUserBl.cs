using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface ISystemUserBl
{
    IDataResult<SystemUserDto> Add(SystemUserDto systemUserDto);
    IDataResult<SystemUserDto> GetByEmail(string email);
    IDataResult<SystemUserDto> GetById(long id);
    IDataResult<SystemUserDto> GetByPhone(string phone);
    IResult Update(SystemUserDto systemUserDto);
}

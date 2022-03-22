using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract
{
    public interface ISystemUserBl
    {
        IDataResult<SystemUserDto> Add(SystemUserDto systemUserDto);
        IResult Update(SystemUserDto systemUserDto);
    }
}

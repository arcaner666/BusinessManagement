using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface ISystemUserDal
{
    long Add(SystemUserDto systemUserDto);
    void Delete(long id);
    SystemUserDto GetByEmail(string email);
    SystemUserDto GetById(long id);
    SystemUserDto GetByPhone(string phone);
    void Update(SystemUserDto systemUserDto);
}

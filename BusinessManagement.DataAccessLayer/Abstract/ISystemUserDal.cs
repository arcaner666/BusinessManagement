using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface ISystemUserDal
{
    SystemUser Add(SystemUser systemUser);
    void Delete(long id);
    SystemUser GetByEmail(string email);
    SystemUser GetById(long id);
    SystemUser GetByPhone(string phone);
    void Update(SystemUser systemUser);
}

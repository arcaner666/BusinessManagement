using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IManagerDal
{
    long Add(ManagerDto managerDto);
    void Delete(long id);
    List<ManagerDto> GetByBusinessId(int businessId);
    ManagerDto GetByBusinessIdAndPhone(int businessId, string phone);
    ManagerDto GetById(long id);
    List<ManagerExtDto> GetExtsByBusinessId(int businessId);
    void Update(ManagerDto managerDto);
}

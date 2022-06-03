using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IManagerDal
{
    long Add(ManagerDto managerDto);
    void Delete(long id);
    IEnumerable<ManagerDto> GetByBusinessId(int businessId);
    ManagerDto GetByBusinessIdAndPhone(int businessId, string phone);
    ManagerDto GetById(long id);
    IEnumerable<ManagerExtDto> GetExtsByBusinessId(int businessId);
    void Update(ManagerDto managerDto);
}

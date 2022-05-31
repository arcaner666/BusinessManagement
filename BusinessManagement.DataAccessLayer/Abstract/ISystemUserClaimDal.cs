using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface ISystemUserClaimDal
{
    long Add(SystemUserClaimDto systemUserClaimDto);
    void Delete(long id);
    SystemUserClaimDto GetBySystemUserIdAndOperationClaimId(long systemUserId, int operationClaimId);
    List<SystemUserClaimExtDto> GetExtsBySystemUserId(long systemUserId);
}

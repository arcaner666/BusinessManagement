using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface ISystemUserClaimDal
{
    long Add(SystemUserClaim systemUserClaim);
    void Delete(long id);
    SystemUserClaim GetBySystemUserIdAndOperationClaimId(long systemUserId, int operationClaimId);
    List<SystemUserClaimExt> GetExtsBySystemUserId(long systemUserId);
}

using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface ISystemUserClaimDal
{
    long Add(SystemUserClaim systemUserClaim);
    void Delete(long id);
    SystemUserClaim GetBySystemUserIdAndOperationClaimId(long systemUserId, int operationClaimId);
    IEnumerable<SystemUserClaimExt> GetExtsBySystemUserId(long systemUserId);
}

using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract
{
    public interface ISystemUserClaimDal
    {
        SystemUserClaim Add(SystemUserClaim systemUserClaim);
        void Delete(long id);
        SystemUserClaim GetBySystemUserIdAndOperationClaimId(long systemUserId, int operationClaimId);
        List<SystemUserClaim> GetExtsBySystemUserId(long systemUserId);
    }
}

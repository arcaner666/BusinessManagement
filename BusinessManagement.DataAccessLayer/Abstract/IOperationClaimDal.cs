using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IOperationClaimDal
{
    IEnumerable<OperationClaim> GetAll();
    OperationClaim GetByOperationClaimName(string operationClaimName);
}

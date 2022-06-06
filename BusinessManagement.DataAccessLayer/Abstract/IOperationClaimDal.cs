using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IOperationClaimDal
{
    List<OperationClaim> GetAll();
    OperationClaim GetByOperationClaimName(string operationClaimName);
}

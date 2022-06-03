using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IOperationClaimDal
{
    IEnumerable<OperationClaimDto> GetAll();
    OperationClaimDto GetByOperationClaimName(string operationClaimName);
}

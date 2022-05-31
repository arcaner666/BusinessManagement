using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IOperationClaimDal
{
    List<OperationClaimDto> GetAll();
    OperationClaimDto GetByOperationClaimName(string operationClaimName);
}

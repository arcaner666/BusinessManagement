using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class OperationClaimBl : IOperationClaimBl
{
    private readonly IOperationClaimDal _operationClaimDal;

    public OperationClaimBl(
        IOperationClaimDal operationClaimDal
    )
    {
        _operationClaimDal = operationClaimDal;
    }

    public IDataResult<IEnumerable<OperationClaimDto>> GetAll()
    {
        IEnumerable<OperationClaimDto> operationClaimDtos = _operationClaimDal.GetAll();
        if (!operationClaimDtos.Any())
            return new ErrorDataResult<IEnumerable<OperationClaimDto>>(Messages.OperationClaimsNotFound);

        return new SuccessDataResult<IEnumerable<OperationClaimDto>>(operationClaimDtos, Messages.OperationClaimsListed);
    }

    public IDataResult<OperationClaimDto> GetByOperationClaimName(string operationClaimName)
    {
        OperationClaimDto operationClaimDto = _operationClaimDal.GetByOperationClaimName(operationClaimName);
        if (operationClaimDto is null)
            return new ErrorDataResult<OperationClaimDto>(Messages.OperationClaimNotFound);

        return new SuccessDataResult<OperationClaimDto>(operationClaimDto, Messages.OperationClaimListedByOperationClaimName);
    }
}

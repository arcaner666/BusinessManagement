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

    public IDataResult<List<OperationClaimDto>> GetAll()
    {
        List<OperationClaim> allOperationClaims = _operationClaimDal.GetAll();
        if (allOperationClaims.Count == 0)
            return new ErrorDataResult<List<OperationClaimDto>>(Messages.OperationClaimsNotFound);

        List<OperationClaimDto> allOperationClaimDtos = FillDtos(allOperationClaims);

        return new SuccessDataResult<List<OperationClaimDto>>(allOperationClaimDtos, Messages.OperationClaimsListed);
    }

    public IDataResult<OperationClaimDto> GetByOperationClaimName(string operationClaimName)
    {
        OperationClaim searchedOperationClaim = _operationClaimDal.GetByOperationClaimName(operationClaimName);
        if (searchedOperationClaim is null)
            return new ErrorDataResult<OperationClaimDto>(Messages.OperationClaimNotFound);

        OperationClaimDto searchedOperationClaimDto = FillDto(searchedOperationClaim);

        return new SuccessDataResult<OperationClaimDto>(searchedOperationClaimDto, Messages.OperationClaimListedByOperationClaimName);
    }

    private OperationClaimDto FillDto(OperationClaim operationClaim)
    {
        OperationClaimDto operationClaimDto = new()
        {
            OperationClaimId = operationClaim.OperationClaimId,
            OperationClaimName = operationClaim.OperationClaimName,
        };

        return operationClaimDto;
    }

    private List<OperationClaimDto> FillDtos(List<OperationClaim> operationClaims)
    {
        List<OperationClaimDto> operationClaimDtos = operationClaims.Select(operationClaim => FillDto(operationClaim)).ToList();

        return operationClaimDtos;
    }
}

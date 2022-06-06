using AutoMapper;
using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class OperationClaimBl : IOperationClaimBl
{
    private readonly IMapper _mapper;
    private readonly IOperationClaimDal _operationClaimDal;

    public OperationClaimBl(
        IMapper mapper,
        IOperationClaimDal operationClaimDal
    )
    {
        _operationClaimDal = operationClaimDal;
        _mapper = mapper;
    }

    public IDataResult<List<OperationClaimDto>> GetAll()
    {
        List<OperationClaim> operationClaims = _operationClaimDal.GetAll();
        if (!operationClaims.Any())
            return new ErrorDataResult<List<OperationClaimDto>>(Messages.OperationClaimsNotFound);

        var operationClaimDtos = _mapper.Map<List<OperationClaimDto>>(operationClaims);

        return new SuccessDataResult<List<OperationClaimDto>>(operationClaimDtos, Messages.OperationClaimsListed);
    }

    public IDataResult<OperationClaimDto> GetByOperationClaimName(string operationClaimName)
    {
        OperationClaim operationClaim = _operationClaimDal.GetByOperationClaimName(operationClaimName);
        if (operationClaim is null)
            return new ErrorDataResult<OperationClaimDto>(Messages.OperationClaimNotFound);

        var operationClaimDto = _mapper.Map<OperationClaimDto>(operationClaim);

        return new SuccessDataResult<OperationClaimDto>(operationClaimDto, Messages.OperationClaimListedByOperationClaimName);
    }
}

using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete
{
    public class OperationClaimBl
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
            List<OperationClaim> getOperationClaims = _operationClaimDal.GetAll();

            List<OperationClaimDto> getOperationClaimDtos = FillDtos(getOperationClaims);

            return new SuccessDataResult<List<OperationClaimDto>>(getOperationClaimDtos, Messages.OperationClaimsListed);
        }

        public IDataResult<OperationClaimDto> GetByOperationClaimName(string operationClaimName)
        {
            OperationClaim getOperationClaim = _operationClaimDal.GetByOperationClaimName(operationClaimName);
            if (getOperationClaim == null)
            {
                return new ErrorDataResult<OperationClaimDto>(Messages.OperationClaimNotFound);
            }

            OperationClaimDto getOperationClaimDto = FillDto(getOperationClaim);

            return new SuccessDataResult<OperationClaimDto>(getOperationClaimDto, Messages.OperationClaimListedByOperationClaimName);
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
}

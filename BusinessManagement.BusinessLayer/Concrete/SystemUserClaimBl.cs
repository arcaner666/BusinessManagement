using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class SystemUserClaimBl : ISystemUserClaimBl
{
    private readonly ISystemUserClaimDal _systemUserClaimDal;

    public SystemUserClaimBl(
        ISystemUserClaimDal systemUserClaimDal
    )
    {
        _systemUserClaimDal = systemUserClaimDal;
    }

    public IDataResult<SystemUserClaimDto> Add(SystemUserClaimDto systemUserClaimDto)
    {
        SystemUserClaimDto searchedSystemUserClaimDto = _systemUserClaimDal.GetBySystemUserIdAndOperationClaimId(systemUserClaimDto.SystemUserId, systemUserClaimDto.OperationClaimId);
        if (searchedSystemUserClaimDto is not null)
            return new ErrorDataResult<SystemUserClaimDto>(Messages.SystemUserClaimAlreadyExists);

        systemUserClaimDto.CreatedAt = DateTimeOffset.Now;
        systemUserClaimDto.UpdatedAt = DateTimeOffset.Now;
        long id = _systemUserClaimDal.Add(systemUserClaimDto);
        systemUserClaimDto.SystemUserClaimId = id;

        return new SuccessDataResult<SystemUserClaimDto>(systemUserClaimDto, Messages.SystemUserAdded);
    }

    public IDataResult<List<SystemUserClaimExtDto>> GetExtsBySystemUserId(long systemUserId)
    {
        List<SystemUserClaimExtDto> systemUserClaimExtDtos = _systemUserClaimDal.GetExtsBySystemUserId(systemUserId);
        if (systemUserClaimExtDtos.Count == 0)
            return new ErrorDataResult<List<SystemUserClaimExtDto>>(Messages.SystemUserClaimsNotFound);

        return new SuccessDataResult<List<SystemUserClaimExtDto>>(systemUserClaimExtDtos, Messages.SystemUserExtsListedBySystemUserId);
    }
}

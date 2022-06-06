using AutoMapper;
using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Concrete;

public class SystemUserClaimBl : ISystemUserClaimBl
{
    private readonly IMapper _mapper;
    private readonly ISystemUserClaimDal _systemUserClaimDal;

    public SystemUserClaimBl(
        IMapper mapper,
        ISystemUserClaimDal systemUserClaimDal
    )
    {
        _mapper = mapper;
        _systemUserClaimDal = systemUserClaimDal;
    }

    public IDataResult<SystemUserClaimDto> Add(SystemUserClaimDto systemUserClaimDto)
    {
        SystemUserClaim searchedSystemUserClaim = _systemUserClaimDal.GetBySystemUserIdAndOperationClaimId(systemUserClaimDto.SystemUserId, systemUserClaimDto.OperationClaimId);
        if (searchedSystemUserClaim is not null)
            return new ErrorDataResult<SystemUserClaimDto>(Messages.SystemUserClaimAlreadyExists);

        var addedSystemUserClaim = _mapper.Map<SystemUserClaim>(systemUserClaimDto);

        addedSystemUserClaim.CreatedAt = DateTimeOffset.Now;
        addedSystemUserClaim.UpdatedAt = DateTimeOffset.Now;
        long id = _systemUserClaimDal.Add(addedSystemUserClaim);
        addedSystemUserClaim.SystemUserClaimId = id;

        var addedSystemUserClaimDto = _mapper.Map<SystemUserClaimDto>(addedSystemUserClaim);

        return new SuccessDataResult<SystemUserClaimDto>(addedSystemUserClaimDto, Messages.SystemUserAdded);
    }

    public IDataResult<List<SystemUserClaimExtDto>> GetExtsBySystemUserId(long systemUserId)
    {
        List<SystemUserClaimExt> systemUserClaimExts = _systemUserClaimDal.GetExtsBySystemUserId(systemUserId);
        if (!systemUserClaimExts.Any())
            return new ErrorDataResult<List<SystemUserClaimExtDto>>(Messages.SystemUserClaimsNotFound);

        var systemUserClaimExtDtos = _mapper.Map<List<SystemUserClaimExtDto>>(systemUserClaimExts);

        return new SuccessDataResult<List<SystemUserClaimExtDto>>(systemUserClaimExtDtos, Messages.SystemUserExtsListedBySystemUserId);
    }
}

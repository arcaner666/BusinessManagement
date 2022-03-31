﻿using BusinessManagement.BusinessLayer.Abstract;
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
        SystemUserClaim getSystemUserClaim = _systemUserClaimDal.GetBySystemUserIdAndOperationClaimId(systemUserClaimDto.SystemUserId, systemUserClaimDto.OperationClaimId);
        if (getSystemUserClaim != null)
            return new ErrorDataResult<SystemUserClaimDto>(Messages.SystemUserClaimAlreadyExists);

        SystemUserClaim addSystemUserClaim = new()
        {
            SystemUserId = systemUserClaimDto.SystemUserId,
            OperationClaimId = systemUserClaimDto.OperationClaimId,
            CreatedAt = DateTimeOffset.Now,
            UpdatedAt = DateTimeOffset.Now,
        };
        _systemUserClaimDal.Add(addSystemUserClaim);

        SystemUserClaimDto addSystemUserClaimDto = FillDto(addSystemUserClaim);

        return new SuccessDataResult<SystemUserClaimDto>(addSystemUserClaimDto, Messages.SystemUserAdded);
    }

    public IDataResult<List<SystemUserClaimExtDto>> GetExtsBySystemUserId(long systemUserId)
    {
        List<SystemUserClaim> getSystemUserClaims = _systemUserClaimDal.GetExtsBySystemUserId(systemUserId);
        if (getSystemUserClaims.Count == 0)
            return new ErrorDataResult<List<SystemUserClaimExtDto>>(Messages.SystemUserClaimsNotFound);

        List<SystemUserClaimExtDto> getSystemUserClaimExtDtos = FillExtDtos(getSystemUserClaims);

        return new SuccessDataResult<List<SystemUserClaimExtDto>>(getSystemUserClaimExtDtos, Messages.SystemUserExtsListedBySystemUserId);
    }

    private SystemUserClaimDto FillDto(SystemUserClaim systemUserClaim)
    {
        SystemUserClaimDto systemUserClaimDto = new()
        {
            SystemUserClaimId = systemUserClaim.SystemUserClaimId,
            SystemUserId = systemUserClaim.SystemUserId,
            OperationClaimId = systemUserClaim.OperationClaimId,
            CreatedAt = systemUserClaim.CreatedAt,
            UpdatedAt = systemUserClaim.UpdatedAt,
        };

        return systemUserClaimDto;
    }

    private List<SystemUserClaimDto> FillDtos(List<SystemUserClaim> systemUserClaims)
    {
        List<SystemUserClaimDto> systemUserClaimDtos = systemUserClaims.Select(systemUserClaim => FillDto(systemUserClaim)).ToList();

        return systemUserClaimDtos;
    }

    private SystemUserClaimExtDto FillExtDto(SystemUserClaim systemUserClaim)
    {
        SystemUserClaimExtDto systemUserClaimExtDto = new()
        {
            SystemUserClaimId = systemUserClaim.SystemUserClaimId,
            SystemUserId = systemUserClaim.SystemUserId,
            OperationClaimId = systemUserClaim.OperationClaimId,
            CreatedAt = systemUserClaim.CreatedAt,
            UpdatedAt = systemUserClaim.UpdatedAt,

            OperationClaimName = systemUserClaim.OperationClaim.OperationClaimName,
        };

        return systemUserClaimExtDto;
    }

    private List<SystemUserClaimExtDto> FillExtDtos(List<SystemUserClaim> systemUserClaims)
    {
        List<SystemUserClaimExtDto> systemUserClaimExtDtos = systemUserClaims.Select(systemUserClaim => FillExtDto(systemUserClaim)).ToList();

        return systemUserClaimExtDtos;
    }
}

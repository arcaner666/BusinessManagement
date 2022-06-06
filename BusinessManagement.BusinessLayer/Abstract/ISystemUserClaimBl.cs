using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface ISystemUserClaimBl
{
    IDataResult<SystemUserClaimDto> Add(SystemUserClaimDto systemUserClaimDto);
    IDataResult<List<SystemUserClaimExtDto>> GetExtsBySystemUserId(long systemUserId);
}

using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract
{
    public interface ISystemUserClaimBl
    {
        IDataResult<SystemUserClaimDto> Add(SystemUserClaimDto systemUserClaimDto);
    }
}

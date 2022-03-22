using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract
{
    public interface IAccountBl
    {
        IDataResult<AccountDto> Add(AccountDto accountDto);
    }
}

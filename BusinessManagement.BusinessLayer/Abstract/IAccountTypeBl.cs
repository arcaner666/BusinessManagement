using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IAccountTypeBl
{
    IDataResult<IEnumerable<AccountTypeDto>> GetAll();
    IDataResult<AccountTypeDto> GetByAccountTypeName(string accountTypeName);
    IDataResult<IEnumerable<AccountTypeDto>> GetByAccountTypeNames(AccountTypeNamesDto accountTypeNamesDto);
    IDataResult<AccountTypeDto> GetById(short id);
}

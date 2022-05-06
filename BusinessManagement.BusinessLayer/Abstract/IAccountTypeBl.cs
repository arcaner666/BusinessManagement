using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IAccountTypeBl
{
    IDataResult<List<AccountTypeDto>> GetAll();
    IDataResult<AccountTypeDto> GetByAccountTypeName(string accountTypeName);
    IDataResult<List<AccountTypeDto>> GetByAccountTypeNames(AccountTypeNamesDto accountTypeNamesDto);
    IDataResult<AccountTypeDto> GetById(short id);
}

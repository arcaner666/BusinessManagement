using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IAccountTypeDal
{
    IEnumerable<AccountTypeDto> GetAll();
    AccountTypeDto GetByAccountTypeName(string accountTypeName);
    IEnumerable<AccountTypeDto> GetByAccountTypeNames(string[] accountTypeNames);
    AccountTypeDto GetById(short id);
}

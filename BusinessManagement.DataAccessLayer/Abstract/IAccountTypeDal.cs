using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IAccountTypeDal
{
    List<AccountTypeDto> GetAll();
    AccountTypeDto GetByAccountTypeName(string accountTypeName);
    List<AccountTypeDto> GetByAccountTypeNames(string[] accountTypeNames);
    AccountTypeDto GetById(short id);
}

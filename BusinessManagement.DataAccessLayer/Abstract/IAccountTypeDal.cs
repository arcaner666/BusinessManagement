using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IAccountTypeDal
{
    IEnumerable<AccountType> GetAll();
    AccountType GetByAccountTypeName(string accountTypeName);
    IEnumerable<AccountType> GetByAccountTypeNames(string[] accountTypeNames);
    AccountType GetById(short id);
}

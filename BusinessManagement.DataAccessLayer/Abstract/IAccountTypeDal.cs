using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IAccountTypeDal
{
    List<AccountType> GetAll();
    AccountType GetByAccountTypeName(string accountTypeName);
    List<AccountType> GetByAccountTypeNames(string[] accountTypeNames);
    AccountType GetById(short id);
}

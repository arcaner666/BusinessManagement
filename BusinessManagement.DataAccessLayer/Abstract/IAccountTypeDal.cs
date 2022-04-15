using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IAccountTypeDal
{
    List<AccountType> GetAll();
    AccountType GetByAccountTypeName(string accountTypeName);
    AccountType GetById(short id);
}

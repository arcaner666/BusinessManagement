using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface ICurrencyDal
{
    List<Currency> GetAll();
    Currency GetByCurrencyName(string currencyName);
}

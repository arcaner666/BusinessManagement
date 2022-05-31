using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface ICurrencyDal
{
    List<CurrencyDto> GetAll();
    CurrencyDto GetByCurrencyName(string currencyName);
}

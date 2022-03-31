using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface ICurrencyBl
{
    IDataResult<List<CurrencyDto>> GetAll();
    IDataResult<CurrencyDto> GetByCurrencyName(string currencyName);
}

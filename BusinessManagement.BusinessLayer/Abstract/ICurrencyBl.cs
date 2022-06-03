using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface ICurrencyBl
{
    IDataResult<IEnumerable<CurrencyDto>> GetAll();
    IDataResult<CurrencyDto> GetByCurrencyName(string currencyName);
}

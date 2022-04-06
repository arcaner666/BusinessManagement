using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class CurrencyBl : ICurrencyBl
{
    private readonly ICurrencyDal _currencyDal;

    public CurrencyBl(
        ICurrencyDal currencyDal
    )
    {
        _currencyDal = currencyDal;
    }

    public IDataResult<List<CurrencyDto>> GetAll()
    {
        List<Currency> allCurrencies = _currencyDal.GetAll();
        if (allCurrencies.Count == 0)
            return new ErrorDataResult<List<CurrencyDto>>(Messages.CurrenciesNotFound);

        List<CurrencyDto> allCurrencyDtos = FillDtos(allCurrencies);

        return new SuccessDataResult<List<CurrencyDto>>(allCurrencyDtos, Messages.CurrenciesListed);
    }

    public IDataResult<CurrencyDto> GetByCurrencyName(string currencyName)
    {
        Currency searchedCurrency = _currencyDal.GetByCurrencyName(currencyName);
        if (searchedCurrency is null)
            return new ErrorDataResult<CurrencyDto>(Messages.CurrencyNotFound);

        CurrencyDto searchedCurrencyDto = FillDto(searchedCurrency);

        return new SuccessDataResult<CurrencyDto>(searchedCurrencyDto, Messages.CurrencyListedByCurrencyName);
    }

    private CurrencyDto FillDto(Currency currency)
    {
        CurrencyDto currencyDto = new()
        {
            CurrencyId = currency.CurrencyId,
            CurrencyName = currency.CurrencyName,
            CurrencySymbol = currency.CurrencySymbol,
        };

        return currencyDto;
    }

    private List<CurrencyDto> FillDtos(List<Currency> currencies)
    {
        List<CurrencyDto> currencyDtos = currencies.Select(currency => FillDto(currency)).ToList();

        return currencyDtos;
    }
}

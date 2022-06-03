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

    public IDataResult<IEnumerable<CurrencyDto>> GetAll()
    {
        IEnumerable<CurrencyDto> currencyDtos = _currencyDal.GetAll();
        if (!currencyDtos.Any())
            return new ErrorDataResult<IEnumerable<CurrencyDto>>(Messages.CurrenciesNotFound);

        return new SuccessDataResult<IEnumerable<CurrencyDto>>(currencyDtos, Messages.CurrenciesListed);
    }

    public IDataResult<CurrencyDto> GetByCurrencyName(string currencyName)
    {
        CurrencyDto currencyDto = _currencyDal.GetByCurrencyName(currencyName);
        if (currencyDto is null)
            return new ErrorDataResult<CurrencyDto>(Messages.CurrencyNotFound);

        return new SuccessDataResult<CurrencyDto>(currencyDto, Messages.CurrencyListedByCurrencyName);
    }
}

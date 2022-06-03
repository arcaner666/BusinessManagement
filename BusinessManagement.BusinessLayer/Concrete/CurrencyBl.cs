﻿using AutoMapper;
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
    private readonly IMapper _mapper;

    public CurrencyBl(
        ICurrencyDal currencyDal,
        IMapper mapper
    )
    {
        _currencyDal = currencyDal;
        _mapper = mapper;
    }

    public IDataResult<IEnumerable<CurrencyDto>> GetAll()
    {
        IEnumerable<Currency> currencies = _currencyDal.GetAll();
        if (!currencies.Any())
            return new ErrorDataResult<IEnumerable<CurrencyDto>>(Messages.CurrenciesNotFound);

        var currencyDtos = _mapper.Map<IEnumerable<CurrencyDto>>(currencies);

        return new SuccessDataResult<IEnumerable<CurrencyDto>>(currencyDtos, Messages.CurrenciesListed);
    }

    public IDataResult<CurrencyDto> GetByCurrencyName(string currencyName)
    {
        Currency currency = _currencyDal.GetByCurrencyName(currencyName);
        if (currency is null)
            return new ErrorDataResult<CurrencyDto>(Messages.CurrencyNotFound);

        var currencyDto = _mapper.Map<CurrencyDto>(currency);

        return new SuccessDataResult<CurrencyDto>(currencyDto, Messages.CurrencyListedByCurrencyName);
    }
}

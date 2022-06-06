using AutoMapper;
using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Concrete;

public class CashBl : ICashBl
{
    private readonly ICashDal _cashDal;
    private readonly IMapper _mapper;

    public CashBl(
        ICashDal cashDal,
        IMapper mapper
    )
    {
        _cashDal = cashDal;
        _mapper = mapper;
    }

    public IDataResult<CashDto> Add(CashDto cashDto)
    {
        Cash searchedCash = _cashDal.GetByBusinessIdAndAccountId(cashDto.BusinessId, cashDto.AccountId);
        if (searchedCash is not null)
            return new ErrorDataResult<CashDto>(Messages.CashAlreadyExists);

        var addedCash = _mapper.Map<Cash>(cashDto);

        addedCash.CreatedAt = DateTimeOffset.Now;
        addedCash.UpdatedAt = DateTimeOffset.Now;
        long id = _cashDal.Add(addedCash);
        addedCash.CashId = id;

        var addedCashDto = _mapper.Map<CashDto>(addedCash);

        return new SuccessDataResult<CashDto>(addedCashDto, Messages.CashAdded);
    }

    public IResult Delete(long id)
    {
        var getCashResult = GetById(id);
        if (getCashResult is null)
            return getCashResult;

        _cashDal.Delete(id);

        return new SuccessResult(Messages.CashDeleted);
    }

    public IDataResult<CashDto> GetByAccountId(long accountId)
    {
        Cash cash = _cashDal.GetByAccountId(accountId);
        if (cash is null)
            return new ErrorDataResult<CashDto>(Messages.CashNotFound);

        var cashDto = _mapper.Map<CashDto>(cash);

        return new SuccessDataResult<CashDto>(cashDto, Messages.CashListedByAccountId);
    }

    public IDataResult<List<CashDto>> GetByBusinessId(int businessId)
    {
        List<Cash> cash = _cashDal.GetByBusinessId(businessId);
        if (!cash.Any())
            return new ErrorDataResult<List<CashDto>>(Messages.CashNotFound);

        var cashDtos = _mapper.Map<List<CashDto>>(cash);

        return new SuccessDataResult<List<CashDto>>(cashDtos, Messages.CashListedByBusinessId);
    }

    public IDataResult<CashDto> GetById(long id)
    {
        Cash cash = _cashDal.GetById(id);
        if (cash is null)
            return new ErrorDataResult<CashDto>(Messages.CashNotFound);

        var cashDto = _mapper.Map<CashDto>(cash);

        return new SuccessDataResult<CashDto>(cashDto, Messages.CashListedById);
    }

    public IDataResult<CashExtDto> GetExtByAccountId(long accountId)
    {
        CashExt cashExt = _cashDal.GetExtByAccountId(accountId);
        if (cashExt is null)
            return new ErrorDataResult<CashExtDto>(Messages.CashNotFound);

        var cashExtDto = _mapper.Map<CashExtDto>(cashExt);

        return new SuccessDataResult<CashExtDto>(cashExtDto, Messages.CashExtListedByAccountId);
    }

    public IDataResult<CashExtDto> GetExtById(long id)
    {
        CashExt cashExt = _cashDal.GetExtById(id);
        if (cashExt is null)
            return new ErrorDataResult<CashExtDto>(Messages.CashNotFound);

        var cashExtDto = _mapper.Map<CashExtDto>(cashExt);

        return new SuccessDataResult<CashExtDto>(cashExtDto, Messages.CashExtListedById);
    }

    public IDataResult<List<CashExtDto>> GetExtsByBusinessId(int businessId)
    {
        List<CashExt> cashExts = _cashDal.GetExtsByBusinessId(businessId);
        if (!cashExts.Any())
            return new ErrorDataResult<List<CashExtDto>>(Messages.CashNotFound);

        var cashExtDtos = _mapper.Map<List<CashExtDto>>(cashExts);

        return new SuccessDataResult<List<CashExtDto>>(cashExtDtos, Messages.CashExtsListedByBusinessId);
    }

    public IResult Update(CashDto cashDto)
    {
        Cash cash = _cashDal.GetById(cashDto.CashId);
        if (cash is null)
            return new ErrorDataResult<CashDto>(Messages.CashNotFound);

        cash.UpdatedAt = DateTimeOffset.Now;
        _cashDal.Update(cash);

        return new SuccessResult(Messages.CashUpdated);
    }
}

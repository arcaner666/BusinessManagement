using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class CashBl : ICashBl
{
    private readonly ICashDal _cashDal;

    public CashBl(
        ICashDal cashDal
    )
    {
        _cashDal = cashDal;
    }

    public IDataResult<CashDto> Add(CashDto cashDto)
    {
        CashDto searchedCashDto = _cashDal.GetByBusinessIdAndAccountId(cashDto.BusinessId, cashDto.AccountId);
        if (searchedCashDto is not null)
        {
            return new ErrorDataResult<CashDto>(Messages.CashAlreadyExists);
        }

        cashDto.CreatedAt = DateTimeOffset.Now;
        cashDto.UpdatedAt = DateTimeOffset.Now;
        long id = _cashDal.Add(cashDto);
        cashDto.CashId = id;

        return new SuccessDataResult<CashDto>(cashDto, Messages.CashAdded);
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
        CashDto cashDto = _cashDal.GetByAccountId(accountId);
        if (cashDto is null)
            return new ErrorDataResult<CashDto>(Messages.CashNotFound);

        return new SuccessDataResult<CashDto>(cashDto, Messages.CashListedByAccountId);
    }

    public IDataResult<IEnumerable<CashDto>> GetByBusinessId(int businessId)
    {
        IEnumerable<CashDto> cashDto = _cashDal.GetByBusinessId(businessId);
        if (!cashDto.Any())
            return new ErrorDataResult<IEnumerable<CashDto>>(Messages.CashNotFound);

        return new SuccessDataResult<IEnumerable<CashDto>>(cashDto, Messages.CashListedByBusinessId);
    }

    public IDataResult<CashDto> GetById(long id)
    {
        CashDto cashDto = _cashDal.GetById(id);
        if (cashDto is null)
            return new ErrorDataResult<CashDto>(Messages.CashNotFound);

        return new SuccessDataResult<CashDto>(cashDto, Messages.CashListedById);
    }

    public IResult Update(CashDto cashDto)
    {
        var searchedCashResult = GetById(cashDto.CashId);
        if (!searchedCashResult.Success)
            return searchedCashResult;

        searchedCashResult.Data.UpdatedAt = DateTimeOffset.Now;
        _cashDal.Update(searchedCashResult.Data);

        return new SuccessResult(Messages.CashUpdated);
    }
}

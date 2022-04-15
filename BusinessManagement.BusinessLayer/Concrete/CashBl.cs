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
        Cash searchedCash = _cashDal.GetByBusinessIdAndAccountId(cashDto.BusinessId, cashDto.AccountId);
        if (searchedCash is not null)
        {
            return new ErrorDataResult<CashDto>(Messages.CashAlreadyExists);
        }

        Cash addedCash = new()
        {
            BusinessId = cashDto.BusinessId,
            BranchId = cashDto.BranchId,
            AccountId = cashDto.AccountId,
            CurrencyId = cashDto.CurrencyId,
            CreatedAt = DateTimeOffset.Now,
            UpdatedAt = DateTimeOffset.Now,
        };
        _cashDal.Add(addedCash);

        CashDto addedCashDto = FillDto(addedCash);

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

    public IDataResult<List<CashDto>> GetByBusinessId(int businessId)
    {
        List<Cash> searchedCash = _cashDal.GetByBusinessId(businessId);
        if (searchedCash.Count == 0)
            return new ErrorDataResult<List<CashDto>>(Messages.CashNotFound);

        List<CashDto> searchedCashDtos = FillDtos(searchedCash);

        return new SuccessDataResult<List<CashDto>>(searchedCashDtos, Messages.CashListedByBusinessId);
    }

    public IDataResult<CashDto> GetById(long id)
    {
        Cash searchedCash = _cashDal.GetById(id);
        if (searchedCash is null)
            return new ErrorDataResult<CashDto>(Messages.CashNotFound);

        CashDto searchedCashDto = FillDto(searchedCash);

        return new SuccessDataResult<CashDto>(searchedCashDto, Messages.CashListedById);
    }

    public IResult Update(CashDto cashDto)
    {
        Cash searchedCash = _cashDal.GetById(cashDto.CashId);
        if (searchedCash is null)
            return new ErrorDataResult<AccountDto>(Messages.CashNotFound);

        searchedCash.UpdatedAt = DateTimeOffset.Now;
        _cashDal.Update(searchedCash);

        return new SuccessResult(Messages.CashUpdated);
    }

    private CashDto FillDto(Cash cash)
    {
        CashDto cashDto = new()
        {
            CashId = cash.CashId,
            BusinessId = cash.BusinessId,
            BranchId = cash.BranchId,
            AccountId = cash.AccountId,
            CurrencyId = cash.CurrencyId,
            CreatedAt = cash.CreatedAt,
            UpdatedAt = cash.UpdatedAt,
        };

        return cashDto;
    }

    private List<CashDto> FillDtos(List<Cash> cash)
    {
        List<CashDto> cashDtos = cash.Select(cash => FillDto(cash)).ToList();

        return cashDtos;
    }
}

using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class BankBl : IBankBl
{
    private readonly IBankDal _bankDal;

    public BankBl(
        IBankDal bankDal
    )
    {
        _bankDal = bankDal;
    }

    public IDataResult<BankDto> Add(BankDto bankDto)
    {
        BankDto searchedBankDto = _bankDal.GetByBusinessIdAndIban(bankDto.BusinessId, bankDto.Iban);
        if (searchedBankDto is not null)
            return new ErrorDataResult<BankDto>(Messages.BankAlreadyExists);

        bankDto.CreatedAt = DateTimeOffset.Now;
        bankDto.UpdatedAt = DateTimeOffset.Now;
        long id = _bankDal.Add(bankDto);
        bankDto.BankId = id;

        return new SuccessDataResult<BankDto>(bankDto, Messages.BankAdded);
    }

    public IResult Delete(long id)
    {
        var getBankResult = GetById(id);
        if (getBankResult is null)
            return getBankResult;

        _bankDal.Delete(id);

        return new SuccessResult(Messages.BankDeleted);
    }

    public IDataResult<BankDto> GetByAccountId(long accountId)
    {
        BankDto bankDto = _bankDal.GetByAccountId(accountId);
        if (bankDto is null)
            return new ErrorDataResult<BankDto>(Messages.BankNotFound);

        return new SuccessDataResult<BankDto>(bankDto, Messages.BankListedByAccountId);
    }

    public IDataResult<List<BankDto>> GetByBusinessId(int businessId)
    {
        List<BankDto> bankDtos = _bankDal.GetByBusinessId(businessId);
        if (bankDtos.Count == 0)
            return new ErrorDataResult<List<BankDto>>(Messages.BankNotFound);

        return new SuccessDataResult<List<BankDto>>(bankDtos, Messages.BankListedByBusinessId);
    }

    public IDataResult<BankDto> GetById(long id)
    {
        BankDto bankDto = _bankDal.GetById(id);
        if (bankDto is null)
            return new ErrorDataResult<BankDto>(Messages.BankNotFound);

        return new SuccessDataResult<BankDto>(bankDto, Messages.BankListedById);
    }

    public IResult Update(BankDto bankDto)
    {
        var searchedBankResult = GetById(bankDto.BankId);
        if (!searchedBankResult.Success)
            return searchedBankResult;

        searchedBankResult.Data.BankName = bankDto.BankName;
        searchedBankResult.Data.BankBranchName = bankDto.BankBranchName;
        searchedBankResult.Data.BankCode = bankDto.BankCode;
        searchedBankResult.Data.BankBranchCode = bankDto.BankBranchCode;
        searchedBankResult.Data.BankAccountCode = bankDto.BankAccountCode;
        searchedBankResult.Data.Iban = bankDto.Iban;
        searchedBankResult.Data.OfficerName = bankDto.OfficerName;
        searchedBankResult.Data.StandartMaturity = bankDto.StandartMaturity;
        searchedBankResult.Data.UpdatedAt = DateTimeOffset.Now;
        _bankDal.Update(searchedBankResult.Data);

        return new SuccessResult(Messages.BankUpdated);
    }
}

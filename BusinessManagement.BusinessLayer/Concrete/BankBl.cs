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
        var getBankDtoResult = GetById(id);
        if (getBankDtoResult is null)
            return getBankDtoResult;

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
        var searchedBankDtoResult = GetById(bankDto.BankId);
        if (!searchedBankDtoResult.Success)
            return searchedBankDtoResult;

        searchedBankDtoResult.Data.BankName = bankDto.BankName;
        searchedBankDtoResult.Data.BankBranchName = bankDto.BankBranchName;
        searchedBankDtoResult.Data.BankCode = bankDto.BankCode;
        searchedBankDtoResult.Data.BankBranchCode = bankDto.BankBranchCode;
        searchedBankDtoResult.Data.BankAccountCode = bankDto.BankAccountCode;
        searchedBankDtoResult.Data.Iban = bankDto.Iban;
        searchedBankDtoResult.Data.OfficerName = bankDto.OfficerName;
        searchedBankDtoResult.Data.StandartMaturity = bankDto.StandartMaturity;
        searchedBankDtoResult.Data.UpdatedAt = DateTimeOffset.Now;
        _bankDal.Update(searchedBankDtoResult.Data);

        return new SuccessResult(Messages.BankUpdated);
    }
}

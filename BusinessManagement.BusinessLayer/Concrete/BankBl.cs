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
        Bank searchedBank = _bankDal.GetByBusinessIdAndIban(bankDto.BusinessId, bankDto.Iban);
        if (searchedBank is not null)
        {
            return new ErrorDataResult<BankDto>(Messages.BankAlreadyExists);
        }

        Bank addedBank = new()
        {
            BusinessId = bankDto.BusinessId,
            BranchId = bankDto.BranchId,
            AccountId = bankDto.AccountId,
            FullAddressId = bankDto.FullAddressId,
            CurrencyId = bankDto.CurrencyId,
            BankName = bankDto.BankName,
            BankBranchName = bankDto.BankBranchName,
            BankCode = bankDto.BankCode,
            BankBranchCode = bankDto.BankBranchCode,
            BankAccountCode = bankDto.BankAccountCode,
            Iban = bankDto.Iban,
            OfficerName = bankDto.OfficerName,
            StandartMaturity = bankDto.StandartMaturity,
            CreatedAt = DateTimeOffset.Now,
            UpdatedAt = DateTimeOffset.Now,
        };
        _bankDal.Add(addedBank);

        BankDto addedBankDto = FillDto(addedBank);

        return new SuccessDataResult<BankDto>(addedBankDto, Messages.BankAdded);
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
        Bank searchedBank = _bankDal.GetByAccountId(accountId);
        if (searchedBank is null)
            return new ErrorDataResult<BankDto>(Messages.BankNotFound);

        BankDto searchedBankDto = FillDto(searchedBank);

        return new SuccessDataResult<BankDto>(searchedBankDto, Messages.BankListedByAccountId);
    }

    public IDataResult<List<BankDto>> GetByBusinessId(int businessId)
    {
        List<Bank> searchedBank = _bankDal.GetByBusinessId(businessId);
        if (searchedBank.Count == 0)
            return new ErrorDataResult<List<BankDto>>(Messages.BankNotFound);

        List<BankDto> searchedBankDtos = FillDtos(searchedBank);

        return new SuccessDataResult<List<BankDto>>(searchedBankDtos, Messages.BankListedByBusinessId);
    }

    public IDataResult<BankDto> GetById(long id)
    {
        Bank searchedBank = _bankDal.GetById(id);
        if (searchedBank is null)
            return new ErrorDataResult<BankDto>(Messages.BankNotFound);

        BankDto searchedBankDto = FillDto(searchedBank);

        return new SuccessDataResult<BankDto>(searchedBankDto, Messages.BankListedById);
    }

    public IResult Update(BankDto bankDto)
    {
        Bank searchedBank = _bankDal.GetById(bankDto.BankId);
        if (searchedBank is null)
            return new ErrorDataResult<AccountDto>(Messages.BankNotFound);

        searchedBank.BankName = bankDto.BankName;
        searchedBank.BankBranchName = bankDto.BankBranchName;
        searchedBank.BankCode = bankDto.BankCode;
        searchedBank.BankBranchCode = bankDto.BankBranchCode;
        searchedBank.BankAccountCode = bankDto.BankAccountCode;
        searchedBank.Iban = bankDto.Iban;
        searchedBank.OfficerName = bankDto.OfficerName;
        searchedBank.StandartMaturity = bankDto.StandartMaturity;
        searchedBank.UpdatedAt = DateTimeOffset.Now;
        _bankDal.Update(searchedBank);

        return new SuccessResult(Messages.BankUpdated);
    }

    private BankDto FillDto(Bank bank)
    {
        BankDto bankDto = new()
        {
            BankId = bank.BankId,
            BusinessId = bank.BusinessId,
            BranchId = bank.BranchId,
            AccountId = bank.AccountId,
            FullAddressId = bank.FullAddressId,
            CurrencyId = bank.CurrencyId,
            BankName = bank.BankName,
            BankBranchName = bank.BankBranchName,
            BankCode = bank.BankCode,
            BankBranchCode = bank.BankBranchCode,
            BankAccountCode = bank.BankAccountCode,
            Iban = bank.Iban,
            OfficerName = bank.OfficerName,
            StandartMaturity = bank.StandartMaturity,
            CreatedAt = bank.CreatedAt,
            UpdatedAt = bank.UpdatedAt,
        };

        return bankDto;
    }

    private List<BankDto> FillDtos(List<Bank> bank)
    {
        List<BankDto> bankDtos = bank.Select(bank => FillDto(bank)).ToList();

        return bankDtos;
    }
}

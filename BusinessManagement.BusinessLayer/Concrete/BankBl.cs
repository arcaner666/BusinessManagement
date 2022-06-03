using AutoMapper;
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
    private readonly IMapper _mapper;

    public BankBl(
        IBankDal bankDal,
        IMapper mapper
    )
    {
        _bankDal = bankDal;
        _mapper = mapper;
    }

    public IDataResult<BankDto> Add(BankDto bankDto)
    {
        Bank searchedBank = _bankDal.GetByBusinessIdAndIban(bankDto.BusinessId, bankDto.Iban);
        if (searchedBank is not null)
            return new ErrorDataResult<BankDto>(Messages.BankAlreadyExists);

        var addedBank = _mapper.Map<Bank>(bankDto);

        addedBank.CreatedAt = DateTimeOffset.Now;
        addedBank.UpdatedAt = DateTimeOffset.Now;
        long id = _bankDal.Add(addedBank);
        addedBank.BankId = id;

        var addedBankDto = _mapper.Map<BankDto>(addedBank);

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
        Bank bank = _bankDal.GetByAccountId(accountId);
        if (bank is null)
            return new ErrorDataResult<BankDto>(Messages.BankNotFound);

        var bankDto = _mapper.Map<BankDto>(bank);

        return new SuccessDataResult<BankDto>(bankDto, Messages.BankListedByAccountId);
    }

    public IDataResult<IEnumerable<BankDto>> GetByBusinessId(int businessId)
    {
        IEnumerable<Bank> banks = _bankDal.GetByBusinessId(businessId);
        if (!banks.Any())
            return new ErrorDataResult<IEnumerable<BankDto>>(Messages.BankNotFound);

        var bankDtos = _mapper.Map<IEnumerable<BankDto>>(banks);

        return new SuccessDataResult<IEnumerable<BankDto>>(bankDtos, Messages.BankListedByBusinessId);
    }

    public IDataResult<BankDto> GetById(long id)
    {
        Bank bank = _bankDal.GetById(id);
        if (bank is null)
            return new ErrorDataResult<BankDto>(Messages.BankNotFound);

        var bankDto = _mapper.Map<BankDto>(bank);

        return new SuccessDataResult<BankDto>(bankDto, Messages.BankListedById);
    }

    public IResult Update(BankDto bankDto)
    {
        Bank bank = _bankDal.GetById(bankDto.BankId);
        if (bank is null)
            return new ErrorDataResult<BankDto>(Messages.BankNotFound);

        bank.BankName = bankDto.BankName;
        bank.BankBranchName = bankDto.BankBranchName;
        bank.BankCode = bankDto.BankCode;
        bank.BankBranchCode = bankDto.BankBranchCode;
        bank.BankAccountCode = bankDto.BankAccountCode;
        bank.Iban = bankDto.Iban;
        bank.OfficerName = bankDto.OfficerName;
        bank.StandartMaturity = bankDto.StandartMaturity;
        bank.UpdatedAt = DateTimeOffset.Now;
        _bankDal.Update(bank);

        return new SuccessResult(Messages.BankUpdated);
    }
}

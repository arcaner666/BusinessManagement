using AutoMapper;
using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
using BusinessManagement.Entities.ExtendedDatabaseModels;

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

    public IDataResult<List<BankDto>> GetByBusinessId(int businessId)
    {
        List<Bank> banks = _bankDal.GetByBusinessId(businessId);
        if (!banks.Any())
            return new ErrorDataResult<List<BankDto>>(Messages.BankNotFound);

        var bankDtos = _mapper.Map<List<BankDto>>(banks);

        return new SuccessDataResult<List<BankDto>>(bankDtos, Messages.BankListedByBusinessId);
    }

    public IDataResult<BankDto> GetById(long id)
    {
        Bank bank = _bankDal.GetById(id);
        if (bank is null)
            return new ErrorDataResult<BankDto>(Messages.BankNotFound);

        var bankDto = _mapper.Map<BankDto>(bank);

        return new SuccessDataResult<BankDto>(bankDto, Messages.BankListedById);
    }

    public IDataResult<BankExtDto> GetExtByAccountId(long accountId)
    {
        BankExt bankExt = _bankDal.GetExtByAccountId(accountId);
        if (bankExt is null)
            return new ErrorDataResult<BankExtDto>(Messages.BankNotFound);

        var bankExtDto = _mapper.Map<BankExtDto>(bankExt);

        return new SuccessDataResult<BankExtDto>(bankExtDto, Messages.BankExtListedByAccountId);
    }

    public IDataResult<BankExtDto> GetExtById(long id)
    {
        BankExt bankExt = _bankDal.GetExtById(id);
        if (bankExt is null)
            return new ErrorDataResult<BankExtDto>(Messages.BankNotFound);

        var bankExtDto = _mapper.Map<BankExtDto>(bankExt);

        return new SuccessDataResult<BankExtDto>(bankExtDto, Messages.BankExtListedById);
    }

    public IDataResult<List<BankExtDto>> GetExtsByBusinessId(int businessId)
    {
        List<BankExt> bankExts = _bankDal.GetExtsByBusinessId(businessId);
        if (!bankExts.Any())
            return new ErrorDataResult<List<BankExtDto>>(Messages.BankNotFound);

        var bankExtDtos = _mapper.Map<List<BankExtDto>>(bankExts);

        return new SuccessDataResult<List<BankExtDto>>(bankExtDtos, Messages.BankExtsListedByBusinessId);
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

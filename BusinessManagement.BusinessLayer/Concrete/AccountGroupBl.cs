using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class AccountGroupBl : IAccountGroupBl
{
    private readonly IAccountGroupDal _accountGroupDal;

    public AccountGroupBl(
        IAccountGroupDal accountGroupDal
    )
    {
        _accountGroupDal = accountGroupDal;
    }

    public IDataResult<IEnumerable<AccountGroupDto>> GetAll()
    {
        //return new ErrorDataResult<IEnumerable<AccountGroupDto>>(Messages.AccountGroupsNotFound);
        //throw new Exception("new Exception");

        IEnumerable<AccountGroupDto> accountGroupDtos = _accountGroupDal.GetAll();
        if (!accountGroupDtos.Any())
            return new ErrorDataResult<IEnumerable<AccountGroupDto>>(Messages.AccountGroupsNotFound);

        return new SuccessDataResult<IEnumerable<AccountGroupDto>>(accountGroupDtos, Messages.AccountGroupsListed);
    }

    public IDataResult<AccountGroupDto> GetByAccountGroupCode(string accountGroupCode)
    {
        AccountGroupDto accountGroupDto = _accountGroupDal.GetByAccountGroupCode(accountGroupCode);
        if (accountGroupDto is null)
            return new ErrorDataResult<AccountGroupDto>(Messages.AccountGroupNotFound);

        return new SuccessDataResult<AccountGroupDto>(accountGroupDto, Messages.AccountGroupListedByAccountGroupCode);
    }

    public IDataResult<IEnumerable<AccountGroupDto>> GetByAccountGroupCodes(AccountGroupCodesDto accountGroupCodesDto)
    {
        IEnumerable<AccountGroupDto> accountGroupDtos = _accountGroupDal.GetByAccountGroupCodes(accountGroupCodesDto.AccountGroupCodes);
        if (!accountGroupDtos.Any())
            return new ErrorDataResult<IEnumerable<AccountGroupDto>>(Messages.AccountGroupsNotFound);

        return new SuccessDataResult<IEnumerable<AccountGroupDto>>(accountGroupDtos, Messages.AccountGroupsListedByAccountGroupCodes);
    }

    public IDataResult<AccountGroupDto> GetById(short id)
    {
        AccountGroupDto accountGroupDto = _accountGroupDal.GetById(id);
        if (accountGroupDto is null)
            return new ErrorDataResult<AccountGroupDto>(Messages.AccountGroupNotFound);

        return new SuccessDataResult<AccountGroupDto>(accountGroupDto, Messages.AccountGroupListedById);
    }
}

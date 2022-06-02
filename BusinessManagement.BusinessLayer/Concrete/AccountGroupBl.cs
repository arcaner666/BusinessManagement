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

    public IDataResult<List<AccountGroupDto>> GetAll()
    {
        return new ErrorDataResult<List<AccountGroupDto>>(Messages.AccountGroupsNotFound);
        
        List<AccountGroupDto> accountGroupDtos = _accountGroupDal.GetAll();
        if (accountGroupDtos.Count == 0)
            return new ErrorDataResult<List<AccountGroupDto>>(Messages.AccountGroupsNotFound);

        return new SuccessDataResult<List<AccountGroupDto>>(accountGroupDtos, Messages.AccountGroupsListed);
    }

    public IDataResult<AccountGroupDto> GetByAccountGroupCode(string accountGroupCode)
    {
        AccountGroupDto accountGroupDto = _accountGroupDal.GetByAccountGroupCode(accountGroupCode);
        if (accountGroupDto is null)
            return new ErrorDataResult<AccountGroupDto>(Messages.AccountGroupNotFound);

        return new SuccessDataResult<AccountGroupDto>(accountGroupDto, Messages.AccountGroupListedByAccountGroupCode);
    }

    public IDataResult<List<AccountGroupDto>> GetByAccountGroupCodes(AccountGroupCodesDto accountGroupCodesDto)
    {
        List<AccountGroupDto> accountGroupDtos = _accountGroupDal.GetByAccountGroupCodes(accountGroupCodesDto.AccountGroupCodes);
        if (accountGroupDtos.Count == 0)
            return new ErrorDataResult<List<AccountGroupDto>>(Messages.AccountGroupsNotFound);

        return new SuccessDataResult<List<AccountGroupDto>>(accountGroupDtos, Messages.AccountGroupsListedByAccountGroupCodes);
    }

    public IDataResult<AccountGroupDto> GetById(short id)
    {
        AccountGroupDto accountGroupDto = _accountGroupDal.GetById(id);
        if (accountGroupDto is null)
            return new ErrorDataResult<AccountGroupDto>(Messages.AccountGroupNotFound);

        return new SuccessDataResult<AccountGroupDto>(accountGroupDto, Messages.AccountGroupListedById);
    }
}

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
        List<AccountGroup> allAccountGroups = _accountGroupDal.GetAll();
        if (allAccountGroups.Count == 0)
            return new ErrorDataResult<List<AccountGroupDto>>(Messages.AccountGroupsNotFound);

        List<AccountGroupDto> allAccountGroupDtos = FillDtos(allAccountGroups);

        return new SuccessDataResult<List<AccountGroupDto>>(allAccountGroupDtos, Messages.AccountGroupsListed);
    }

    public IDataResult<AccountGroupDto> GetByAccountGroupCode(string accountGroupCode)
    {
        AccountGroup searchedAccountGroup = _accountGroupDal.GetByAccountGroupCode(accountGroupCode);
        if (searchedAccountGroup is null)
            return new ErrorDataResult<AccountGroupDto>(Messages.AccountGroupNotFound);

        AccountGroupDto searchedAccountGroupDto = FillDto(searchedAccountGroup);

        return new SuccessDataResult<AccountGroupDto>(searchedAccountGroupDto, Messages.AccountGroupListedByAccountGroupCode);
    }

    public IDataResult<List<AccountGroupDto>> GetByAccountGroupCodes(AccountGroupCodesDto accountGroupCodesDto)
    {
        List<AccountGroup> searchedAccountGroups = _accountGroupDal.GetByAccountGroupCodes(accountGroupCodesDto.AccountGroupCodes);
        if (searchedAccountGroups.Count == 0)
            return new ErrorDataResult<List<AccountGroupDto>>(Messages.AccountGroupsNotFound);

        List<AccountGroupDto> searchedAccountGroupDtos = FillDtos(searchedAccountGroups);

        return new SuccessDataResult<List<AccountGroupDto>>(searchedAccountGroupDtos, Messages.AccountGroupsListedByAccountGroupCodes);
    }

    public IDataResult<AccountGroupDto> GetById(short id)
    {
        AccountGroup searchedAccountGroup = _accountGroupDal.GetById(id);
        if (searchedAccountGroup is null)
            return new ErrorDataResult<AccountGroupDto>(Messages.AccountGroupNotFound);

        AccountGroupDto searchedAccountGroupDto = FillDto(searchedAccountGroup);

        return new SuccessDataResult<AccountGroupDto>(searchedAccountGroupDto, Messages.AccountGroupListedById);
    }

    private AccountGroupDto FillDto(AccountGroup accountGroup)
    {
        AccountGroupDto accountGroupDto = new()
        {
            AccountGroupId = accountGroup.AccountGroupId,
            AccountGroupName = accountGroup.AccountGroupName,
            AccountGroupCode = accountGroup.AccountGroupCode,
        };

        return accountGroupDto;
    }

    private List<AccountGroupDto> FillDtos(List<AccountGroup> accountGroups)
    {
        List<AccountGroupDto> accountGroupDtos = accountGroups.Select(accountGroup => FillDto(accountGroup)).ToList();

        return accountGroupDtos;
    }
}

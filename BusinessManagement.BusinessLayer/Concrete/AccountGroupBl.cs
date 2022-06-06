using AutoMapper;
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
    private readonly IMapper _mapper;

    public AccountGroupBl(
        IAccountGroupDal accountGroupDal,
        IMapper mapper
    )
    {
        _accountGroupDal = accountGroupDal;
        _mapper = mapper;
    }

    public IDataResult<List<AccountGroupDto>> GetAll()
    {
        //throw new Exception("new Exception");

        List<AccountGroup> accountGroups = _accountGroupDal.GetAll();
        if (!accountGroups.Any())
            return new ErrorDataResult<List<AccountGroupDto>>(Messages.AccountGroupsNotFound);

        var accountGroupDtos = _mapper.Map<List<AccountGroupDto>>(accountGroups);

        return new SuccessDataResult<List<AccountGroupDto>>(accountGroupDtos, Messages.AccountGroupsListed);
    }

    public IDataResult<AccountGroupDto> GetByAccountGroupCode(string accountGroupCode)
    {
        AccountGroup accountGroup = _accountGroupDal.GetByAccountGroupCode(accountGroupCode);
        if (accountGroup is null)
            return new ErrorDataResult<AccountGroupDto>(Messages.AccountGroupNotFound);

        var accountGroupDto = _mapper.Map<AccountGroupDto>(accountGroup);

        return new SuccessDataResult<AccountGroupDto>(accountGroupDto, Messages.AccountGroupListedByAccountGroupCode);
    }

    public IDataResult<List<AccountGroupDto>> GetByAccountGroupCodes(AccountGroupCodesDto accountGroupCodesDto)
    {
        List<AccountGroup> accountGroups = _accountGroupDal.GetByAccountGroupCodes(accountGroupCodesDto.AccountGroupCodes);
        if (!accountGroups.Any())
            return new ErrorDataResult<List<AccountGroupDto>>(Messages.AccountGroupsNotFound);

        var accountGroupDtos = _mapper.Map<List<AccountGroupDto>>(accountGroups);

        return new SuccessDataResult<List<AccountGroupDto>>(accountGroupDtos, Messages.AccountGroupsListedByAccountGroupCodes);
    }

    public IDataResult<AccountGroupDto> GetById(short id)
    {
        AccountGroup accountGroup = _accountGroupDal.GetById(id);
        if (accountGroup is null)
            return new ErrorDataResult<AccountGroupDto>(Messages.AccountGroupNotFound);

        var accountGroupDto = _mapper.Map<AccountGroupDto>(accountGroup);

        return new SuccessDataResult<AccountGroupDto>(accountGroupDto, Messages.AccountGroupListedById);
    }
}

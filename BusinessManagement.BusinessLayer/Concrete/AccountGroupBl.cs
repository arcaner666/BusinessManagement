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

    public IDataResult<IEnumerable<AccountGroupDto>> GetAll()
    {
        //throw new Exception("new Exception");

        IEnumerable<AccountGroup> accountGroups = _accountGroupDal.GetAll();
        if (!accountGroups.Any())
            return new ErrorDataResult<IEnumerable<AccountGroupDto>>(Messages.AccountGroupsNotFound);

        var accountGroupDtos = _mapper.Map<IEnumerable<AccountGroupDto>>(accountGroups);

        return new SuccessDataResult<IEnumerable<AccountGroupDto>>(accountGroupDtos, Messages.AccountGroupsListed);
    }

    public IDataResult<AccountGroupDto> GetByAccountGroupCode(string accountGroupCode)
    {
        AccountGroup accountGroup = _accountGroupDal.GetByAccountGroupCode(accountGroupCode);
        if (accountGroup is null)
            return new ErrorDataResult<AccountGroupDto>(Messages.AccountGroupNotFound);

        var accountGroupDto = _mapper.Map<AccountGroupDto>(accountGroup);

        return new SuccessDataResult<AccountGroupDto>(accountGroupDto, Messages.AccountGroupListedByAccountGroupCode);
    }

    public IDataResult<IEnumerable<AccountGroupDto>> GetByAccountGroupCodes(AccountGroupCodesDto accountGroupCodesDto)
    {
        IEnumerable<AccountGroup> accountGroups = _accountGroupDal.GetByAccountGroupCodes(accountGroupCodesDto.AccountGroupCodes);
        if (!accountGroups.Any())
            return new ErrorDataResult<IEnumerable<AccountGroupDto>>(Messages.AccountGroupsNotFound);

        var accountGroupDtos = _mapper.Map<IEnumerable<AccountGroupDto>>(accountGroups);

        return new SuccessDataResult<IEnumerable<AccountGroupDto>>(accountGroupDtos, Messages.AccountGroupsListedByAccountGroupCodes);
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

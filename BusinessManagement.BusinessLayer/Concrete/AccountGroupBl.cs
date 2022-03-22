using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete
{
    public class AccountGroupBl
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
            List<AccountGroup> getAccountGroups = _accountGroupDal.GetAll();

            List<AccountGroupDto> getAccountGroupDtos = FillDtos(getAccountGroups);

            return new SuccessDataResult<List<AccountGroupDto>>(getAccountGroupDtos, Messages.AccountGroupsListed);
        }

        public IDataResult<AccountGroupDto> GetByAccountGroupCode(string accountGroupCode)
        {
            AccountGroup getAccountGroup = _accountGroupDal.GetByAccountGroupCode(accountGroupCode);
            if (getAccountGroup == null)
            {
                return new ErrorDataResult<AccountGroupDto>(Messages.AccountGroupNotFound);
            }

            AccountGroupDto getAccountGroupDto = FillDto(getAccountGroup);

            return new SuccessDataResult<AccountGroupDto>(getAccountGroupDto, Messages.AccountGroupListedByAccountGroupCode);
        }

        public IDataResult<AccountGroupDto> GetById(short id)
        {
            AccountGroup getAccountGroup = _accountGroupDal.GetById(id);
            if (getAccountGroup == null)
            {
                return new ErrorDataResult<AccountGroupDto>(Messages.AccountGroupNotFound);
            }

            AccountGroupDto getAccountGroupDto = FillDto(getAccountGroup);

            return new SuccessDataResult<AccountGroupDto>(getAccountGroupDto, Messages.AccountGroupListedById);
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
}

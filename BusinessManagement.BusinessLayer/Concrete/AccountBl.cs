using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete
{
    public class AccountBl
    {
        private readonly IAccountDal _accountDal;

        public AccountBl(
            IAccountDal accountDal
        )
        {
            _accountDal = accountDal;
        }

        public IDataResult<AccountDto> Add(AccountDto accountDto)
        {
            Account getAccount = _accountDal.GetByBusinessIdAndAccountCode(accountDto.BusinessId, accountDto.AccountCode);
            if (getAccount != null)
            {
                return new ErrorDataResult<AccountDto>(Messages.AccountAlreadyExists);
            }

            Account addAccount = new()
            {
                BusinessId = accountDto.BusinessId,
                BranchId = accountDto.BranchId,
                AccountGroupId = accountDto.AccountGroupId,
                CurrencyId = accountDto.CurrencyId,
                AccountOrder = 1,
                AccountName = "TL Kasası",
                AccountCode = "10000000100000001",
                TaxOffice = "",
                TaxNumber = 0,
                IdentityNumber = 0,
                DebitBalance = 0,
                CreditBalance = 0,
                Balance = 0,
                Limit = 0,
                StandartMaturity = 0,
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = DateTimeOffset.Now,
            };
            _accountDal.Add(addAccount);

            AccountDto addAccountDto = FillDto(addAccount);

            return new SuccessDataResult<AccountDto>(addAccountDto, Messages.AccountAdded);
        }
        
        private AccountDto FillDto(Account account)
        {
            AccountDto accountDto = new()
            {
                AccountId = account.AccountId,
                BusinessId = account.BusinessId,
                FullAddressId = account.FullAddressId,
                AccountOrder = account.AccountOrder,
                AccountName = account.AccountName,
                AccountCode = account.AccountCode,
                CreatedAt = account.CreatedAt,
                UpdatedAt = account.UpdatedAt,
            };

            return accountDto;
        }

        private List<AccountDto> FillDtos(List<Account> accountes)
        {
            List<AccountDto> accountDtos = accountes.Select(account => FillDto(account)).ToList();

            return accountDtos;
        }
    }
}

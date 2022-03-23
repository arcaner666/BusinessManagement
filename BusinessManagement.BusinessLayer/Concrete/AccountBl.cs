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
                AccountOrder = accountDto.AccountOrder,
                AccountName = accountDto.AccountName,
                AccountCode = accountDto.AccountCode,
                TaxOffice = "",
                TaxNumber = 0,
                IdentityNumber = 0,
                DebitBalance = 0,
                CreditBalance = 0,
                Balance = 0,
                Limit = accountDto.Limit,
                StandartMaturity = accountDto.StandartMaturity,
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
                BusinessId = account.BusinessId,
                BranchId = account.BranchId,
                AccountGroupId = account.AccountGroupId,
                CurrencyId = account.CurrencyId,
                AccountOrder = account.AccountOrder,
                AccountName = account.AccountName,
                AccountCode = account.AccountCode,
                TaxOffice = account.TaxOffice,
                TaxNumber = account.TaxNumber,
                IdentityNumber = account.IdentityNumber,
                DebitBalance = account.DebitBalance,
                CreditBalance = account.CreditBalance,
                Balance = account.Balance,
                Limit = account.Limit,
                StandartMaturity = account.StandartMaturity,
                CreatedAt = account.CreatedAt,
                UpdatedAt = account.UpdatedAt,
            };

            return accountDto;
        }

        private List<AccountDto> FillDtos(List<Account> accounts)
        {
            List<AccountDto> accountDtos = accounts.Select(account => FillDto(account)).ToList();

            return accountDtos;
        }
    }
}

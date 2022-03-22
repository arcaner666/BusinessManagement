using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Aspects.Autofac.Transaction;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete
{
    public class AuthorizationBl : IAuthorizationBl
    {
        private readonly IAccountGroupBl _accountGroupBl;
        private readonly IBranchBl _branchBl;
        private readonly IBusinessBl _businessBl;
        private readonly ICurrencyBl _currencyBl;
        private readonly IFullAddressBl _fullAddressBl;
        private readonly IOperationClaimBl _operationClaimBl;
        private readonly ISystemUserBl _systemUserBl;
        private readonly ISystemUserClaimBl _systemUserClaimBl;

        public AuthorizationBl(
            IAccountGroupBl accountGroupBl,
            IBranchBl branchBl,
            IBusinessBl businessBl,
            ICurrencyBl currencyBl,
            IFullAddressBl fullAddressBl,
            IOperationClaimBl operationClaimBl,
            ISystemUserBl systemUserBl,
            ISystemUserClaimBl systemUserClaimBl
        )
        {
            _accountGroupBl = accountGroupBl;
            _branchBl = branchBl;
            _businessBl = businessBl;
            _currencyBl = currencyBl;
            _fullAddressBl = fullAddressBl;
            _operationClaimBl = operationClaimBl;
            _systemUserBl = systemUserBl;
            _systemUserClaimBl = systemUserClaimBl;
        }

        [TransactionScopeAspect]
        public IResult RegisterSectionManager(ManagerExtDto managerExtDto)
        {
            // Yeni bir sistem kullanıcısı eklenir.
            SystemUserDto systemUserDto = new()
            {
                Phone = managerExtDto.Phone,
            };
            var addSystemUserResult = _systemUserBl.Add(systemUserDto);
            if (!addSystemUserResult.Success) return addSystemUserResult;

            // Yetki adından yetkinin id'si bulunur.
            var getOperationClaimResult = _operationClaimBl.GetByOperationClaimName("Manager");
            if (!getOperationClaimResult.Success) return getOperationClaimResult;

            // Yönetici yetkileri verilir.
            SystemUserClaimDto systemUserClaimDto = new()
            {
                SystemUserId = addSystemUserResult.Data.SystemUserId,
                OperationClaimId = getOperationClaimResult.Data.OperationClaimId,
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = DateTimeOffset.Now,
            };
            var addSystemUserClaimResult = _systemUserClaimBl.Add(systemUserClaimDto);
            if (!addSystemUserClaimResult.Success) return addSystemUserClaimResult;

            // Yeni bir işletme eklenir.
            BusinessDto businessDto = new()
            {
                OwnerSystemUserId = addSystemUserResult.Data.SystemUserId,
                BusinessName = managerExtDto.BusinessName,
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = DateTimeOffset.Now,
            };
            var addBusinessResult = _businessBl.Add(businessDto);
            if (!addBusinessResult.Success) return addBusinessResult;

            // İşletmenin merkez şubesinin adresi eklenir.
            FullAddressDto fullAddressDto = new()
            {
                CityId = managerExtDto.CityId,
                DistrictId = managerExtDto.DistrictId,
                AddressText = managerExtDto.AddressText,
            };
            var addFullAddressResult = _fullAddressBl.Add(fullAddressDto);
            if (!addFullAddressResult.Success) return addFullAddressResult;

            // İşletmenin merkez şubesi eklenir.
            BranchDto branchDto = new()
            {
                BusinessId = addBusinessResult.Data.BusinessId,
                FullAddressId = addFullAddressResult.Data.FullAddressId,
                BranchOrder = 1,
                BranchName = "Merkez",
                BranchCode = "000001",
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = DateTimeOffset.Now,
            };
            var addBranchResult = _branchBl.Add(branchDto);
            if (!addBranchResult.Success) return addBranchResult;

            // Kullanıcı kaydındaki işletme ve şube id'leri güncellenir.
            addSystemUserResult.Data.BusinessId = addBusinessResult.Data.BusinessId;
            addSystemUserResult.Data.BranchId = addBranchResult.Data.BranchId;
            _systemUserBl.Update(addSystemUserResult.Data);

            // Kasanın hesap grubunun id'si getirilir.
            var getAccountGroupResult = _accountGroupBl.GetByAccountGroupCode("100");
            if (!getAccountGroupResult.Success) return getAccountGroupResult;

            // Kasanın doviz cinsi getirilir.
            var getCurrencyResult = _currencyBl.GetByCurrencyName("TL");
            if (!getCurrencyResult.Success) return getCurrencyResult;

            // İşletmenin kasa hesabı oluşturulur.
            Account getAccountResult = _accountDal.GetIfAlreadyExist(addBusinessResult.BusinessId, "10000000100000001");
            if (getAccountResult != null)
            {
                return BadRequest(new ErrorResult(Messages.AccountAlreadyExists));
            }

            Account addAccountResult = new()
            {
                BusinessId = addBusinessResult.BusinessId,
                BranchId = addBranchResult.BranchId,
                AccountGroupId = getAccountGroupResult.AccountGroupId,
                CurrencyId = getCurrencyResult.CurrencyId,
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
            _accountDal.Add(addAccountResult);

            return new SuccessResult(Messages.AuthorizationSectionManagerRegistered);
        }
    }
}

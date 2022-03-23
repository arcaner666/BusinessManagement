using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Aspects.Autofac.Transaction;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete
{
    public class AuthorizationBl : IAuthorizationBl
    {
        private readonly IAccountBl _accountBl;
        private readonly IAccountGroupBl _accountGroupBl;
        private readonly IBranchBl _branchBl;
        private readonly IBusinessBl _businessBl;
        private readonly ICurrencyBl _currencyBl;
        private readonly IFullAddressBl _fullAddressBl;
        private readonly IManagerBl _managerBl;
        private readonly IOperationClaimBl _operationClaimBl;
        private readonly ISectionGroupBl _sectionGroupBl;
        private readonly ISystemUserBl _systemUserBl;
        private readonly ISystemUserClaimBl _systemUserClaimBl;

        public AuthorizationBl(
            IAccountBl accountBl,
            IAccountGroupBl accountGroupBl,
            IBranchBl branchBl,
            IBusinessBl businessBl,
            ICurrencyBl currencyBl,
            IFullAddressBl fullAddressBl,
            IManagerBl managerBl,
            IOperationClaimBl operationClaimBl,
            ISectionGroupBl sectionGroupBl,
            ISystemUserBl systemUserBl,
            ISystemUserClaimBl systemUserClaimBl
        )
        {
            _accountBl = accountBl;
            _accountGroupBl = accountGroupBl;
            _branchBl = branchBl;
            _businessBl = businessBl;
            _currencyBl = currencyBl;
            _fullAddressBl = fullAddressBl;
            _managerBl = managerBl;
            _operationClaimBl = operationClaimBl;
            _sectionGroupBl = sectionGroupBl;
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
                Role = "Manager",
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
            };
            var addSystemUserClaimResult = _systemUserClaimBl.Add(systemUserClaimDto);
            if (!addSystemUserClaimResult.Success) return addSystemUserClaimResult;

            // Yeni bir işletme eklenir.
            BusinessDto businessDto = new()
            {
                OwnerSystemUserId = addSystemUserResult.Data.SystemUserId,
                BusinessName = managerExtDto.BusinessName,
            };
            var addBusinessResult = _businessBl.Add(businessDto);
            if (!addBusinessResult.Success) return addBusinessResult;

            // İşletmenin merkez şubesinin adresi eklenir.
            FullAddressDto fullAddressDto = new()
            {
                CityId = managerExtDto.CityId,
                DistrictId = managerExtDto.DistrictId,
                AddressTitle = "Merkez",
                PostalCode = 0,
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
            AccountDto accountDto = new()
            {
                BusinessId = addBusinessResult.Data.BusinessId,
                BranchId = addBranchResult.Data.BranchId,
                AccountGroupId = getAccountGroupResult.Data.AccountGroupId,
                CurrencyId = getCurrencyResult.Data.CurrencyId,
                AccountOrder = 1,
                AccountName = "TL Kasası",
                AccountCode = "10000000100000001",
                Limit = 0,
                StandartMaturity = 0,
            };
            var addAccountResult = _accountBl.Add(accountDto);
            if (!addAccountResult.Success) return addAccountResult;

            // Yeni bir yönetici eklenir.
            ManagerDto managerDto = new()
            {
                BusinessId = addBusinessResult.Data.BusinessId,
                BranchId = addBranchResult.Data.BranchId,
                NameSurname = managerExtDto.NameSurname,
                Phone = managerExtDto.Phone,
            };
            var addManagerResult = _managerBl.Add(managerDto);
            if (!addManagerResult.Success) return addManagerResult;

            // Yeni site grubu eklenir.
            SectionGroupDto sectionGroupDto = new()
            {
                BusinessId = addBusinessResult.Data.BusinessId,
                BranchId = addBranchResult.Data.BranchId,
                SectionGroupName = "Genel",
            };
            var addSectionGroupResult = _sectionGroupBl.Add(sectionGroupDto);
            if (!addSectionGroupResult.Success) return addSectionGroupResult;

            return new SuccessResult(Messages.AuthorizationSectionManagerRegistered);
        }
    }
}

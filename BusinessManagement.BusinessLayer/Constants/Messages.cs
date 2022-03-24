﻿namespace BusinessManagement.BusinessLayer.Constants
{
    public static class Messages
    {
        #region Account
        public const string AccountAdded = "AccountAdded";
        public const string AccountAlreadyExists = "AccountAlreadyExists";
        public const string AccountCashAdded = "AccountCashAdded";
        public const string AccountCashDeleted = "AccountCashDeleted";
        public const string AccountAccountGroupIsNotDefined = "AccountAccountGroupIsNotDefined";
        public const string AccountEmployeeAdded = "AccountEmployeeAdded";
        public const string AccountEmployeeDeleted = "AccountEmployeeDeleted";
        public const string AccountExtsListedByBusinessId = "AccountExtsListedByBusinessId";
        public const string AccountExtsListedByBusinessIdAndAccountGroupCode = "AccountExtsListedByBusinessIdAndAccountGroupCode";
        public const string AccountExtUpdated = "AccountExtUpdated";
        public const string AccountNotFound = "AccountNotFound";
        public const string AccountOrderAndCodeGenerated = "AccountOrderAndCodeGenerated";
        public const string AccountHouseOwnerAdded = "AccountHouseOwnerAdded";
        public const string AccountHouseOwnerDeleted = "AccountHouseOwnerDeleted";
        public const string AccountsNotFound = "AccountsNotFound";
        public const string AccountSystemUserAdded = "AccountSystemUserAdded";
        public const string AccountSystemUserClaimsAdded = "AccountSystemUserClaimsAdded";
        public const string AccountTaxNumberOrIdentityNumberIsNull = "AccountTaxNumberOrIdentityNumberIsNull";
        public const string AccountTenantAdded = "AccountTenantAdded";
        public const string AccountTenantDeleted = "AccountTenantDeleted";
        #endregion

        #region AccountGroup
        public const string AccountGroupListedByAccountGroupCode = "AccountGroupListedByAccountGroupCode";
        public const string AccountGroupListedById = "AccountGroupListedById";
        public const string AccountGroupNotFound = "AccountGroupNotFound";
        public const string AccountGroupsListed = "AccountGroupsListed";
        public const string AccountGroupsNotFound = "AccountGroupsNotFound";
        #endregion

        #region Apartment
        public const string ApartmentAdded = "ApartmentAdded";
        public const string ApartmentAlreadyExists = "ApartmentAlreadyExists";
        public const string ApartmentExtDeleted = "ApartmentExtDeleted";
        public const string ApartmentExtsListedByBusinessId = "ApartmentExtsListedByBusinessId";
        public const string ApartmentNotFound = "ApartmentNotFound";
        public const string ApartmentsListedBySectionId = "ApartmentsListedBySectionId";
        public const string ApartmentsNotFound = "ApartmentsNotFound";
        public const string ApartmentUpdated = "ApartmentUpdated";
        #endregion

        #region Authorization
        public const string AuthorizationCanNotGetClaimsPrincipal = "AuthorizationCanNotGetClaimsPrincipal";
        public const string AuthorizationDenied = "AuthorizationDenied";
        public const string AuthorizationLoggedIn = "AuthorizationLoggedIn";
        public const string AuthorizationLoggedOut = "AuthorizationLoggedOut";
        public const string AuthorizationSectionManagerRegistered = "AuthorizationSectionManagerRegistered";
        public const string AuthorizationTokenExpired = "AuthorizationTokenExpired";
        public const string AuthorizationTokenInvalid = "AuthorizationTokenInvalid";
        public const string AuthorizationTokensRefreshed = "AuthorizationTokensRefreshed";
        public const string AuthorizationWrongPassword = "AuthorizationWrongPassword";
        #endregion

        #region Bank
        public const string BankAdded = "BankAdded";
        public const string BankAlreadyExists = "BankAlreadyExists";
        public const string BankExtsListedByBusinessId = "BankExtsListedByBusinessId";
        public const string BankNotFound = "BankNotFound";
        public const string BanksNotFound = "BanksNotFound";
        #endregion

        #region Branch
        public const string BranchAdded = "BranchAdded";
        public const string BranchAlreadyExists = "BranchAlreadyExists";
        public const string BranchCanNotDeleteMainBranch = "BranchCanNotDeleteMainBranch";
        public const string BranchDeleted = "BranchDeleted";
        public const string BranchesNotFound = "BranchesNotFound";
        public const string BranchExtAdded = "BranchExtAdded";
        public const string BranchExtDeleted = "BranchExtDeleted";
        public const string BranchExtListedById = "BranchExtListedById";
        public const string BranchExtsListedByBusinessId = "BranchExtsListedByBusinessId";
        public const string BranchExtUpdated = "BranchExtUpdated";
        public const string BranchListedByBranchCode = "BranchListedByBranchCode";
        public const string BranchListedById = "BranchListedById";
        public const string BranchNotFound = "BranchNotFound";
        public const string BranchOrderAndCodeGenerated = "BranchOrderAndCodeGenerated";
        public const string BranchsListedByAccountId = "BranchsListedByAccountId";
        public const string BranchUpdated = "BranchUpdated";
        #endregion

        #region Business
        public const string BusinessAlreadyExists = "BusinessAlreadyExists";
        public const string BusinessAdded = "BusinessAdded";
        #endregion

        #region City
        public const string CitiesListed = "CitiesListed";
        public const string CitiesNotFound = "CitiesNotFound";
        #endregion

        #region Currency
        public const string CurrenciesNotFound = "CurrenciesNotFound";
        public const string CurrenciesListed = "CurrenciesListed";
        public const string CurrencyListedByCurrencyName = "CurrencyListedByCurrencyName";
        public const string CurrencyNotFound = "CurrencyNotFound";
        #endregion

        #region Customer
        public const string CustomerAdded = "CustomerAdded";
        public const string CustomerAlreadyExists = "CustomerAlreadyExists";
        public const string CustomerDeleted = "CustomerDeleted";
        public const string CustomerExtsListedByBusinessId = "CustomerExtsListedByBusinessId";
        public const string CustomerListedByBusinessId = "CustomerListedByBusinessId";
        public const string CustomerListedById = "CustomerListedById";
        public const string CustomerNotFound = "CustomerNotFound";
        public const string CustomersNotFound = "CustomersNotFound";
        public const string CustomerUpdated = "CustomerUpdated";
        #endregion

        #region District
        public const string DistrictsListed = "DistrictsListed";
        public const string DistrictsListedByCityId = "DistrictsListedByCityId";
        public const string DistrictsNotFound = "DistrictsNotFound";
        #endregion

        #region Employee
        public const string EmployeeAlreadyExists = "EmployeeAlreadyExists";
        public const string EmployeeExtsListedByBusinessId = "EmployeeExtsListedByBusinessId";
        public const string EmployeeExtUpdated = "EmployeeExtUpdated";
        public const string EmployeeNotFound = "EmployeeNotFound";
        public const string EmployeesNotFound = "EmployeesNotFound";
        #endregion

        #region EmployeeType
        public const string EmployeeTypesListed = "EmployeeTypesListed";
        public const string EmployeeTypesNotFound = "EmployeeTypesNotFound";
        #endregion

        #region Flat
        public const string FlatAdded = "FlatAdded";
        public const string FlatAlreadyExists = "FlatAlreadyExists";
        public const string FlatDeleted = "FlatDeleted";
        public const string FlatExtAdded = "FlatExtAdded";
        public const string FlatExtsListedByBusinessId = "FlatExtsListedByBusinessId";
        public const string FlatListedByFlatCode = "FlatListedByFlatCode";
        public const string FlatListedById = "FlatListedById";
        public const string FlatNotFound = "FlatNotFound";
        public const string FlatsListedByApartmentId = "FlatsListedByApartmentId";
        public const string FlatsListedBySectionId = "FlatsListedBySectionId";
        public const string FlatsNotFound = "FlatsNotFound";
        public const string FlatUpdated = "FlatUpdated";
        #endregion

        #region FullAddress
        public const string FullAddressAdded = "FullAddressAdded";
        public const string FullAddressAlreadyExists = "FullAddressAlreadyExists";
        public const string FullAddressDeleted = "FullAddressDeleted";
        public const string FullAddressListedById = "FullAddressListedById";
        public const string FullAddressNotFound = "FullAddressNotFound";
        public const string FullAddressUpdated = "FullAddressUpdated";
        #endregion

        #region Manager
        public const string ManagerAdded = "ManagerAdded";
        public const string ManagerAlreadyExists = "ManagerAlreadyExists";
        public const string ManagersListedByBusinessId = "ManagersListedByBusinessId";
        public const string ManagersNotFound = "ManagersNotFound";
        #endregion

        #region OperationClaim
        public const string OperationClaimListedByOperationClaimName = "OperationClaimListedByOperationClaimName";
        public const string OperationClaimNotFound = "OperationClaimNotFound";
        public const string OperationClaimsListed = "OperationClaimsListed";
        public const string OperationClaimsNotFound = "OperationClaimsNotFound";
        #endregion

        #region HouseOwner
        public const string HouseOwnerAlreadyExists = "HouseOwnerAlreadyExists";
        public const string HouseOwnerExtsListedByBusinessId = "HouseOwnerExtsListedByBusinessId";
        public const string HouseOwnerExtUpdated = "HouseOwnerExtUpdated";
        public const string HouseOwnerNotFound = "HouseOwnerNotFound";
        public const string HouseOwnersNotFound = "HouseOwnersNotFound";
        #endregion
        
        #region Section
        public const string SectionAdded = "SectionAdded";
        public const string SectionAlreadyExists = "SectionAlreadyExists";
        public const string SectionDeleted = "SectionDeleted";
        public const string SectionExtAdded = "SectionExtAdded";
        public const string SectionExtDeleted = "SectionExtDeleted";
        public const string SectionExtListedById = "SectionExtListedById";
        public const string SectionExtsListedByBusinessId = "SectionExtsListedByBusinessId";
        public const string SectionExtUpdated = "SectionExtUpdated";
        public const string SectionListedById = "SectionListedById";
        public const string SectionNotFound = "SectionNotFound";
        public const string SectionsListedByBusinessId = "SectionsListedByBusinessId";
        public const string SectionsNotFound = "SectionsNotFound";
        public const string SectionUpdated = "SectionUpdated";
        #endregion

        #region SectionGroup
        public const string SectionGroupAdded = "SectionGroupAdded";
        public const string SectionGroupAlreadyExists = "SectionGroupAlreadyExists";
        public const string SectionGroupDeleted = "SectionGroupDeleted";
        public const string SectionGroupListedById = "SectionGroupListedById";
        public const string SectionGroupNotFound = "SectionGroupNotFound";
        public const string SectionGroupsListedByBusinessId = "SectionGroupsListedByBusinessId";
        public const string SectionGroupsNotFound = "SectionGroupsNotFound";
        public const string SectionGroupUpdated = "SectionGroupUpdated";
        #endregion

        #region SystemUser
        public const string SystemUserAdded = "SystemUserAdded";
        public const string SystemUserAlreadyExists = "SystemUserAlreadyExists";
        public const string SystemUserExtsListedBySystemUserId = "SystemUserExtsListedBySystemUserId";
        public const string SystemUserListedByEmail = "SystemUserListedByEmail";
        public const string SystemUserListedById = "SystemUserListedById";
        public const string SystemUserListedByPhone = "SystemUserListedByPhone";
        public const string SystemUserNotFound = "SystemUserNotFound";
        public const string SystemUserUpdated = "SystemUserUpdated";
        #endregion

        #region SystemUserClaim
        public const string SystemUserClaimAlreadyExists = "SystemUserClaimAlreadyExists";
        public const string SystemUserClaimsNotFound = "SystemUserClaimsNotFound";
        #endregion

        #region Tenant
        public const string TenantAlreadyExists = "TenantAlreadyExists";
        public const string TenantExtsListedByBusinessId = "TenantExtsListedByBusinessId";
        public const string TenantExtUpdated = "TenantExtUpdated";
        public const string TenantNotFound = "TenantNotFound";
        public const string TenantsNotFound = "TenantsNotFound";
        #endregion
    }
}

namespace BusinessManagement.Entities.DTOs
{
    public class BankExtDto
    {
        public long BankId { get; set; }
        public int BusinessId { get; set; }
        public long BranchId { get; set; }
        public long AccountId { get; set; }
        public long FullAddressId { get; set; }
        public string BankName { get; set; }
        public string BankBranchName { get; set; }
        public string BankCode { get; set; }
        public string BankBranchCode { get; set; }
        public string BankAccountCode { get; set; }
        public string Iban { get; set; }
        public string OfficerName { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        // Extended With Branch
        public string BranchName { get; set; }

        // Extended With Account
        public short AccountGroupId { get; set; }
        public byte CurrencyId { get; set; }
        public int AccountOrder { get; set; }
        public string AccountName { get; set; }
        public string AccountCode { get; set; }

        // Extended With Account + AccountGroup
        public string AccountGroupName { get; set; }

        // Extended With Account + Currency
        public string CurrencyName { get; set; }

        // Extended With FullAddress
        public short CityId { get; set; }
        public int DistrictId { get; set; }
        public string AddressText { get; set; }
    }
}

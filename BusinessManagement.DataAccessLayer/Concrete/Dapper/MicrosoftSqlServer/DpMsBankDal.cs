using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsBankDal : IBankDal
{
    private readonly IDbConnection _db;

    public DpMsBankDal(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public Bank Add(Bank bank)
    {
        var sql = "INSERT INTO Bank (BusinessId, BranchId, AccountId, FullAddressId, CurrencyId, BankName, BankBranchName, BankCode, BankBranchCode, BankAccountCode, Iban, OfficerName, StandartMaturity, CreatedAt, UpdatedAt)"
            + " VALUES(@BusinessId, @BranchId, @AccountId, @FullAddressId, @CurrencyId, @BankName, @BankBranchName, @BankCode, @BankBranchCode, @BankAccountCode, @Iban, @OfficerName, @StandartMaturity, @CreatedAt, @UpdatedAt) SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
        var id = _db.Query<long>(sql, bank).Single();
        bank.BankId = id;
        return bank;
    }

    public void Delete(long id)
    {
        var sql = "DELETE FROM Bank"
            + " WHERE BankId = @BankId";
        _db.Execute(sql, new { @BankId = id });
    }

    public Bank GetByAccountId(long accountId)
    {
        var sql = "SELECT * FROM Bank"
            + " WHERE AccountId = @AccountId";
        return _db.Query<Bank>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public List<Bank> GetByBusinessId(int businessId)
    {
        var sql = "SELECT * FROM Bank"
            + " WHERE BusinessId = @BusinessId";
        return _db.Query<Bank>(sql, new { @BusinessId = businessId }).ToList();
    }

    public Bank GetByBusinessIdAndIban(int businessId, string iban)
    {
        var sql = "SELECT * FROM Bank"
            + " WHERE BusinessId = @BusinessId AND Iban = @Iban";
        return _db.Query<Bank>(sql, new
        {
            @BusinessId = businessId,
            @Iban = iban,
        }).SingleOrDefault();
    }

    public Bank GetById(long id)
    {
        var sql = "SELECT * FROM Bank"
            + " WHERE BankId = @BankId";
        return _db.Query<Bank>(sql, new { @BankId = id }).SingleOrDefault();
    }

    public Bank GetExtByAccountId(long accountId)
    {
        var sql = "SELECT * FROM Bank b"
            + " INNER JOIN Branch br ON b.BranchId = br.BranchId"
            + " INNER JOIN FullAddress fa ON b.FullAddressId = fa.FullAddressId"
            + " INNER JOIN City c ON fa.CityId = c.CityId"
            + " INNER JOIN District d ON fa.DistrictId = d.DistrictId"
            + " INNER JOIN Currency cu ON b.CurrencyId = cu.CurrencyId"
            + " WHERE b.AccountId = @AccountId;";
        return _db.Query<Bank, Branch, FullAddress, City, District, Currency, Bank>(sql,
            (bank, branch, fullAddress, city, district, currency) =>
            {
                bank.Branch = branch;
                bank.FullAddress = fullAddress;
                bank.FullAddress.City = city;
                bank.FullAddress.District = district;
                bank.Currency = currency;
                return bank;
            }, new { @AccountId = accountId },
            splitOn: "BranchId,FullAddressId,CityId,DistrictId,CurrencyId").SingleOrDefault();
    }

    public Bank GetExtById(long id)
    {
        var sql = "SELECT * FROM Bank b"
            + " INNER JOIN Branch br ON b.BranchId = br.BranchId"
            + " INNER JOIN FullAddress fa ON b.FullAddressId = fa.FullAddressId"
            + " INNER JOIN City c ON fa.CityId = c.CityId"
            + " INNER JOIN District d ON fa.DistrictId = d.DistrictId"
            + " INNER JOIN Currency cu ON b.CurrencyId = cu.CurrencyId"
            + " WHERE b.BankId = @BankId;";
        return _db.Query<Bank, Branch, FullAddress, City, District, Currency, Bank>(sql,
            (bank, branch, fullAddress, city, district, currency) =>
            {
                bank.Branch = branch;
                bank.FullAddress = fullAddress;
                bank.FullAddress.City = city;
                bank.FullAddress.District = district;
                bank.Currency = currency;
                return bank;
            }, new { @BankId = id },
            splitOn: "BranchId,FullAddressId,CityId,DistrictId,CurrencyId").SingleOrDefault();
    }

    public List<Bank> GetExtsByBusinessId(int businessId)
    {
        var sql = "SELECT * FROM Bank b"
            + " INNER JOIN Branch br ON b.BranchId = br.BranchId"
            + " INNER JOIN FullAddress fa ON b.FullAddressId = fa.FullAddressId"
            + " INNER JOIN City c ON fa.CityId = c.CityId"
            + " INNER JOIN District d ON fa.DistrictId = d.DistrictId"
            + " INNER JOIN Currency cu ON b.CurrencyId = cu.CurrencyId"
            + " WHERE b.BusinessId = @BusinessId;";
        return _db.Query<Bank, Branch, FullAddress, City, District, Currency, Bank>(sql,
            (bank, branch, fullAddress, city, district, currency) =>
            {
                bank.Branch = branch;
                bank.FullAddress = fullAddress;
                bank.FullAddress.City = city;
                bank.FullAddress.District = district;
                bank.Currency = currency;
                return bank;
            }, new { @BusinessId = businessId },
            splitOn: "BranchId,FullAddressId,CityId,DistrictId,CurrencyId").ToList();
    }

    public void Update(Bank bank)
    {
        var sql = "UPDATE Bank SET BusinessId = @BusinessId, BranchId = @BranchId, AccountId = @AccountId, FullAddressId = @FullAddressId, CurrencyId = @CurrencyId, BankName = @BankName, BankBranchName = @BankBranchName, BankCode = @BankCode, BankBranchCode = @BankBranchCode, BankAccountCode = @BankAccountCode, Iban = @Iban, OfficerName = @OfficerName, StandartMaturity = @StandartMaturity, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt"
            + " WHERE BankId = @BankId";
        _db.Execute(sql, bank);
    }
}

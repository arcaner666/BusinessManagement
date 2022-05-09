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

    public Bank GetById(long id)
    {
        var sql = "SELECT * FROM Bank"
            + " WHERE BankId = @BankId";
        return _db.Query<Bank>(sql, new { @BankId = id }).SingleOrDefault();
    }

    public List<Bank> GetExtsByBusinessId(int businessId)
    {
        var sql = "SELECT * FROM Bank b"
            + " INNER JOIN Branch br ON b.BranchId = br.BranchId"
            + " INNER JOIN FullAddress fa ON b.FullAddressId = fa.FullAddressId"
            + " INNER JOIN City c ON fa.CityId = c.CityId"
            + " INNER JOIN District d ON fa.DistrictId = d.DistrictId"
            + " WHERE b.BusinessId = @BusinessId;";
        return _db.Query<Bank, Branch, FullAddress, City, District, Bank>(sql,
            (bank, branch, fullAddress, city, district) =>
            {
                bank.Branch = branch;
                bank.FullAddress = fullAddress;
                bank.FullAddress.City = city;
                bank.FullAddress.District = district;
                return bank;
            }, new { @BusinessId = businessId },
            splitOn: "BranchId,FullAddressId,CityId,DistrictId").ToList();
    }

    public Bank GetIfAlreadyExist(int businessId, string iban)
    {
        var sql = "SELECT * FROM Bank"
            + " WHERE BusinessId = @BusinessId AND Iban = @Iban";
        return _db.Query<Bank>(sql, new
        {
            @BusinessId = businessId,
            @Iban = iban,
        }).SingleOrDefault();
    }

    public void Update(Bank bank)
    {
        var sql = "UPDATE Bank SET BusinessId = @BusinessId, BranchId = @BranchId, AccountId = @AccountId, FullAddressId = @FullAddressId, CurrencyId = @CurrencyId, BankName = @BankName, BankBranchName = @BankBranchName, BankCode = @BankCode, BankBranchCode = @BankBranchCode, BankAccountCode = @BankAccountCode, Iban = @Iban, OfficerName = @OfficerName, StandartMaturity = @StandartMaturity, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt"
            + " WHERE BankId = @BankId";
        _db.Execute(sql, bank);
    }
}

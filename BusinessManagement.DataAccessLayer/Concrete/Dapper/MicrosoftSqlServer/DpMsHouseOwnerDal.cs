using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsHouseOwnerDal : IHouseOwnerDal
{
    private readonly IDbConnection _db;

    public DpMsHouseOwnerDal(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public HouseOwner Add(HouseOwner houseOwner)
    {
        var sql = "INSERT INTO HouseOwner (BusinessId, BranchId, AccountId, NameSurname, Email, Phone, DateOfBirth, Gender, Notes, AvatarUrl, TaxOffice, TaxNumber, IdentityNumber, StandartMaturity, CreatedAt, UpdatedAt)"
            + " VALUES(@BusinessId, @BranchId, @AccountId, @NameSurname, @Email, @Phone, @DateOfBirth, @Gender, @Notes, @AvatarUrl, @TaxOffice, @TaxNumber, @IdentityNumber, @StandartMaturity, @CreatedAt, @UpdatedAt) SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
        var id = _db.Query<long>(sql, houseOwner).Single();
        houseOwner.HouseOwnerId = id;
        return houseOwner;
    }

    public void Delete(long id)
    {
        var sql = "DELETE FROM HouseOwner"
            + " WHERE HouseOwnerId = @HouseOwnerId";
        _db.Execute(sql, new { @HouseOwnerId = id });
    }

    public List<HouseOwner> GetByBusinessId(int businessId)
    {
        var sql = "SELECT * FROM HouseOwner"
            + " WHERE BusinessId = @BusinessId";
        return _db.Query<HouseOwner>(sql, new { @BusinessId = businessId }).ToList();
    }

    public HouseOwner GetByBusinessIdAndAccountId(int businessId, long accountId)
    {
        var sql = "SELECT * FROM HouseOwner"
            + " WHERE BusinessId = @BusinessId AND AccountId = @AccountId";
        return _db.Query<HouseOwner>(sql, new
        {
            @BusinessId = businessId,
            @AccountId = accountId,
        }).SingleOrDefault();
    }

    public HouseOwner GetById(long id)
    {
        var sql = "SELECT * FROM HouseOwner"
            + " WHERE HouseOwnerId = @HouseOwnerId";
        return _db.Query<HouseOwner>(sql, new { @HouseOwnerId = id }).SingleOrDefault();
    }

    public HouseOwner GetExtByAccountId(long accountId)
    {
        var sql = "SELECT * FROM HouseOwner ho"
            + " INNER JOIN Account a ON ho.AccountId = a.AccountId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " WHERE ho.AccountId = @AccountId";
        return _db.Query<HouseOwner, Account, AccountGroup, HouseOwner>(sql,
            (houseOwner, account, accountGroup) =>
            {
                houseOwner.Account = account;
                houseOwner.Account.AccountGroup = accountGroup;
                return houseOwner;
            }, new { @AccountId = accountId },
            splitOn: "AccountId,AccountGroupId").SingleOrDefault();
    }

    public HouseOwner GetExtById(long id)
    {
        var sql = "SELECT * FROM HouseOwner ho"
            + " INNER JOIN Account a ON ho.AccountId = a.AccountId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " WHERE ho.HouseOwnerId = @HouseOwnerId";
        return _db.Query<HouseOwner, Account, AccountGroup, HouseOwner>(sql,
            (houseOwner, account, accountGroup) =>
            {
                houseOwner.Account = account;
                houseOwner.Account.AccountGroup = accountGroup;
                return houseOwner;
            }, new { @HouseOwnerId = id },
            splitOn: "AccountId,AccountGroupId").SingleOrDefault();
    }

    public List<HouseOwner> GetExtsByBusinessId(int businessId)
    {
        var sql = "SELECT * FROM HouseOwner ho"
            + " INNER JOIN Account a ON ho.AccountId = a.AccountId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " WHERE ho.BusinessId = @BusinessId";
        return _db.Query<HouseOwner, Account, AccountGroup, HouseOwner>(sql,
            (houseOwner, account, accountGroup) =>
            {
                houseOwner.Account = account;
                houseOwner.Account.AccountGroup = accountGroup;
                return houseOwner;
            }, new { @BusinessId = businessId },
            splitOn: "AccountId,AccountGroupId").ToList();
    }

    public void Update(HouseOwner houseOwner)
    {
        var sql = "UPDATE HouseOwner SET BusinessId = @BusinessId, BranchId = @BranchId, AccountId = @AccountId, NameSurname = @NameSurname, Email = @Email, Phone = @Phone, DateOfBirth = @DateOfBirth, Gender = @Gender, Notes = @Notes, AvatarUrl = @AvatarUrl, TaxOffice = @TaxOffice, TaxNumber = @TaxNumber, IdentityNumber = @IdentityNumber, StandartMaturity = @StandartMaturity, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt"
            + " WHERE HouseOwnerId = @HouseOwnerId";
        _db.Execute(sql, houseOwner);
    }
}

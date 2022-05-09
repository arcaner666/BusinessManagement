using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsTenantDal : ITenantDal
{
    private readonly IDbConnection _db;

    public DpMsTenantDal(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public Tenant Add(Tenant tenant)
    {
        var sql = "INSERT INTO Tenant (BusinessId, BranchId, AccountId, NameSurname, Email, Phone, DateOfBirth, Gender, Notes, AvatarUrl, TaxOffice, TaxNumber, IdentityNumber, StandartMaturity, CreatedAt, UpdatedAt)"
            + " VALUES(@BusinessId, @BranchId, @AccountId, @NameSurname, @Email, @Phone, @DateOfBirth, @Gender, @Notes, @AvatarUrl, @TaxOffice, @TaxNumber, @IdentityNumber, @StandartMaturity, @CreatedAt, @UpdatedAt) SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
        var id = _db.Query<long>(sql, tenant).Single();
        tenant.TenantId = id;
        return tenant;
    }

    public void Delete(long id)
    {
        var sql = "DELETE FROM Tenant"
            + " WHERE TenantId = @TenantId";
        _db.Execute(sql, new { @TenantId = id });
    }

    public List<Tenant> GetByBusinessId(int businessId)
    {
        var sql = "SELECT * FROM Tenant"
            + " WHERE BusinessId = @BusinessId";
        return _db.Query<Tenant>(sql, new { @BusinessId = businessId }).ToList();
    }

    public Tenant GetByBusinessIdAndAccountId(int businessId, long accountId)
    {
        var sql = "SELECT * FROM Tenant"
            + " WHERE BusinessId = @BusinessId AND AccountId = @AccountId";
        return _db.Query<Tenant>(sql, new
        {
            @BusinessId = businessId,
            @AccountId = accountId,
        }).SingleOrDefault();
    }

    public Tenant GetById(long id)
    {
        var sql = "SELECT * FROM Tenant"
            + " WHERE TenantId = @TenantId";
        return _db.Query<Tenant>(sql, new { @TenantId = id }).SingleOrDefault();
    }

    public Tenant GetExtById(long id)
    {
        var sql = "SELECT * FROM Tenant t"
            + " INNER JOIN Account a ON t.AccountId = a.AccountId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " WHERE t.TenantId = @TenantId";
        return _db.Query<Tenant, Account, AccountGroup, Tenant>(sql,
            (tenant, account, accountGroup) =>
            {
                tenant.Account = account;
                tenant.Account.AccountGroup = accountGroup;
                return tenant;
            }, new { @TenantId = id },
            splitOn: "AccountId,AccountGroupId").SingleOrDefault();
    }

    public List<Tenant> GetExtsByBusinessId(int businessId)
    {
        var sql = "SELECT * FROM Tenant t"
            + " INNER JOIN Account a ON t.AccountId = a.AccountId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " WHERE t.BusinessId = @BusinessId";
        return _db.Query<Tenant, Account, AccountGroup, Tenant>(sql,
            (tenant, account, accountGroup) =>
            {
                tenant.Account = account;
                tenant.Account.AccountGroup = accountGroup;
                return tenant;
            }, new { @BusinessId = businessId },
            splitOn: "AccountId,AccountGroupId").ToList();
    }

    public void Update(Tenant tenant)
    {
        var sql = "UPDATE Tenant SET BusinessId = @BusinessId, BranchId = @BranchId, AccountId = @AccountId, NameSurname = @NameSurname, Email = @Email, Phone = @Phone, DateOfBirth = @DateOfBirth, Gender = @Gender, Notes = @Notes, AvatarUrl = @AvatarUrl, TaxOffice = @TaxOffice, TaxNumber = @TaxNumber, IdentityNumber = @IdentityNumber, StandartMaturity = @StandartMaturity, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt"
            + " WHERE TenantId = @TenantId";
        _db.Execute(sql, tenant);
    }
}

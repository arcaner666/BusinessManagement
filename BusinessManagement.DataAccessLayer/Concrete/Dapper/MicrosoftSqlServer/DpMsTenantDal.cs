using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.ExtendedDatabaseModels;

using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsTenantDal : ITenantDal
{
    private readonly DapperContext _context;

    public DpMsTenantDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(Tenant tenant)
    {
        using var connection = _context.CreateConnection();
        var sql = "INSERT INTO Tenant ("
            + " BusinessId,"
            + " BranchId,"
            + " AccountId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " DateOfBirth,"
            + " Gender,"
            + " Notes,"
            + " AvatarUrl,"
            + " TaxOffice,"
            + " TaxNumber,"
            + " IdentityNumber,"
            + " StandartMaturity,"
            + " CreatedAt,"
            + " UpdatedAt)"
            + " VALUES("
            + " @BusinessId,"
            + " @BranchId,"
            + " @AccountId,"
            + " @NameSurname,"
            + " @Email,"
            + " @Phone,"
            + " @DateOfBirth,"
            + " @Gender,"
            + " @Notes,"
            + " @AvatarUrl,"
            + " @TaxOffice,"
            + " @TaxNumber,"
            + " @IdentityNumber,"
            + " @StandartMaturity,"
            + " @CreatedAt,"
            + " @UpdatedAt)"
            + " SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
        return connection.Query<long>(sql, tenant).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM Tenant"
            + " WHERE TenantId = @TenantId";
        connection.Execute(sql, new { @TenantId = id });
    }

    public Tenant GetByAccountId(long accountId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " TenantId,"
            + " BusinessId,"
            + " BranchId,"
            + " AccountId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " DateOfBirth,"
            + " Gender,"
            + " Notes,"
            + " AvatarUrl,"
            + " TaxOffice,"
            + " TaxNumber,"
            + " IdentityNumber,"
            + " StandartMaturity,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Tenant"
            + " WHERE AccountId = @AccountId";
        return connection.Query<Tenant>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public List<Tenant> GetByBusinessId(int businessId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " TenantId,"
            + " BusinessId,"
            + " BranchId,"
            + " AccountId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " DateOfBirth,"
            + " Gender,"
            + " Notes,"
            + " AvatarUrl,"
            + " TaxOffice,"
            + " TaxNumber,"
            + " IdentityNumber,"
            + " StandartMaturity,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Tenant"
            + " WHERE BusinessId = @BusinessId";
        return connection.Query<Tenant>(sql, new { @BusinessId = businessId }).ToList();
    }

    public Tenant GetByBusinessIdAndAccountId(int businessId, long accountId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " TenantId,"
            + " BusinessId,"
            + " BranchId,"
            + " AccountId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " DateOfBirth,"
            + " Gender,"
            + " Notes,"
            + " AvatarUrl,"
            + " TaxOffice,"
            + " TaxNumber,"
            + " IdentityNumber,"
            + " StandartMaturity,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Tenant"
            + " WHERE BusinessId = @BusinessId AND AccountId = @AccountId";
        return connection.Query<Tenant>(sql, new
        {
            @BusinessId = businessId,
            @AccountId = accountId,
        }).SingleOrDefault();
    }

    public Tenant GetById(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " TenantId,"
            + " BusinessId,"
            + " BranchId,"
            + " AccountId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " DateOfBirth,"
            + " Gender,"
            + " Notes,"
            + " AvatarUrl,"
            + " TaxOffice,"
            + " TaxNumber,"
            + " IdentityNumber,"
            + " StandartMaturity,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Tenant"
            + " WHERE TenantId = @TenantId";
        return connection.Query<Tenant>(sql, new { @TenantId = id }).SingleOrDefault();
    }

    public TenantExt GetExtByAccountId(long accountId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " t.TenantId,"
            + " t.BusinessId,"
            + " t.BranchId,"
            + " t.AccountId,"
            + " t.NameSurname,"
            + " t.Email,"
            + " t.Phone,"
            + " t.DateOfBirth,"
            + " t.Gender,"
            + " t.Notes,"
            + " t.AvatarUrl,"
            + " t.TaxOffice,"
            + " t.TaxNumber,"
            + " t.IdentityNumber,"
            + " t.StandartMaturity,"
            + " t.CreatedAt,"
            + " t.UpdatedAt,"
            + " a.AccountGroupId,"
            + " a.AccountOrder,"
            + " a.AccountName,"
            + " a.AccountCode,"
            + " a.Limit,"
            + " ag.AccountGroupName"
            + " FROM Tenant t"
            + " INNER JOIN Account a ON t.AccountId = a.AccountId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " WHERE t.AccountId = @AccountId";
        return connection.Query<TenantExt>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public TenantExt GetExtById(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " t.TenantId,"
            + " t.BusinessId,"
            + " t.BranchId,"
            + " t.AccountId,"
            + " t.NameSurname,"
            + " t.Email,"
            + " t.Phone,"
            + " t.DateOfBirth,"
            + " t.Gender,"
            + " t.Notes,"
            + " t.AvatarUrl,"
            + " t.TaxOffice,"
            + " t.TaxNumber,"
            + " t.IdentityNumber,"
            + " t.StandartMaturity,"
            + " t.CreatedAt,"
            + " t.UpdatedAt,"
            + " a.AccountGroupId,"
            + " a.AccountOrder,"
            + " a.AccountName,"
            + " a.AccountCode,"
            + " a.Limit,"
            + " ag.AccountGroupName"
            + " FROM Tenant t"
            + " INNER JOIN Account a ON t.AccountId = a.AccountId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " WHERE t.TenantId = @TenantId";
        return connection.Query<TenantExt>(sql, new { @TenantId = id }).SingleOrDefault();
    }

    public List<TenantExt> GetExtsByBusinessId(int businessId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " t.TenantId,"
            + " t.BusinessId,"
            + " t.BranchId,"
            + " t.AccountId,"
            + " t.NameSurname,"
            + " t.Email,"
            + " t.Phone,"
            + " t.DateOfBirth,"
            + " t.Gender,"
            + " t.Notes,"
            + " t.AvatarUrl,"
            + " t.TaxOffice,"
            + " t.TaxNumber,"
            + " t.IdentityNumber,"
            + " t.StandartMaturity,"
            + " t.CreatedAt,"
            + " t.UpdatedAt,"
            + " a.AccountGroupId,"
            + " a.AccountOrder,"
            + " a.AccountName,"
            + " a.AccountCode,"
            + " a.Limit,"
            + " ag.AccountGroupName"
            + " FROM Tenant t"
            + " INNER JOIN Account a ON t.AccountId = a.AccountId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " WHERE t.BusinessId = @BusinessId";
        return connection.Query<TenantExt>(sql, new { @BusinessId = businessId }).ToList();
    }

    public void Update(Tenant tenant)
    {
        using var connection = _context.CreateConnection();
        var sql = "UPDATE Tenant SET"
            + " BusinessId = @BusinessId,"
            + " BranchId = @BranchId,"
            + " AccountId = @AccountId,"
            + " NameSurname = @NameSurname,"
            + " Email = @Email,"
            + " Phone = @Phone,"
            + " DateOfBirth = @DateOfBirth,"
            + " Gender = @Gender,"
            + " Notes = @Notes,"
            + " AvatarUrl = @AvatarUrl,"
            + " TaxOffice = @TaxOffice,"
            + " TaxNumber = @TaxNumber,"
            + " IdentityNumber = @IdentityNumber,"
            + " StandartMaturity = @StandartMaturity,"
            + " CreatedAt = @CreatedAt,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE TenantId = @TenantId";
        connection.Execute(sql, tenant);
    }
}

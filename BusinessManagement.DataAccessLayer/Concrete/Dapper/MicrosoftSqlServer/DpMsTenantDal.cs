using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsTenantDal : ITenantDal
{
    private readonly DapperContext _context;

    public DpMsTenantDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(TenantDto tenantDto)
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
        return connection.Query<long>(sql, tenantDto).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM Tenant"
            + " WHERE TenantId = @TenantId";
        connection.Execute(sql, new { @TenantId = id });
    }

    public TenantDto GetByAccountId(long accountId)
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
        return connection.Query<TenantDto>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public IEnumerable<TenantDto> GetByBusinessId(int businessId)
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
        return connection.Query<TenantDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public TenantDto GetByBusinessIdAndAccountId(int businessId, long accountId)
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
        return connection.Query<TenantDto>(sql, new
        {
            @BusinessId = businessId,
            @AccountId = accountId,
        }).SingleOrDefault();
    }

    public TenantDto GetById(long id)
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
        return connection.Query<TenantDto>(sql, new { @TenantId = id }).SingleOrDefault();
    }

    public TenantExtDto GetExtByAccountId(long accountId)
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
        return connection.Query<TenantExtDto>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public TenantExtDto GetExtById(long id)
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
        return connection.Query<TenantExtDto>(sql, new { @TenantId = id }).SingleOrDefault();
    }

    public IEnumerable<TenantExtDto> GetExtsByBusinessId(int businessId)
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
        return connection.Query<TenantExtDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public void Update(TenantDto tenantDto)
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
        connection.Execute(sql, tenantDto);
    }
}

using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
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

    public long Add(TenantDto tenantDto)
    {
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
        return _db.Query<long>(sql, tenantDto).Single();
    }

    public void Delete(long id)
    {
        var sql = "DELETE FROM Tenant"
            + " WHERE TenantId = @TenantId";
        _db.Execute(sql, new { @TenantId = id });
    }

    public TenantDto GetByAccountId(long accountId)
    {
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
        return _db.Query<TenantDto>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public List<TenantDto> GetByBusinessId(int businessId)
    {
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
        return _db.Query<TenantDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public TenantDto GetByBusinessIdAndAccountId(int businessId, long accountId)
    {
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
        return _db.Query<TenantDto>(sql, new
        {
            @BusinessId = businessId,
            @AccountId = accountId,
        }).SingleOrDefault();
    }

    public TenantDto GetById(long id)
    {
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
        return _db.Query<TenantDto>(sql, new { @TenantId = id }).SingleOrDefault();
    }

    public TenantExtDto GetExtByAccountId(long accountId)
    {
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
        return _db.Query<TenantExtDto>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public TenantExtDto GetExtById(long id)
    {
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
        return _db.Query<TenantExtDto>(sql, new { @TenantId = id }).SingleOrDefault();
    }

    public List<TenantExtDto> GetExtsByBusinessId(int businessId)
    {
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
        return _db.Query<TenantExtDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public void Update(TenantDto tenantDto)
    {
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
        _db.Execute(sql, tenantDto);
    }
}

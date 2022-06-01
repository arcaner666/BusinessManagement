using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsCashDal : ICashDal
{
    private readonly IDbConnection _db;

    public DpMsCashDal(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public long Add(CashDto cashDto)
    {
        var sql = "INSERT INTO Cash ("
            + " BusinessId,"
            + " BranchId,"
            + " AccountId,"
            + " CurrencyId,"
            + " CreatedAt,"
            + " UpdatedAt)"
            + " VALUES("
            + " @BusinessId,"
            + " @BranchId,"
            + " @AccountId,"
            + " @CurrencyId,"
            + " @CreatedAt,"
            + " @UpdatedAt)"
            + " SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
        return _db.Query<long>(sql, cashDto).Single();
    }

    public void Delete(long id)
    {
        var sql = "DELETE FROM Cash"
            + " WHERE CashId = @CashId";
        _db.Execute(sql, new { @CashId = id });
    }

    public CashDto GetByAccountId(long accountId)
    {
        var sql = "SELECT"
            + " CashId,"
            + " BusinessId,"
            + " BranchId,"
            + " AccountId,"
            + " CurrencyId,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Cash"
            + " WHERE AccountId = @AccountId";
        return _db.Query<CashDto>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public List<CashDto> GetByBusinessId(int businessId)
    {
        var sql = "SELECT"
            + " CashId,"
            + " BusinessId,"
            + " BranchId,"
            + " AccountId,"
            + " CurrencyId,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Cash"
            + " WHERE BusinessId = @BusinessId";
        return _db.Query<CashDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public CashDto GetByBusinessIdAndAccountId(int businessId, long accountId)
    {
        var sql = "SELECT"
            + " CashId,"
            + " BusinessId,"
            + " BranchId,"
            + " AccountId,"
            + " CurrencyId,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Cash"
            + " WHERE BusinessId = @BusinessId AND AccountId = @AccountId";
        return _db.Query<CashDto>(sql, new
        {
            @BusinessId = businessId,
            @AccountId = accountId,
        }).SingleOrDefault();
    }

    public CashDto GetById(long id)
    {
        var sql = "SELECT"
            + " CashId,"
            + " BusinessId,"
            + " BranchId,"
            + " AccountId,"
            + " CurrencyId,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Cash"
            + " WHERE CashId = @CashId";
        return _db.Query<CashDto>(sql, new { @CashId = id }).SingleOrDefault();
    }

    public CashExtDto GetExtByAccountId(long accountId)
    {
        var sql = "SELECT"
            + " c.CashId,"
            + " c.BusinessId,"
            + " c.BranchId,"
            + " c.AccountId,"
            + " c.CurrencyId,"
            + " c.CreatedAt,"
            + " c.UpdatedAt,"
            + " b.BranchName,"
            + " a.AccountGroupId,"
            + " a.AccountOrder,"
            + " a.AccountName,"
            + " a.AccountCode,"
            + " a.Limit,"
            + " ag.AccountGroupName,"
            + " cu.CurrencyName"
            + " FROM Cash c"
            + " INNER JOIN Branch b ON c.BranchId = b.BranchId"
            + " INNER JOIN Account a ON c.AccountId = a.AccountId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " INNER JOIN Currency cu ON c.CurrencyId = cu.CurrencyId"
            + " WHERE c.AccountId = @AccountId";
        return _db.Query<CashExtDto>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public CashExtDto GetExtById(long id)
    {
        var sql = "SELECT"
            + " c.CashId,"
            + " c.BusinessId,"
            + " c.BranchId,"
            + " c.AccountId,"
            + " c.CurrencyId,"
            + " c.CreatedAt,"
            + " c.UpdatedAt,"
            + " b.BranchName,"
            + " a.AccountGroupId,"
            + " a.AccountOrder,"
            + " a.AccountName,"
            + " a.AccountCode,"
            + " a.Limit,"
            + " ag.AccountGroupName,"
            + " cu.CurrencyName"
            + " FROM Cash c"
            + " INNER JOIN Branch b ON c.BranchId = b.BranchId"
            + " INNER JOIN Account a ON c.AccountId = a.AccountId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " INNER JOIN Currency cu ON c.CurrencyId = cu.CurrencyId"
            + " WHERE c.CashId = @CashId";
        return _db.Query<CashExtDto>(sql, new { @CashId = id }).SingleOrDefault();
    }

    public List<CashExtDto> GetExtsByBusinessId(int businessId)
    {
        var sql = "SELECT"
            + " c.CashId,"
            + " c.BusinessId,"
            + " c.BranchId,"
            + " c.AccountId,"
            + " c.CurrencyId,"
            + " c.CreatedAt,"
            + " c.UpdatedAt,"
            + " b.BranchName,"
            + " a.AccountGroupId,"
            + " a.AccountOrder,"
            + " a.AccountName,"
            + " a.AccountCode,"
            + " a.Limit,"
            + " ag.AccountGroupName,"
            + " cu.CurrencyName"
            + " FROM Cash c"
            + " INNER JOIN Branch b ON c.BranchId = b.BranchId"
            + " INNER JOIN Account a ON c.AccountId = a.AccountId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " INNER JOIN Currency cu ON c.CurrencyId = cu.CurrencyId"
            + " WHERE c.BusinessId = @BusinessId";
        return _db.Query<CashExtDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public void Update(CashDto cashDto)
    {
        var sql = "UPDATE Cash SET"
            + " BusinessId = @BusinessId,"
            + " BranchId = @BranchId,"
            + " AccountId = @AccountId,"
            + " CurrencyId = @CurrencyId,"
            + " CreatedAt = @CreatedAt,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE CashId = @CashId";
        _db.Execute(sql, cashDto);
    }
}

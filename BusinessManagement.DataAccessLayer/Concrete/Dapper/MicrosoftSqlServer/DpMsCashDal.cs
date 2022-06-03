using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsCashDal : ICashDal
{
    private readonly DapperContext _context;

    public DpMsCashDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(CashDto cashDto)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<long>(sql, cashDto).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM Cash"
            + " WHERE CashId = @CashId";
        connection.Execute(sql, new { @CashId = id });
    }

    public CashDto GetByAccountId(long accountId)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<CashDto>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public IEnumerable<CashDto> GetByBusinessId(int businessId)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<CashDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public CashDto GetByBusinessIdAndAccountId(int businessId, long accountId)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<CashDto>(sql, new
        {
            @BusinessId = businessId,
            @AccountId = accountId,
        }).SingleOrDefault();
    }

    public CashDto GetById(long id)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<CashDto>(sql, new { @CashId = id }).SingleOrDefault();
    }

    public CashExtDto GetExtByAccountId(long accountId)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<CashExtDto>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public CashExtDto GetExtById(long id)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<CashExtDto>(sql, new { @CashId = id }).SingleOrDefault();
    }

    public IEnumerable<CashExtDto> GetExtsByBusinessId(int businessId)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<CashExtDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public void Update(CashDto cashDto)
    {
        using var connection = _context.CreateConnection();
        var sql = "UPDATE Cash SET"
            + " BusinessId = @BusinessId,"
            + " BranchId = @BranchId,"
            + " AccountId = @AccountId,"
            + " CurrencyId = @CurrencyId,"
            + " CreatedAt = @CreatedAt,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE CashId = @CashId";
        connection.Execute(sql, cashDto);
    }
}

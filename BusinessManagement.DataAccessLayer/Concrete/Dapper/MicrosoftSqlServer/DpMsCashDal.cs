using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
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

    public Cash Add(Cash cash)
    {
        var sql = "INSERT INTO Cash (BusinessId, BranchId, AccountId, CurrencyId, CreatedAt, UpdatedAt)"
            + " VALUES(@BusinessId, @BranchId, @AccountId, @CurrencyId, @CreatedAt, @UpdatedAt) SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
        var id = _db.Query<long>(sql, cash).Single();
        cash.CashId = id;
        return cash;
    }

    public void Delete(long id)
    {
        var sql = "DELETE FROM Cash"
            + " WHERE CashId = @CashId";
        _db.Execute(sql, new { @CashId = id });
    }

    public List<Cash> GetByBusinessId(int businessId)
    {
        var sql = "SELECT * FROM Cash"
            + " WHERE BusinessId = @BusinessId";
        return _db.Query<Cash>(sql, new { @BusinessId = businessId }).ToList();
    }

    public Cash GetByBusinessIdAndAccountId(int businessId, long accountId)
    {
        var sql = "SELECT * FROM Cash"
            + " WHERE BusinessId = @BusinessId AND AccountId = @AccountId";
        return _db.Query<Cash>(sql, new
        {
            @BusinessId = businessId,
            @AccountId = accountId,
        }).SingleOrDefault();
    }

    public Cash GetById(long id)
    {
        var sql = "SELECT * FROM Cash"
            + " WHERE CashId = @CashId";
        return _db.Query<Cash>(sql, new { @CashId = id }).SingleOrDefault();
    }

    public Cash GetExtByAccountId(long accountId)
    {
        var sql = "SELECT * FROM Cash c"
            + " INNER JOIN Account a ON c.AccountId = a.AccountId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " INNER JOIN Currency cu ON c.CurrencyId = cu.CurrencyId"
            + " WHERE c.AccountId = @AccountId";
        return _db.Query<Cash, Account, AccountGroup, Currency, Cash>(sql,
            (cash, account, accountGroup, currency) =>
            {
                cash.Account = account;
                cash.Account.AccountGroup = accountGroup;
                cash.Currency = currency;
                return cash;
            }, new { @AccountId = accountId },
            splitOn: "AccountId,AccountGroupId,CurrencyId").SingleOrDefault();
    }

    public Cash GetExtById(long id)
    {
        var sql = "SELECT * FROM Cash c"
            + " INNER JOIN Account a ON c.AccountId = a.AccountId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " INNER JOIN Currency cu ON c.CurrencyId = cu.CurrencyId"
            + " WHERE c.CashId = @CashId";
        return _db.Query<Cash, Account, AccountGroup, Currency, Cash>(sql,
            (cash, account, accountGroup, currency) =>
            {
                cash.Account = account;
                cash.Account.AccountGroup = accountGroup;
                cash.Currency = currency;
                return cash;
            }, new { @CashId = id },
            splitOn: "AccountId,AccountGroupId,CurrencyId").SingleOrDefault();
    }

    public List<Cash> GetExtsByBusinessId(int businessId)
    {
        var sql = "SELECT * FROM Cash c"
            + " INNER JOIN Account a ON c.AccountId = a.AccountId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " INNER JOIN Currency cu ON c.CurrencyId = cu.CurrencyId"
            + " WHERE c.BusinessId = @BusinessId";
        return _db.Query<Cash, Account, AccountGroup, Currency, Cash>(sql,
            (cash, account, accountGroup, currency) =>
            {
                cash.Account = account;
                cash.Account.AccountGroup = accountGroup;
                cash.Currency = currency;
                return cash;
            }, new { @BusinessId = businessId },
            splitOn: "AccountId,AccountGroupId,CurrencyId").ToList();
    }

    public void Update(Cash cash)
    {
        var sql = "UPDATE Cash SET BusinessId = @BusinessId, BranchId = @BranchId, AccountId = @AccountId, CurrencyId = @CurrencyId, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt"
            + " WHERE CashId = @CashId";
        _db.Execute(sql, cash);
    }
}

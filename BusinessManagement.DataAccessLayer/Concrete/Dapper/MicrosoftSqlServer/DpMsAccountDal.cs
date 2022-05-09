using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsAccountDal : IAccountDal
{
    private readonly IDbConnection _db;

    public DpMsAccountDal(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public Account Add(Account account)
    {
        var sql = "INSERT INTO Account (BusinessId, BranchId, AccountGroupId, AccountTypeId, AccountOrder, AccountName, AccountCode, DebitBalance, CreditBalance, Balance, Limit, CreatedAt, UpdatedAt)"
            + " VALUES(@BusinessId, @BranchId, @AccountGroupId, @AccountTypeId, @AccountOrder, @AccountName, @AccountCode, @DebitBalance, @CreditBalance, @Balance, @Limit, @CreatedAt, @UpdatedAt) SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
        var id = _db.Query<long>(sql, account).Single();
        account.AccountId = id;
        return account;
    }

    public void Delete(long id)
    {
        var sql = "DELETE FROM Account"
            + " WHERE AccountId = @AccountId";
        _db.Execute(sql, new { @AccountId = id });
    }

    public List<Account> GetByAccountGroupId(short accountGroupId)
    {
        var sql = "SELECT * FROM Account"
            + " WHERE AccountGroupId = @AccountGroupId";
        return _db.Query<Account>(sql, new { @AccountGroupId = accountGroupId }).ToList();
    }

    public Account GetByBusinessIdAndAccountCode(int businessId, string accountCode)
    {
        var sql = "SELECT * FROM Account"
            + " WHERE BusinessId = @BusinessId AND AccountCode = @AccountCode";
        return _db.Query<Account>(sql, new
        {
            @BusinessId = businessId,
            @AccountCode = accountCode,
        }).SingleOrDefault();
    }

    public Account GetById(long id)
    {
        var sql = "SELECT * FROM Account"
            + " WHERE AccountId = @AccountId";
        return _db.Query<Account>(sql, new { @AccountId = id }).SingleOrDefault();
    }

    public Account GetExtById(long id)
    {
        var sql = "SELECT * FROM Account a"
            + " INNER JOIN Branch b ON a.BranchId = b.BranchId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " INNER JOIN AccountType at ON a.AccountTypeId = at.AccountTypeId"
            + " WHERE a.AccountId = @AccountId";
        return _db.Query<Account, Branch, AccountGroup, AccountType, Account>(sql,
            (account, branch, accountGroup, accountType) =>
            {
                account.Branch = branch;
                account.AccountGroup = accountGroup;
                account.AccountType = accountType;
                return account;
            }, new { @AccountId = id },
            splitOn: "BranchId,AccountGroupId,AccountTypeId").SingleOrDefault();
    }

    public List<Account> GetExtsByBusinessId(int businessId)
    {
        var sql = "SELECT * FROM Account a"
            + " INNER JOIN Branch b ON a.BranchId = b.BranchId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " INNER JOIN AccountType at ON a.AccountTypeId = at.AccountTypeId"
            + " WHERE a.BusinessId = @BusinessId";
        return _db.Query<Account, Branch, AccountGroup, AccountType, Account>(sql,
            (account, branch, accountGroup, accountType) =>
            {
                account.Branch = branch;
                account.AccountGroup = accountGroup;
                account.AccountType = accountType;
                return account;
            }, new { @BusinessId = businessId },
            splitOn: "BranchId,AccountGroupId,AccountTypeId").ToList();
    }

    public List<Account> GetExtsByBusinessIdAndAccountGroupCodes(int businessId, string[] accountGroupCodes)
    {
        var sql = "SELECT * FROM Account a"
            + " INNER JOIN Branch b ON a.BranchId = b.BranchId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " INNER JOIN AccountType at ON a.AccountTypeId = at.AccountTypeId"
            + " WHERE a.BusinessId = @BusinessId AND ag.AccountGroupCode IN @AccountGroupCodes";
        return _db.Query<Account, Branch, AccountGroup, AccountType, Account>(sql,
            (account, branch, accountGroup, accountType) =>
            {
                account.Branch = branch;
                account.AccountGroup = accountGroup;
                account.AccountType = accountType;
                return account;
            }, new
            {
                @BusinessId = businessId,
                @AccountGroupCodes = accountGroupCodes,
            },
            splitOn: "BranchId,AccountGroupId,AccountTypeId").ToList();
    }

    public Account GetLastAccountOrderForAnAccountGroup(int businessId, long branchId, string accountGroupCode)
    {
        var sql = "SELECT * FROM Account"
            + " WHERE BusinessId = @BusinessId"
            + " AND BranchId = @BranchId"
            + " AND AccountGroupId = (SELECT AccountGroupId FROM AccountGroup WHERE AccountGroupCode = @AccountGroupCode)"
            + " AND AccountOrder = (SELECT MAX(AccountOrder) FROM Account WHERE BusinessId = @BusinessId AND BranchId = @BranchId AND AccountGroupId = (SELECT AccountGroupId FROM AccountGroup WHERE AccountGroupCode = @AccountGroupCode))";
        return _db.Query<Account>(sql, new
        {
            @BusinessId = businessId,
            @BranchId = branchId,
            @AccountGroupCode = accountGroupCode,
        }).SingleOrDefault();
    }

    public Account GetMaxAccountOrderByBusinessIdAndBranchIdAndAccountGroupId(int businessId, long branchId, short accountGroupId)
    {
        var sql = "SELECT * FROM Account"
            + " WHERE BusinessId = @BusinessId AND BranchId = @BranchId AND AccountGroupId = @AccountGroupId AND AccountOrder ="
            + " (SELECT MAX(AccountOrder) FROM Account"
            + " WHERE BusinessId = @BusinessId AND BranchId = @BranchId AND AccountGroupId = @AccountGroupId)";
        return _db.Query<Account>(sql, new
        {
            @BusinessId = businessId,
            @BranchId = branchId,
            @AccountGroupId = accountGroupId,
        }).SingleOrDefault();
    }

    public void Update(Account account)
    {
        var sql = "UPDATE Account SET BusinessId = @BusinessId, BranchId = @BranchId, AccountGroupId = @AccountGroupId, AccountTypeId = @AccountTypeId, AccountOrder = @AccountOrder, AccountName = @AccountName, AccountCode = @AccountCode, DebitBalance = @DebitBalance, CreditBalance = @CreditBalance, Balance = @Balance, Limit = @Limit, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt"
            + " WHERE AccountId = @AccountId";
        _db.Execute(sql, account);
    }
}

﻿using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.ExtendedDatabaseModels;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsAccountDal : IAccountDal
{
    private readonly DapperContext _context;

    public DpMsAccountDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(Account account)
    {
        using var connection = _context.CreateConnection();
        var sql = "INSERT INTO Account ("
            + " BusinessId,"
            + " BranchId,"
            + " AccountGroupId,"
            + " AccountTypeId,"
            + " AccountOrder,"
            + " AccountName,"
            + " AccountCode,"
            + " DebitBalance,"
            + " CreditBalance,"
            + " Balance,"
            + " Limit,"
            + " CreatedAt,"
            + " UpdatedAt)"
            + " VALUES(@BusinessId,"
            + " @BranchId,"
            + " @AccountGroupId,"
            + " @AccountTypeId,"
            + " @AccountOrder,"
            + " @AccountName,"
            + " @AccountCode,"
            + " @DebitBalance,"
            + " @CreditBalance,"
            + " @Balance,"
            + " @Limit,"
            + " @CreatedAt,"
            + " @UpdatedAt)"
            + " SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
        return connection.Query<long>(sql, account).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM Account"
            + " WHERE AccountId = @AccountId";
        connection.Execute(sql, new { @AccountId = id });
    }

    public List<Account> GetByAccountGroupId(short accountGroupId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " AccountId,"
            + " BusinessId,"
            + " BranchId,"
            + " AccountGroupId,"
            + " AccountTypeId,"
            + " AccountOrder,"
            + " AccountName,"
            + " AccountCode,"
            + " DebitBalance,"
            + " CreditBalance,"
            + " Balance,"
            + " Limit,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Account"
            + " WHERE AccountGroupId = @AccountGroupId";
        return connection.Query<Account>(sql, new { @AccountGroupId = accountGroupId }).ToList();
    }

    public Account GetByBusinessIdAndAccountCode(int businessId, string accountCode)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " AccountId,"
            + " BusinessId,"
            + " BranchId,"
            + " AccountGroupId,"
            + " AccountTypeId,"
            + " AccountOrder,"
            + " AccountName,"
            + " AccountCode,"
            + " DebitBalance,"
            + " CreditBalance,"
            + " Balance,"
            + " Limit,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Account"
            + " WHERE BusinessId = @BusinessId AND AccountCode = @AccountCode";
        return connection.Query<Account>(sql, new
        {
            @BusinessId = businessId,
            @AccountCode = accountCode,
        }).SingleOrDefault();
    }

    public Account GetById(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " AccountId,"
            + " BusinessId,"
            + " BranchId,"
            + " AccountGroupId,"
            + " AccountTypeId,"
            + " AccountOrder,"
            + " AccountName,"
            + " AccountCode,"
            + " DebitBalance,"
            + " CreditBalance,"
            + " Balance,"
            + " Limit,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Account"
            + " WHERE AccountId = @AccountId";
        return connection.Query<Account>(sql, new { @AccountId = id }).SingleOrDefault();
    }

    public AccountExt GetExtById(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " a.AccountId,"
            + " a.BusinessId,"
            + " a.BranchId,"
            + " a.AccountGroupId,"
            + " a.AccountTypeId,"
            + " a.AccountOrder,"
            + " a.AccountName,"
            + " a.AccountCode,"
            + " a.DebitBalance,"
            + " a.CreditBalance,"
            + " a.Balance,"
            + " a.Limit,"
            + " a.CreatedAt,"
            + " a.UpdatedAt,"
            + " b.BranchName,"
            + " ag.AccountGroupName,"
            + " ag.AccountGroupCode,"
            + " at.AccountTypeName"
            + " FROM Account a"
            + " INNER JOIN Branch b ON a.BranchId = b.BranchId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " INNER JOIN AccountType at ON a.AccountTypeId = at.AccountTypeId"
            + " WHERE a.AccountId = @AccountId";
        return connection.Query<AccountExt>(sql, new { @AccountId = id }).SingleOrDefault();
    }

    public List<AccountExt> GetExtsByBusinessId(int businessId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " a.AccountId,"
            + " a.BusinessId,"
            + " a.BranchId,"
            + " a.AccountGroupId,"
            + " a.AccountTypeId,"
            + " a.AccountOrder,"
            + " a.AccountName,"
            + " a.AccountCode,"
            + " a.DebitBalance,"
            + " a.CreditBalance,"
            + " a.Balance,"
            + " a.Limit,"
            + " a.CreatedAt,"
            + " a.UpdatedAt,"
            + " b.BranchName,"
            + " ag.AccountGroupName,"
            + " ag.AccountGroupCode,"
            + " at.AccountTypeName"
            + " FROM Account a"
            + " INNER JOIN Branch b ON a.BranchId = b.BranchId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " INNER JOIN AccountType at ON a.AccountTypeId = at.AccountTypeId"
            + " WHERE a.BusinessId = @BusinessId";
        return connection.Query<AccountExt>(sql, new { @BusinessId = businessId }).ToList();
    }

    public List<AccountExt> GetExtsByBusinessIdAndAccountGroupCodes(int businessId, string[] accountGroupCodes)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " a.AccountId,"
            + " a.BusinessId,"
            + " a.BranchId,"
            + " a.AccountGroupId,"
            + " a.AccountTypeId,"
            + " a.AccountOrder,"
            + " a.AccountName,"
            + " a.AccountCode,"
            + " a.DebitBalance,"
            + " a.CreditBalance,"
            + " a.Balance,"
            + " a.Limit,"
            + " a.CreatedAt,"
            + " a.UpdatedAt,"
            + " b.BranchName,"
            + " ag.AccountGroupName,"
            + " ag.AccountGroupCode,"
            + " at.AccountTypeName"
            + " FROM Account a"
            + " INNER JOIN Branch b ON a.BranchId = b.BranchId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " INNER JOIN AccountType at ON a.AccountTypeId = at.AccountTypeId"
            + " WHERE a.BusinessId = @BusinessId AND ag.AccountGroupCode IN @AccountGroupCodes";
        return connection.Query<AccountExt>(sql, new
            {
                @BusinessId = businessId,
                @AccountGroupCodes = accountGroupCodes,
            }).ToList();
    }

    public int GetMaxAccountOrderByBusinessIdAndBranchIdAndAccountGroupId(int businessId, long branchId, short accountGroupId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT ISNULL(MAX(AccountOrder), 0)" 
            + " FROM Account" 
            + " WHERE BusinessId = @BusinessId"
            + " AND BranchId = @BranchId"
            + " AND AccountGroupId = @AccountGroupId";
        return connection.Query<int>(sql, new
        {
            @BusinessId = businessId,
            @BranchId = branchId,
            @AccountGroupId = accountGroupId,
        }).SingleOrDefault();
    }

    public void Update(Account account)
    {
        using var connection = _context.CreateConnection();
        var sql = "UPDATE Account SET"
            + " BusinessId = @BusinessId,"
            + " BranchId = @BranchId,"
            + " AccountGroupId = @AccountGroupId,"
            + " AccountTypeId = @AccountTypeId,"
            + " AccountOrder = @AccountOrder,"
            + " AccountName = @AccountName,"
            + " AccountCode = @AccountCode,"
            + " DebitBalance = @DebitBalance,"
            + " CreditBalance = @CreditBalance,"
            + " Balance = @Balance,"
            + " Limit = @Limit,"
            + " CreatedAt = @CreatedAt,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE AccountId = @AccountId";
        connection.Execute(sql, account);
    }
}

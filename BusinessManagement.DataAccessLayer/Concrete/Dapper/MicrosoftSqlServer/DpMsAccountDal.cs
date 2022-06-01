using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
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

    public long Add(AccountDto accountDto)
    {
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
        return _db.Query<long>(sql, accountDto).Single();
    }

    public void Delete(long id)
    {
        var sql = "DELETE FROM Account"
            + " WHERE AccountId = @AccountId";
        _db.Execute(sql, new { @AccountId = id });
    }

    public List<AccountDto> GetByAccountGroupId(short accountGroupId)
    {
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
        return _db.Query<AccountDto>(sql, new { @AccountGroupId = accountGroupId }).ToList();
    }

    public AccountDto GetByBusinessIdAndAccountCode(int businessId, string accountCode)
    {
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
        return _db.Query<AccountDto>(sql, new
        {
            @BusinessId = businessId,
            @AccountCode = accountCode,
        }).SingleOrDefault();
    }

    public AccountDto GetById(long id)
    {
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
        return _db.Query<AccountDto>(sql, new { @AccountId = id }).SingleOrDefault();
    }

    public AccountExtDto GetExtById(long id)
    {
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
        return _db.Query<AccountExtDto>(sql, new { @AccountId = id }).SingleOrDefault();
    }

    public List<AccountExtDto> GetExtsByBusinessId(int businessId)
    {
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
        return _db.Query<AccountExtDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public List<AccountExtDto> GetExtsByBusinessIdAndAccountGroupCodes(int businessId, string[] accountGroupCodes)
    {
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
        return _db.Query<AccountExtDto>(sql, new
            {
                @BusinessId = businessId,
                @AccountGroupCodes = accountGroupCodes,
            }).ToList();
    }

    public int GetMaxAccountOrderByBusinessIdAndBranchIdAndAccountGroupId(int businessId, long branchId, short accountGroupId)
    {
        var sql = "SELECT ISNULL(MAX(AccountOrder), 0)" 
            + " FROM Account" 
            + " WHERE BusinessId = @BusinessId"
            + " AND BranchId = @BranchId"
            + " AND AccountGroupId = @AccountGroupId";
        return _db.Query<int>(sql, new
        {
            @BusinessId = businessId,
            @BranchId = branchId,
            @AccountGroupId = accountGroupId,
        }).SingleOrDefault();
    }

    public void Update(AccountDto accountDto)
    {
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
        _db.Execute(sql, accountDto);
    }
}

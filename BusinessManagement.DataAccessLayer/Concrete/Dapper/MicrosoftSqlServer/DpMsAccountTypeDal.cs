using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsAccountTypeDal : IAccountTypeDal
{
    private readonly DapperContext _context;

    public DpMsAccountTypeDal(DapperContext context)
    {
        _context = context;
    }

    public List<AccountType> GetAll()
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " AccountTypeId,"
            + " AccountTypeName"
            + " FROM AccountType";
        return connection.Query<AccountType>(sql).ToList();
    }

    public AccountType GetByAccountTypeName(string accountTypeName)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " AccountTypeId,"
            + " AccountTypeName"
            + " FROM AccountType"
            + " WHERE AccountTypeName = @AccountTypeName";
        return connection.Query<AccountType>(sql, new { @AccountTypeName = accountTypeName }).SingleOrDefault();
    }

    public List<AccountType> GetByAccountTypeNames(string[] accountTypeNames)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " AccountTypeId,"
            + " AccountTypeName"
            + " FROM AccountType"
            + " WHERE AccountTypeName IN @AccountTypeNames";
        return connection.Query<AccountType>(sql, new { @AccountTypeNames = accountTypeNames }).ToList();
    }

    public AccountType GetById(short id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " AccountTypeId,"
            + " AccountTypeName"
            + " FROM AccountType"
            + " WHERE AccountTypeId = @AccountTypeId";
        return connection.Query<AccountType>(sql, new { @AccountTypeId = id }).SingleOrDefault();
    }
}

using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsAccountGroupDal : IAccountGroupDal
{
    private readonly DapperContext _context;

    public DpMsAccountGroupDal(DapperContext context)
    {
        _context = context;
    }

    public List<AccountGroup> GetAll()
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " AccountGroupId,"
            + " AccountGroupName,"
            + " AccountGroupCode"
            + " FROM AccountGroup";
        return connection.Query<AccountGroup>(sql).ToList();
    }

    public AccountGroup GetByAccountGroupCode(string accountGroupCode)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " AccountGroupId,"
            + " AccountGroupName,"
            + " AccountGroupCode"
            + " FROM AccountGroup"
            + " WHERE AccountGroupCode = @AccountGroupCode";
        return connection.Query<AccountGroup>(sql, new
        {
            @AccountGroupCode = accountGroupCode,
        }).SingleOrDefault();
    }

    public List<AccountGroup> GetByAccountGroupCodes(string[] accountGroupCodes)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
           + " AccountGroupId,"
           + " AccountGroupName,"
           + " AccountGroupCode"
           + " FROM AccountGroup"
           + " WHERE AccountGroupCode IN @AccountGroupCodes";
        return connection.Query<AccountGroup>(sql, new
        {
            @AccountGroupCodes = accountGroupCodes,
        }).ToList();
    }

    public AccountGroup GetById(short id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " AccountGroupId,"
            + " AccountGroupName,"
            + " AccountGroupCode"
            + " FROM AccountGroup"
            + " WHERE AccountGroupId = @AccountGroupId";
        return connection.Query<AccountGroup>(sql, new { @AccountGroupId = id }).SingleOrDefault();
    }
}

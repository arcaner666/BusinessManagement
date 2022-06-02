using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsAccountGroupDal : IAccountGroupDal
{
    private readonly DapperContext _context;

    public DpMsAccountGroupDal(DapperContext context)
    {
        _context = context;
    }

    public List<AccountGroupDto> GetAll()
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " AccountGroupId,"
            + " AccountGroupName,"
            + " AccountGroupCode"
            + " FROM AccountGroup";
        return connection.Query<AccountGroupDto>(sql).ToList();
    }

    public AccountGroupDto GetByAccountGroupCode(string accountGroupCode)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " AccountGroupId,"
            + " AccountGroupName,"
            + " AccountGroupCode"
            + " FROM AccountGroup"
            + " WHERE AccountGroupCode = @AccountGroupCode";
        return connection.Query<AccountGroupDto>(sql, new
        {
            @AccountGroupCode = accountGroupCode,
        }).SingleOrDefault();
    }

    public List<AccountGroupDto> GetByAccountGroupCodes(string[] accountGroupCodes)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
           + " AccountGroupId,"
           + " AccountGroupName,"
           + " AccountGroupCode"
           + " FROM AccountGroup"
           + " WHERE AccountGroupCode IN @AccountGroupCodes";
        return connection.Query<AccountGroupDto>(sql, new
        {
            @AccountGroupCodes = accountGroupCodes,
        }).ToList();
    }

    public AccountGroupDto GetById(short id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " AccountGroupId,"
            + " AccountGroupName,"
            + " AccountGroupCode"
            + " FROM AccountGroup"
            + " WHERE AccountGroupId = @AccountGroupId";
        return connection.Query<AccountGroupDto>(sql, new { @AccountGroupId = id }).SingleOrDefault();
    }
}

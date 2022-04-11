using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsAccountGroupDal : IAccountGroupDal
{
    private readonly IDbConnection _db;

    public DpMsAccountGroupDal(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public List<AccountGroup> GetAll()
    {
        var sql = "SELECT * FROM AccountGroup";
        return _db.Query<AccountGroup>(sql).ToList();
    }

    public AccountGroup GetByAccountGroupCode(string accountGroupCode)
    {
        var sql = "SELECT * FROM AccountGroup"
            + " WHERE AccountGroupCode = @AccountGroupCode";
        return _db.Query<AccountGroup>(sql, new
        {
            @AccountGroupCode = accountGroupCode,
        }).SingleOrDefault();
    }

    public List<AccountGroup> GetByAccountGroupCodes(string[] accountGroupCodes)
    {
        var sql = "SELECT * FROM AccountGroup"
            + " WHERE AccountGroupCode IN @AccountGroupCodes";
        return _db.Query<AccountGroup>(sql, new
        {
            @AccountGroupCodes = accountGroupCodes,
        }).ToList();
    }

    public AccountGroup GetById(short id)
    {
        var sql = "SELECT * FROM AccountGroup"
            + " WHERE AccountGroupId = @AccountGroupId";
        return _db.Query<AccountGroup>(sql, new { @AccountGroupId = id }).SingleOrDefault();
    }
}

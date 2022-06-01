using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
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

    public List<AccountGroupDto> GetAll()
    {
        var sql = "SELECT"
            + " AccountGroupId,"
            + " AccountGroupName,"
            + " AccountGroupCode"
            + " FROM AccountGroup";
        return _db.Query<AccountGroupDto>(sql).ToList();
    }

    public AccountGroupDto GetByAccountGroupCode(string accountGroupCode)
    {
        var sql = "SELECT"
            + " AccountGroupId,"
            + " AccountGroupName,"
            + " AccountGroupCode"
            + " FROM AccountGroup"
            + " WHERE AccountGroupCode = @AccountGroupCode";
        return _db.Query<AccountGroupDto>(sql, new
        {
            @AccountGroupCode = accountGroupCode,
        }).SingleOrDefault();
    }

    public List<AccountGroupDto> GetByAccountGroupCodes(string[] accountGroupCodes)
    {
        var sql = "SELECT"
           + " AccountGroupId,"
           + " AccountGroupName,"
           + " AccountGroupCode"
           + " FROM AccountGroup"
           + " WHERE AccountGroupCode IN @AccountGroupCodes";
        return _db.Query<AccountGroupDto>(sql, new
        {
            @AccountGroupCodes = accountGroupCodes,
        }).ToList();
    }

    public AccountGroupDto GetById(short id)
    {
        var sql = "SELECT"
            + " AccountGroupId,"
            + " AccountGroupName,"
            + " AccountGroupCode"
            + " FROM AccountGroup"
            + " WHERE AccountGroupId = @AccountGroupId";
        return _db.Query<AccountGroupDto>(sql, new { @AccountGroupId = id }).SingleOrDefault();
    }
}

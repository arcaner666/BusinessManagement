using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsAccountTypeDal : IAccountTypeDal
{
    private readonly IDbConnection _db;

    public DpMsAccountTypeDal(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public List<AccountTypeDto> GetAll()
    {
        var sql = "SELECT"
            + " AccountTypeId,"
            + " AccountTypeName"
            + " FROM AccountType";
        return _db.Query<AccountTypeDto>(sql).ToList();
    }

    public AccountTypeDto GetByAccountTypeName(string accountTypeName)
    {
        var sql = "SELECT"
            + " AccountTypeId,"
            + " AccountTypeName"
            + " FROM AccountType"
            + " WHERE AccountTypeName = @AccountTypeName";
        return _db.Query<AccountTypeDto>(sql, new { @AccountTypeName = accountTypeName }).SingleOrDefault();
    }

    public List<AccountTypeDto> GetByAccountTypeNames(string[] accountTypeNames)
    {
        var sql = "SELECT"
            + " AccountTypeId,"
            + " AccountTypeName"
            + " FROM AccountType"
            + " WHERE AccountTypeName IN @AccountTypeNames";
        return _db.Query<AccountTypeDto>(sql, new { @AccountTypeNames = accountTypeNames }).ToList();
    }

    public AccountTypeDto GetById(short id)
    {
        var sql = "SELECT"
            + " AccountTypeId,"
            + " AccountTypeName"
            + " FROM AccountType"
            + " WHERE AccountTypeId = @AccountTypeId";
        return _db.Query<AccountTypeDto>(sql, new { @AccountTypeId = id }).SingleOrDefault();
    }
}

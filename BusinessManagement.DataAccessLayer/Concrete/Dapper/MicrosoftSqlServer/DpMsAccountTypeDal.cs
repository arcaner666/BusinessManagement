using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
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

    public List<AccountType> GetAll()
    {
        var sql = "SELECT * FROM AccountType";
        return _db.Query<AccountType>(sql).ToList();
    }

    public AccountType GetByAccountTypeName(string accountTypeName)
    {
        var sql = "SELECT * FROM AccountType"
            + " WHERE AccountTypeName = @AccountTypeName";
        return _db.Query<AccountType>(sql, new { @AccountTypeName = accountTypeName }).SingleOrDefault();
    }

    public AccountType GetById(short id)
    {
        var sql = "SELECT * FROM AccountType"
            + " WHERE AccountTypeId = @AccountTypeId";
        return _db.Query<AccountType>(sql, new { @AccountTypeId = id }).SingleOrDefault();
    }
}

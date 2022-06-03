using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsAccountTypeDal : IAccountTypeDal
{
    private readonly DapperContext _context;

    public DpMsAccountTypeDal(DapperContext context)
    {
        _context = context;
    }

    public IEnumerable<AccountTypeDto> GetAll()
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " AccountTypeId,"
            + " AccountTypeName"
            + " FROM AccountType";
        return connection.Query<AccountTypeDto>(sql).ToList();
    }

    public AccountTypeDto GetByAccountTypeName(string accountTypeName)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " AccountTypeId,"
            + " AccountTypeName"
            + " FROM AccountType"
            + " WHERE AccountTypeName = @AccountTypeName";
        return connection.Query<AccountTypeDto>(sql, new { @AccountTypeName = accountTypeName }).SingleOrDefault();
    }

    public IEnumerable<AccountTypeDto> GetByAccountTypeNames(string[] accountTypeNames)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " AccountTypeId,"
            + " AccountTypeName"
            + " FROM AccountType"
            + " WHERE AccountTypeName IN @AccountTypeNames";
        return connection.Query<AccountTypeDto>(sql, new { @AccountTypeNames = accountTypeNames }).ToList();
    }

    public AccountTypeDto GetById(short id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " AccountTypeId,"
            + " AccountTypeName"
            + " FROM AccountType"
            + " WHERE AccountTypeId = @AccountTypeId";
        return connection.Query<AccountTypeDto>(sql, new { @AccountTypeId = id }).SingleOrDefault();
    }
}

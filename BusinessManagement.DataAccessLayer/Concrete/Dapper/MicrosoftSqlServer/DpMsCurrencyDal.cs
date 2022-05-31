using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsCurrencyDal : ICurrencyDal
{
    private readonly IDbConnection _db;

    public DpMsCurrencyDal(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public List<CurrencyDto> GetAll()
    {
        var sql = "SELECT"
            + " CurrencyId,"
            + " CurrencyName,"
            + " CurrencySymbol"
            + " FROM Currency";
        return _db.Query<CurrencyDto>(sql).ToList();
    }        
    
    public CurrencyDto GetByCurrencyName(string currencyName)
    {
        var sql = "SELECT"
            + " CurrencyId,"
            + " CurrencyName,"
            + " CurrencySymbol"
            + " FROM Currency"
            + " WHERE CurrencyName = @CurrencyName";
        return _db.Query<CurrencyDto>(sql, new { @CurrencyName = currencyName }).SingleOrDefault();
    }
}

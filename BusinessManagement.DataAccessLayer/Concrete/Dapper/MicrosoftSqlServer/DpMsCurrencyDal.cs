using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsCurrencyDal : ICurrencyDal
{
    private readonly DapperContext _context;

    public DpMsCurrencyDal(DapperContext context)
    {
        _context = context;
    }

    public List<Currency> GetAll()
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " CurrencyId,"
            + " CurrencyName,"
            + " CurrencySymbol"
            + " FROM Currency";
        return connection.Query<Currency>(sql).ToList();
    }        
    
    public Currency GetByCurrencyName(string currencyName)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " CurrencyId,"
            + " CurrencyName,"
            + " CurrencySymbol"
            + " FROM Currency"
            + " WHERE CurrencyName = @CurrencyName";
        return connection.Query<Currency>(sql, new { @CurrencyName = currencyName }).SingleOrDefault();
    }
}

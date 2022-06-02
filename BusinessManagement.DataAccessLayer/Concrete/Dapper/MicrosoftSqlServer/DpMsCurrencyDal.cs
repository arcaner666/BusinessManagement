using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsCurrencyDal : ICurrencyDal
{
    private readonly DapperContext _context;

    public DpMsCurrencyDal(DapperContext context)
    {
        _context = context;
    }

    public List<CurrencyDto> GetAll()
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " CurrencyId,"
            + " CurrencyName,"
            + " CurrencySymbol"
            + " FROM Currency";
        return connection.Query<CurrencyDto>(sql).ToList();
    }        
    
    public CurrencyDto GetByCurrencyName(string currencyName)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " CurrencyId,"
            + " CurrencyName,"
            + " CurrencySymbol"
            + " FROM Currency"
            + " WHERE CurrencyName = @CurrencyName";
        return connection.Query<CurrencyDto>(sql, new { @CurrencyName = currencyName }).SingleOrDefault();
    }
}

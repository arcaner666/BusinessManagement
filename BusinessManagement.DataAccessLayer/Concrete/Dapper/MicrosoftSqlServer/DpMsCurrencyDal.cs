using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer
{
    public class DpMsCurrencyDal : ICurrencyDal
    {
        private readonly IDbConnection _db;

        public DpMsCurrencyDal(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public List<Currency> GetAll()
        {
            var sql = "SELECT CurrencyId, CurrencyName, CurrencySymbol"
                + " FROM Currency";
            return _db.Query<Currency>(sql).ToList();
        }        
        
        public Currency GetByCurrencyName(string currencyName)
        {
            var sql = "SELECT CurrencyId, CurrencyName, CurrencySymbol"
                + " FROM Currency"
                + " WHERE CurrencyName = @CurrencyName";
            return _db.Query<Currency>(sql, new { @CurrencyName = currencyName }).SingleOrDefault();
        }
    }
}

using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer
{
    public class DpMsCityDal : ICityDal
    {
        private readonly IDbConnection _db;

        public DpMsCityDal(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public List<City> GetAll()
        {
            var sql = "SELECT * FROM City";
            return _db.Query<City>(sql).ToList();
        }
    }
}

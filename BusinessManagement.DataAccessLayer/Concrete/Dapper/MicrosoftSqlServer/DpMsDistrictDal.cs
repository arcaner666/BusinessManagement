using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsDistrictDal : IDistrictDal
{
    private readonly IDbConnection _db;

    public DpMsDistrictDal(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public List<District> GetAll()
    {
        var sql = "SELECT * FROM District";
        return _db.Query<District>(sql).ToList();
    }

    public List<District> GetByCityId(short cityId)
    {
        var sql = "SELECT * FROM District"
            + " WHERE CityId = @CityId";
        return _db.Query<District>(sql, new { @CityId = cityId }).ToList();
    }
}

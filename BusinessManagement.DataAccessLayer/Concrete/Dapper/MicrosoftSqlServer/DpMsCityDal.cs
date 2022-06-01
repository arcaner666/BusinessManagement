using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsCityDal : ICityDal
{
    private readonly IDbConnection _db;

    public DpMsCityDal(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public List<CityDto> GetAll()
    {
        var sql = "SELECT" 
            + " CityId,"
            + " PlateCode,"
            + " CityName"
            + " FROM City";
        return _db.Query<CityDto>(sql).ToList();
    }
}

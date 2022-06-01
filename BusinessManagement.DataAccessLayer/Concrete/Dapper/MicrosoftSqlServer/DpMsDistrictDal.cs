using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
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

    public List<DistrictDto> GetAll()
    {
        var sql = "SELECT"
            + " DistrictId,"
            + " CityId,"
            + " DistrictName"
            + " FROM District";
        return _db.Query<DistrictDto>(sql).ToList();
    }

    public List<DistrictDto> GetByCityId(short cityId)
    {
        var sql = "SELECT"
            + " DistrictId,"
            + " CityId,"
            + " DistrictName"
            + " FROM District"
            + " WHERE CityId = @CityId";
        return _db.Query<DistrictDto>(sql, new { @CityId = cityId }).ToList();
    }
}

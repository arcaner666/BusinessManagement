using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsDistrictDal : IDistrictDal
{
    private readonly DapperContext _context;

    public DpMsDistrictDal(DapperContext context)
    {
        _context = context;
    }

    public List<District> GetAll()
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " DistrictId,"
            + " CityId,"
            + " DistrictName"
            + " FROM District";
        return connection.Query<District>(sql).ToList();
    }

    public List<District> GetByCityId(short cityId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " DistrictId,"
            + " CityId,"
            + " DistrictName"
            + " FROM District"
            + " WHERE CityId = @CityId";
        return connection.Query<District>(sql, new { @CityId = cityId }).ToList();
    }
}

using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsDistrictDal : IDistrictDal
{
    private readonly DapperContext _context;

    public DpMsDistrictDal(DapperContext context)
    {
        _context = context;
    }

    public List<DistrictDto> GetAll()
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " DistrictId,"
            + " CityId,"
            + " DistrictName"
            + " FROM District";
        return connection.Query<DistrictDto>(sql).ToList();
    }

    public List<DistrictDto> GetByCityId(short cityId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " DistrictId,"
            + " CityId,"
            + " DistrictName"
            + " FROM District"
            + " WHERE CityId = @CityId";
        return connection.Query<DistrictDto>(sql, new { @CityId = cityId }).ToList();
    }
}

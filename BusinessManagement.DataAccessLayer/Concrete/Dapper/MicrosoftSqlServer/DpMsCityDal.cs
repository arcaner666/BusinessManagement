using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsCityDal : ICityDal
{
    private readonly DapperContext _context;

    public DpMsCityDal(DapperContext context)
    {
        _context = context;
    }

    public IEnumerable<CityDto> GetAll()
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT" 
            + " CityId,"
            + " PlateCode,"
            + " CityName"
            + " FROM City";
        return connection.Query<CityDto>(sql).ToList();
    }
}

using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsCityDal : ICityDal
{
    private readonly DapperContext _context;

    public DpMsCityDal(DapperContext context)
    {
        _context = context;
    }

    public List<City> GetAll()
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT" 
            + " CityId,"
            + " PlateCode,"
            + " CityName"
            + " FROM City";
        return connection.Query<City>(sql).ToList();
    }
}

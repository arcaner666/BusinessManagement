using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsEmployeeTypeDal : IEmployeeTypeDal
{
    private readonly DapperContext _context;

    public DpMsEmployeeTypeDal(DapperContext context)
    {
        _context = context;
    }

    public IEnumerable<EmployeeType> GetAll()
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " EmployeeTypeId,"
            + " EmployeeTypeName"
            + " FROM EmployeeType";
        return connection.Query<EmployeeType>(sql).ToList();
    }
}

using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsEmployeeTypeDal : IEmployeeTypeDal
{
    private readonly DapperContext _context;

    public DpMsEmployeeTypeDal(DapperContext context)
    {
        _context = context;
    }

    public IEnumerable<EmployeeTypeDto> GetAll()
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " EmployeeTypeId,"
            + " EmployeeTypeName"
            + " FROM EmployeeType";
        return connection.Query<EmployeeTypeDto>(sql).ToList();
    }
}

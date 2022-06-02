using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsBusinessDal : IBusinessDal
{
    private readonly DapperContext _context;

    public DpMsBusinessDal(DapperContext context)
    {
        _context = context;
    }

    public int Add(BusinessDto businessDto)
    {
        using var connection = _context.CreateConnection();
        var sql = "INSERT INTO Business ("
            + " OwnerSystemUserId,"
            + " BusinessOrder,"
            + " BusinessName,"
            + " BusinessCode,"
            + " CreatedAt,"
            + " UpdatedAt)"
            + " VALUES("
            + " @OwnerSystemUserId,"
            + " @BusinessOrder,"
            + " @BusinessName,"
            + " @BusinessCode,"
            + " @CreatedAt,"
            + " @UpdatedAt)"
            + " SELECT CAST(SCOPE_IDENTITY() AS INT)";
        return connection.Query<int>(sql, businessDto).Single();
    }

    public BusinessDto GetByBusinessName(string businessName)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " BusinessId,"
            + " OwnerSystemUserId,"
            + " BusinessOrder,"
            + " BusinessName,"
            + " BusinessCode,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Business"
            + " WHERE BusinessName = @BusinessName";
        return connection.Query<BusinessDto>(sql, new { @BusinessName = businessName }).SingleOrDefault();
    }

    public BusinessDto GetById(int id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " BusinessId,"
            + " OwnerSystemUserId,"
            + " BusinessOrder,"
            + " BusinessName,"
            + " BusinessCode,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Business"
            + " WHERE BusinessId = @BusinessId";
        return connection.Query<BusinessDto>(sql, new { @BusinessId = id }).SingleOrDefault();
    }

    public BusinessDto GetByOwnerSystemUserId(long ownerSystemUserId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " BusinessId,"
            + " OwnerSystemUserId,"
            + " BusinessOrder,"
            + " BusinessName,"
            + " BusinessCode,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Business"
            + " WHERE OwnerSystemUserId = @OwnerSystemUserId";
        return connection.Query<BusinessDto>(sql, new { @OwnerSystemUserId = ownerSystemUserId }).SingleOrDefault();
    }        
}

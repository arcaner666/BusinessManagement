using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsBusinessDal : IBusinessDal
{
    private readonly IDbConnection _db;

    public DpMsBusinessDal(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public Business Add(Business business)
    {
        var sql = "INSERT INTO Business (OwnerSystemUserId, BusinessOrder, BusinessName, BusinessCode, CreatedAt, UpdatedAt)"
            + " VALUES(@OwnerSystemUserId, @BusinessOrder, @BusinessName, @BusinessCode, @CreatedAt, @UpdatedAt) SELECT CAST(SCOPE_IDENTITY() AS INT)";
        var id = _db.Query<int>(sql, business).Single();
        business.BusinessId = id;
        return business;
    }

    public Business GetByBusinessName(string businessName)
    {
        var sql = "SELECT * FROM Business"
            + " WHERE BusinessName = @BusinessName";
        return _db.Query<Business>(sql, new { @BusinessName = businessName }).SingleOrDefault();
    }

    public Business GetById(int id)
    {
        var sql = "SELECT * FROM Business"
            + " WHERE BusinessId = @BusinessId";
        return _db.Query<Business>(sql, new { @BusinessId = id }).SingleOrDefault();
    }

    public Business GetByOwnerSystemUserId(long ownerSystemUserId)
    {
        var sql = "SELECT * FROM Business"
            + " WHERE OwnerSystemUserId = @OwnerSystemUserId";
        return _db.Query<Business>(sql, new { @OwnerSystemUserId = ownerSystemUserId }).SingleOrDefault();
    }        
}

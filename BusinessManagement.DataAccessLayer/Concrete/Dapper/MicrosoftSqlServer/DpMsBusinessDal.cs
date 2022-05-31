using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
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

    public int Add(BusinessDto businessDto)
    {
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
        return _db.Query<int>(sql, businessDto).Single();
    }

    public BusinessDto GetByBusinessName(string businessName)
    {
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
        return _db.Query<BusinessDto>(sql, new { @BusinessName = businessName }).SingleOrDefault();
    }

    public BusinessDto GetById(int id)
    {
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
        return _db.Query<BusinessDto>(sql, new { @BusinessId = id }).SingleOrDefault();
    }

    public BusinessDto GetByOwnerSystemUserId(long ownerSystemUserId)
    {
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
        return _db.Query<BusinessDto>(sql, new { @OwnerSystemUserId = ownerSystemUserId }).SingleOrDefault();
    }        
}

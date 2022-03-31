using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsSystemUserDal : ISystemUserDal
{
    private readonly IDbConnection _db;

    public DpMsSystemUserDal(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public SystemUser Add(SystemUser systemUser)
    {
        var sql = "INSERT INTO SystemUser (Email, Phone, PasswordHash, PasswordSalt, Role, BusinessId, BranchId, Blocked, RefreshToken, RefreshTokenExpiryTime, CreatedAt, UpdatedAt)"
            + " VALUES(@Email, @Phone, @PasswordHash, @PasswordSalt, @Role, @BusinessId, @BranchId, @Blocked, @RefreshToken, @RefreshTokenExpiryTime, @CreatedAt, @UpdatedAt) SELECT CAST(SCOPE_IDENTITY() as int)";
        var id = _db.Query<int>(sql, systemUser).Single();
        systemUser.SystemUserId = id;
        return systemUser;
    }

    public void Delete(long id)
    {
        var sql = "DELETE FROM SystemUser"
            + " WHERE SystemUserId = @SystemUserId";
        _db.Execute(sql, new { @SystemUserId = id });
    }

    public SystemUser GetByEmail(string email)
    {
        var sql = "SELECT * FROM SystemUser"
            + " WHERE Email = @Email";
        return _db.Query<SystemUser>(sql, new { @Email = email }).SingleOrDefault();
    }

    public SystemUser GetById(long id)
    {
        var sql = "SELECT * FROM SystemUser"
            + " WHERE SystemUserId = @SystemUserId";
        return _db.Query<SystemUser>(sql, new { @SystemUserId = id }).SingleOrDefault();
    }

    public SystemUser GetByPhone(string phone)
    {
        var sql = "SELECT * FROM SystemUser"
            + " WHERE Phone = @Phone";
        return _db.Query<SystemUser>(sql, new { @Phone = phone }).SingleOrDefault();
    }

    public void Update(SystemUser systemUser)
    {
        var sql = "UPDATE SystemUser SET Email = @Email, Phone = @Phone, PasswordHash = @PasswordHash, PasswordSalt = @PasswordSalt, Role = @Role, BusinessId = @BusinessId, BranchId = @BranchId, Blocked = @Blocked, RefreshToken = @RefreshToken, RefreshTokenExpiryTime = @RefreshTokenExpiryTime, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt"
            + " WHERE SystemUserId = @SystemUserId";
        _db.Execute(sql, systemUser);
    }
}

using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
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

    public long Add(SystemUserDto systemUserDto)
    {
        var sql = "INSERT INTO SystemUser ("
            + " Email,"
            + " Phone,"
            + " PasswordHash,"
            + " PasswordSalt,"
            + " Role,"
            + " BusinessId,"
            + " BranchId,"
            + " Blocked,"
            + " RefreshToken,"
            + " RefreshTokenExpiryTime,"
            + " CreatedAt,"
            + " UpdatedAt)"
            + " VALUES("
            + " @Email,"
            + " @Phone,"
            + " @PasswordHash,"
            + " @PasswordSalt,"
            + " @Role,"
            + " @BusinessId,"
            + " @BranchId,"
            + " @Blocked,"
            + " @RefreshToken,"
            + " @RefreshTokenExpiryTime,"
            + " @CreatedAt,"
            + " @UpdatedAt)"
            + " SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
        return _db.Query<long>(sql, systemUserDto).Single();
    }

    public void Delete(long id)
    {
        var sql = "DELETE FROM SystemUser"
            + " WHERE SystemUserId = @SystemUserId";
        _db.Execute(sql, new { @SystemUserId = id });
    }

    public SystemUserDto GetByEmail(string email)
    {
        var sql = "SELECT"
            + " SystemUserId,"
            + " Email,"
            + " Phone,"
            + " PasswordHash,"
            + " PasswordSalt,"
            + " Role,"
            + " BusinessId,"
            + " BranchId,"
            + " Blocked,"
            + " RefreshToken,"
            + " RefreshTokenExpiryTime,"
            + " CreatedAt,"
            + " UpdatedAt)"
            + " FROM SystemUser su"
            + " WHERE Email = @Email";
        return _db.Query<SystemUserDto>(sql, new { @Email = email }).SingleOrDefault();
    }

    public SystemUserDto GetById(long id)
    {
        var sql = "SELECT"
            + " SystemUserId,"
            + " Email,"
            + " Phone,"
            + " PasswordHash,"
            + " PasswordSalt,"
            + " Role,"
            + " BusinessId,"
            + " BranchId,"
            + " Blocked,"
            + " RefreshToken,"
            + " RefreshTokenExpiryTime,"
            + " CreatedAt,"
            + " UpdatedAt)"
            + " FROM SystemUser"
            + " WHERE SystemUserId = @SystemUserId";
        return _db.Query<SystemUserDto>(sql, new { @SystemUserId = id }).SingleOrDefault();
    }

    public SystemUserDto GetByPhone(string phone)
    {
        var sql = "SELECT"
            + " SystemUserId,"
            + " Email,"
            + " Phone,"
            + " PasswordHash,"
            + " PasswordSalt,"
            + " Role,"
            + " BusinessId,"
            + " BranchId,"
            + " Blocked,"
            + " RefreshToken,"
            + " RefreshTokenExpiryTime,"
            + " CreatedAt,"
            + " UpdatedAt)"
            + " FROM SystemUser"
            + " WHERE Phone = @Phone";
        return _db.Query<SystemUserDto>(sql, new { @Phone = phone }).SingleOrDefault();
    }

    public void Update(SystemUserDto systemUserDto)
    {
        var sql = "UPDATE SystemUser SET"
            + " Email = @Email,"
            + " Phone = @Phone,"
            + " PasswordHash = @PasswordHash,"
            + " PasswordSalt = @PasswordSalt,"
            + " Role = @Role,"
            + " BusinessId = @BusinessId,"
            + " BranchId = @BranchId,"
            + " Blocked = @Blocked,"
            + " RefreshToken = @RefreshToken,"
            + " RefreshTokenExpiryTime = @RefreshTokenExpiryTime,"
            + " CreatedAt = @CreatedAt,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE SystemUserId = @SystemUserId";
        _db.Execute(sql, systemUserDto);
    }
}

using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsSystemUserDal : ISystemUserDal
{
    private readonly DapperContext _context;

    public DpMsSystemUserDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(SystemUserDto systemUserDto)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<long>(sql, systemUserDto).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM SystemUser"
            + " WHERE SystemUserId = @SystemUserId";
        connection.Execute(sql, new { @SystemUserId = id });
    }

    public SystemUserDto GetByEmail(string email)
    {
        using var connection = _context.CreateConnection();
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
            + " UpdatedAt"
            + " FROM SystemUser su"
            + " WHERE Email = @Email";
        return connection.Query<SystemUserDto>(sql, new { @Email = email }).SingleOrDefault();
    }

    public SystemUserDto GetById(long id)
    {
        using var connection = _context.CreateConnection();
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
            + " UpdatedAt"
            + " FROM SystemUser"
            + " WHERE SystemUserId = @SystemUserId";
        return connection.Query<SystemUserDto>(sql, new { @SystemUserId = id }).SingleOrDefault();
    }

    public SystemUserDto GetByPhone(string phone)
    {
        using var connection = _context.CreateConnection();
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
            + " UpdatedAt"
            + " FROM SystemUser"
            + " WHERE Phone = @Phone";
        return connection.Query<SystemUserDto>(sql, new { @Phone = phone }).SingleOrDefault();
    }

    public void Update(SystemUserDto systemUserDto)
    {
        using var connection = _context.CreateConnection();
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
        connection.Execute(sql, systemUserDto);
    }
}

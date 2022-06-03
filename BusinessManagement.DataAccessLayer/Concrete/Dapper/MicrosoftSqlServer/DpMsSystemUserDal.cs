using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsSystemUserDal : ISystemUserDal
{
    private readonly DapperContext _context;

    public DpMsSystemUserDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(SystemUser systemUser)
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
        return connection.Query<long>(sql, systemUser).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM SystemUser"
            + " WHERE SystemUserId = @SystemUserId";
        connection.Execute(sql, new { @SystemUserId = id });
    }

    public SystemUser GetByEmail(string email)
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
        return connection.Query<SystemUser>(sql, new { @Email = email }).SingleOrDefault();
    }

    public SystemUser GetById(long id)
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
        return connection.Query<SystemUser>(sql, new { @SystemUserId = id }).SingleOrDefault();
    }

    public SystemUser GetByPhone(string phone)
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
        return connection.Query<SystemUser>(sql, new { @Phone = phone }).SingleOrDefault();
    }

    public void Update(SystemUser systemUser)
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
        connection.Execute(sql, systemUser);
    }
}

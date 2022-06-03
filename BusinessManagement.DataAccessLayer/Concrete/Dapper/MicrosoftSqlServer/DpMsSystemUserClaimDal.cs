using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsSystemUserClaimDal : ISystemUserClaimDal
{
    private readonly DapperContext _context;

    public DpMsSystemUserClaimDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(SystemUserClaimDto systemUserClaimDto)
    {
        using var connection = _context.CreateConnection();
        var sql = "INSERT INTO SystemUserClaim ("
            + " SystemUserId,"
            + " OperationClaimId,"
            + " CreatedAt,"
            + " UpdatedAt)"
            + " VALUES("
            + " @SystemUserId,"
            + " @OperationClaimId,"
            + " @CreatedAt,"
            + " @UpdatedAt)"
            + " SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
        return connection.Query<long>(sql, systemUserClaimDto).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM SystemUserClaim"
            + " WHERE SystemUserClaimId = @SystemUserClaimId";
        connection.Execute(sql, new { @SystemUserClaimId = id });
    }

    public SystemUserClaimDto GetBySystemUserIdAndOperationClaimId(long systemUserId, int operationClaimId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " SystemUserClaimId,"
            + " SystemUserId,"
            + " OperationClaimId,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM SystemUserClaim"
            + " WHERE SystemUserId = @SystemUserId AND OperationClaimId = @OperationClaimId";
        return connection.Query<SystemUserClaimDto>(sql, new
        {
            @SystemUserId = systemUserId,
            @OperationClaimId = operationClaimId
        }).SingleOrDefault();
    }

    public IEnumerable<SystemUserClaimExtDto> GetExtsBySystemUserId(long systemUserId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " suc.SystemUserClaimId,"
            + " suc.SystemUserId,"
            + " suc.OperationClaimId,"
            + " suc.CreatedAt,"
            + " suc.UpdatedAt,"
            + " oc.OperationClaimName"
            + " FROM SystemUserClaim suc"
            + " INNER JOIN OperationClaim oc ON suc.OperationClaimId = oc.OperationClaimId"
            + " WHERE suc.SystemUserId = @SystemUserId";
        return connection.Query<SystemUserClaimExtDto>(sql, new { @SystemUserId = systemUserId }).ToList();
    }
}

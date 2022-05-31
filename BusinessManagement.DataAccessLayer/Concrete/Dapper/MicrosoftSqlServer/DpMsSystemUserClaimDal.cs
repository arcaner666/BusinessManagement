using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsSystemUserClaimDal : ISystemUserClaimDal
{
    private readonly IDbConnection _db;

    public DpMsSystemUserClaimDal(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public long Add(SystemUserClaimDto systemUserClaimDto)
    {
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
        return _db.Query<long>(sql, systemUserClaimDto).Single();
    }

    public void Delete(long id)
    {
        var sql = "DELETE FROM SystemUserClaim"
            + " WHERE SystemUserClaimId = @SystemUserClaimId";
        _db.Execute(sql, new { @SystemUserClaimId = id });
    }

    public SystemUserClaimDto GetBySystemUserIdAndOperationClaimId(long systemUserId, int operationClaimId)
    {
        var sql = "SELECT"
            + " SystemUserClaimId,"
            + " SystemUserId,"
            + " OperationClaimId,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM SystemUserClaim"
            + " WHERE SystemUserId = @SystemUserId AND OperationClaimId = @OperationClaimId";
        return _db.Query<SystemUserClaimDto>(sql, new
        {
            @SystemUserId = systemUserId,
            @OperationClaimId = operationClaimId
        }).SingleOrDefault();
    }

    public List<SystemUserClaimExtDto> GetExtsBySystemUserId(long systemUserId)
    {
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
        return _db.Query<SystemUserClaimExtDto>(sql, new { @SystemUserId = systemUserId }).ToList();
    }
}

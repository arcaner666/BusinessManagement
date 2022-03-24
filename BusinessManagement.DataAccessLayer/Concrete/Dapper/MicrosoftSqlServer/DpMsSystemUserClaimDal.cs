using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer
{
    public class DpMsSystemUserClaimDal : ISystemUserClaimDal
    {
        private readonly IDbConnection _db;

        public DpMsSystemUserClaimDal(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public SystemUserClaim Add(SystemUserClaim systemUserClaim)
        {
            var sql = "INSERT INTO SystemUserClaim (SystemUserId, OperationClaimId, CreatedAt, UpdatedAt)"
                + " VALUES(@SystemUserId, @OperationClaimId, @CreatedAt, @UpdatedAt) SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = _db.Query<int>(sql, systemUserClaim).Single();
            systemUserClaim.SystemUserClaimId = id;
            return systemUserClaim;
        }

        public void Delete(long id)
        {
            var sql = "DELETE FROM SystemUserClaim"
                + " WHERE SystemUserClaimId = @SystemUserClaimId";
            _db.Execute(sql, new { @SystemUserClaimId = id });
        }

        public SystemUserClaim GetBySystemUserIdAndOperationClaimId(long systemUserId, int operationClaimId)
        {
            var sql = "SELECT * FROM SystemUserClaim"
                + " WHERE SystemUserId = @SystemUserId AND OperationClaimId = @OperationClaimId";
            return _db.Query<SystemUserClaim>(sql, new
            {
                @SystemUserId = systemUserId,
                @OperationClaimId = operationClaimId
            }).SingleOrDefault();
        }

        public List<SystemUserClaim> GetExtsBySystemUserId(long systemUserId)
        {
            var sql = "SELECT * FROM SystemUserClaim suc"
                + " INNER JOIN OperationClaim oc ON suc.OperationClaimId = oc.OperationClaimId"
                + " WHERE suc.SystemUserId = @SystemUserId";
            return _db.Query<SystemUserClaim, OperationClaim, SystemUserClaim>(
                sql,
                (systemUserClaim, operationClaim) =>
                {
                    systemUserClaim.OperationClaim = operationClaim;
                    return systemUserClaim;
                },
                new { @SystemUserId = systemUserId },
                splitOn: "OperationClaimId").ToList();
        }
    }
}

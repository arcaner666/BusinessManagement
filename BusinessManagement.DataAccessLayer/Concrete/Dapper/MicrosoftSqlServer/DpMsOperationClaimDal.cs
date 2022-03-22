using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer
{
    public class DpMsOperationClaimDal : IOperationClaimDal
    {
        private readonly IDbConnection _db;

        public DpMsOperationClaimDal(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public List<OperationClaim> GetAll()
        {
            var sql = "SELECT OperationClaimId, OperationClaimName"
                + " FROM OperationClaim";
            return _db.Query<OperationClaim>(sql).ToList();
        }

        public OperationClaim GetByOperationClaimName(string operationClaimName)
        {
            var sql = "SELECT OperationClaimId, OperationClaimName"
                + " FROM OperationClaim"
                + " WHERE OperationClaimName = @OperationClaimName";
            return _db.Query<OperationClaim>(sql, new { @OperationClaimName = operationClaimName }).SingleOrDefault();
        }
    }
}

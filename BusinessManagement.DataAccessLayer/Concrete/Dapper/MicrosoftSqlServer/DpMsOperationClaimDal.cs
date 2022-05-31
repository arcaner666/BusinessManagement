using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsOperationClaimDal : IOperationClaimDal
{
    private readonly IDbConnection _db;

    public DpMsOperationClaimDal(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public List<OperationClaimDto> GetAll()
    {
        var sql = "SELECT"
            + " oc.OperationClaimId,"
            + " oc.OperationClaimName"
            + " FROM OperationClaim oc";
        return _db.Query<OperationClaimDto>(sql).ToList();
    }

    public OperationClaimDto GetByOperationClaimName(string operationClaimName)
    {
        var sql = "SELECT"
           + " oc.OperationClaimId,"
           + " oc.OperationClaimName"
           + " FROM OperationClaim oc"
           + " WHERE oc.OperationClaimName = @OperationClaimName";
        return _db.Query<OperationClaimDto>(sql, new { @OperationClaimName = operationClaimName }).SingleOrDefault();
    }
}

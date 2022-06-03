using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsOperationClaimDal : IOperationClaimDal
{
    private readonly DapperContext _context;

    public DpMsOperationClaimDal(DapperContext context)
    {
        _context = context;
    }

    public IEnumerable<OperationClaim> GetAll()
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " oc.OperationClaimId,"
            + " oc.OperationClaimName"
            + " FROM OperationClaim oc";
        return connection.Query<OperationClaim>(sql).ToList();
    }

    public OperationClaim GetByOperationClaimName(string operationClaimName)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
           + " oc.OperationClaimId,"
           + " oc.OperationClaimName"
           + " FROM OperationClaim oc"
           + " WHERE oc.OperationClaimName = @OperationClaimName";
        return connection.Query<OperationClaim>(sql, new { @OperationClaimName = operationClaimName }).SingleOrDefault();
    }
}

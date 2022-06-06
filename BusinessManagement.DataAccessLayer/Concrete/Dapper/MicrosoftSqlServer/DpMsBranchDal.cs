using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.ExtendedDatabaseModels;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsBranchDal : IBranchDal
{
    private readonly DapperContext _context;

    public DpMsBranchDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(Branch branch)
    {
        using var connection = _context.CreateConnection();
        var sql = "INSERT INTO Branch ("
            + " BusinessId,"
            + " FullAddressId,"
            + " BranchOrder,"
            + " BranchName,"
            + " BranchCode,"
            + " CreatedAt,"
            + " UpdatedAt)"
            + " VALUES("
            + " @BusinessId,"
            + " @FullAddressId,"
            + " @BranchOrder,"
            + " @BranchName,"
            + " @BranchCode,"
            + " @CreatedAt,"
            + " @UpdatedAt)"
            + " SELECT CAST(SCOPE_IDENTITY() AS BIGINT);";
        return connection.Query<long>(sql, branch).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM Branch"
            + " WHERE BranchId = @BranchId";
        connection.Execute(sql, new { @BranchId = id });
    }

    public Branch GetByBranchCode(string branchCode)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " BranchId,"
            + " BusinessId,"
            + " FullAddressId,"
            + " BranchOrder,"
            + " BranchName,"
            + " BranchCode,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Branch"
            + " WHERE BranchCode = @BranchCode";
        return connection.Query<Branch>(sql, new { @BranchCode = branchCode }).SingleOrDefault();
    }

    public List<Branch> GetByBusinessId(int businessId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " BranchId,"
            + " BusinessId,"
            + " FullAddressId,"
            + " BranchOrder,"
            + " BranchName,"
            + " BranchCode,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Branch"
            + " WHERE BusinessId = @BusinessId";
        return connection.Query<Branch>(sql, new { @BusinessId = businessId }).ToList();
    }
    public Branch GetByBusinessIdAndBranchName(int businessId, string branchName)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " BranchId,"
            + " BusinessId,"
            + " FullAddressId,"
            + " BranchOrder,"
            + " BranchName,"
            + " BranchCode,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Branch"
            + " WHERE BusinessId = @BusinessId AND BranchName = @BranchName";
        return connection.Query<Branch>(sql, new
        {
            @BusinessId = businessId,
            @BranchName = branchName,
        }).SingleOrDefault();
    }

    public Branch GetByBusinessIdAndBranchOrderOrBranchCode(int businessId, int branchOrder, string branchCode)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " BranchId,"
            + " BusinessId,"
            + " FullAddressId,"
            + " BranchOrder,"
            + " BranchName,"
            + " BranchCode,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Branch"
            + " WHERE BusinessId = @BusinessId AND BranchOrder = @BranchOrder OR BusinessId = @BusinessId AND BranchCode = @BranchCode";
        return connection.Query<Branch>(sql, new
        {
            @BusinessId = businessId,
            @BranchOrder = branchOrder,
            @BranchCode = branchCode,
        }).SingleOrDefault();
    }

    public Branch GetById(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " BranchId,"
            + " BusinessId,"
            + " FullAddressId,"
            + " BranchOrder,"
            + " BranchName,"
            + " BranchCode,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Branch"
            + " WHERE BranchId = @BranchId";
        return connection.Query<Branch>(sql, new { @BranchId = id }).SingleOrDefault();
    }

    public BranchExt GetExtById(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " b.BranchId,"
            + " b.BusinessId,"
            + " b.FullAddressId,"
            + " b.BranchOrder,"
            + " b.BranchName,"
            + " b.BranchCode,"
            + " b.CreatedAt,"
            + " b.UpdatedAt,"
            + " fa.CityId,"
            + " fa.DistrictId,"
            + " fa.AddressTitle,"
            + " fa.PostalCode,"
            + " fa.AddressText"
            + " FROM Branch b"
            + " INNER JOIN FullAddress fa ON b.FullAddressId = fa.FullAddressId"
            + " WHERE b.BranchId = @BranchId;";
        return connection.Query<BranchExt>(sql, new { @BranchId = id }).SingleOrDefault();
    }

    public List<BranchExt> GetExtsByBusinessId(int businessId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " b.BranchId,"
            + " b.BusinessId,"
            + " b.FullAddressId,"
            + " b.BranchOrder,"
            + " b.BranchName,"
            + " b.BranchCode,"
            + " b.CreatedAt,"
            + " b.UpdatedAt,"
            + " fa.CityId,"
            + " fa.DistrictId,"
            + " fa.AddressTitle,"
            + " fa.PostalCode,"
            + " fa.AddressText"
            + " FROM Branch b"
            + " INNER JOIN FullAddress fa ON b.FullAddressId = fa.FullAddressId"
            + " WHERE b.BusinessId = @BusinessId;";
        return connection.Query<BranchExt>(sql, new { @BusinessId = businessId }).ToList();
    }

    public int GetMaxBranchOrderByBusinessId(int businessId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " MAX(BranchOrder)"
            + " FROM Branch"
            + " WHERE BusinessId = @BusinessId";
        return connection.Query<int>(sql, new
        {
            @BusinessId = businessId,
        }).SingleOrDefault();
    }

    public void Update(Branch branch)
    {
        using var connection = _context.CreateConnection();
        var sql = "UPDATE Branch SET"
            + " BusinessId = @BusinessId,"
            + " FullAddressId = @FullAddressId,"
            + " BranchOrder = @BranchOrder,"
            + " BranchName = @BranchName,"
            + " BranchCode = @BranchCode,"
            + " CreatedAt = @CreatedAt,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE BranchId = @BranchId";
        connection.Execute(sql, branch);
    }
}

using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsBranchDal : IBranchDal
{
    private readonly DapperContext _context;

    public DpMsBranchDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(BranchDto branchDto)
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
        return connection.Query<long>(sql, branchDto).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM Branch"
            + " WHERE BranchId = @BranchId";
        connection.Execute(sql, new { @BranchId = id });
    }

    public BranchDto GetByBranchCode(string branchCode)
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
        return connection.Query<BranchDto>(sql, new { @BranchCode = branchCode }).SingleOrDefault();
    }

    public List<BranchDto> GetByBusinessId(int businessId)
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
        return connection.Query<BranchDto>(sql, new { @BusinessId = businessId }).ToList();
    }
    public BranchDto GetByBusinessIdAndBranchName(int businessId, string branchName)
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
        return connection.Query<BranchDto>(sql, new
        {
            @BusinessId = businessId,
            @BranchName = branchName,
        }).SingleOrDefault();
    }

    public BranchDto GetByBusinessIdAndBranchOrderOrBranchCode(int businessId, int branchOrder, string branchCode)
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
        return connection.Query<BranchDto>(sql, new
        {
            @BusinessId = businessId,
            @BranchOrder = branchOrder,
            @BranchCode = branchCode,
        }).SingleOrDefault();
    }

    public BranchDto GetByBusinessIdAndMaxBranchOrder(int businessId)
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
            + " WHERE BusinessId = @BusinessId AND BranchOrder = (SELECT MAX(BranchOrder) FROM Branch WHERE BusinessId = @BusinessId)";
        return connection.Query<BranchDto>(sql, new
        {
            @BusinessId = businessId,
        }).SingleOrDefault();
    }

    public BranchDto GetById(long id)
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
        return connection.Query<BranchDto>(sql, new { @BranchId = id }).SingleOrDefault();
    }

    public BranchExtDto GetExtById(long id)
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
        return connection.Query<BranchExtDto>(sql, new { @BranchId = id }).SingleOrDefault();
    }

    public List<BranchExtDto> GetExtsByBusinessId(int businessId)
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
        return connection.Query<BranchExtDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public void Update(BranchDto branchDto)
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
        connection.Execute(sql, branchDto);
    }
}

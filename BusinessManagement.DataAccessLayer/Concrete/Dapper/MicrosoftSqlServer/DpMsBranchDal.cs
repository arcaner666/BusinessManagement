using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsBranchDal : IBranchDal
{
    private readonly IDbConnection _db;

    public DpMsBranchDal(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public Branch Add(Branch branch)
    {
        var sql = "INSERT INTO Branch (BusinessId, FullAddressId, BranchOrder, BranchName, BranchCode, CreatedAt, UpdatedAt)"
            + " VALUES(@BusinessId, @FullAddressId, @BranchOrder, @BranchName, @BranchCode, @CreatedAt, @UpdatedAt) SELECT CAST(SCOPE_IDENTITY() AS BIGINT);";
        var id = _db.Query<long>(sql, branch).Single();
        branch.BranchId = id;
        return branch;
    }

    public void Delete(long id)
    {
        var sql = "DELETE FROM Branch"
            + " WHERE BranchId = @BranchId";
        _db.Execute(sql, new { @BranchId = id });
    }

    public Branch GetByBranchCode(string branchCode)
    {
        var sql = "SELECT * FROM Branch"
            + " WHERE BranchCode = @BranchCode";
        return _db.Query<Branch>(sql, new { @BranchCode = branchCode }).SingleOrDefault();
    }

    public List<Branch> GetByBusinessId(int businessId)
    {
        var sql = "SELECT * FROM Branch"
            + " WHERE BusinessId = @BusinessId";
        return _db.Query<Branch>(sql, new { @BusinessId = businessId }).ToList();
    }
    public Branch GetByBusinessIdAndBranchName(int businessId, string branchName)
    {
        var sql = "SELECT * FROM Branch"
            + " WHERE BusinessId = @BusinessId AND BranchName = @BranchName";
        return _db.Query<Branch>(sql, new
        {
            @BusinessId = businessId,
            @BranchName = branchName,
        }).SingleOrDefault();
    }

    public Branch GetByBusinessIdAndBranchOrderOrBranchCode(int businessId, int branchOrder, string branchCode)
    {
        var sql = "SELECT * FROM Branch"
            + " WHERE BusinessId = @BusinessId AND BranchOrder = @BranchOrder OR BusinessId = @BusinessId AND BranchCode = @BranchCode";
        return _db.Query<Branch>(sql, new
        {
            @BusinessId = businessId,
            @BranchOrder = branchOrder,
            @BranchCode = branchCode,
        }).SingleOrDefault();
    }

    public Branch GetByBusinessIdAndMaxBranchOrder(int businessId)
    {
        var sql = "SELECT * FROM Branch"
            + " WHERE BusinessId = @BusinessId AND BranchOrder = (SELECT MAX(BranchOrder) FROM Branch WHERE BusinessId = @BusinessId)";
        return _db.Query<Branch>(sql, new
        {
            @BusinessId = businessId,
        }).SingleOrDefault();
    }

    public Branch GetById(long id)
    {
        var sql = "SELECT * FROM Branch"
            + " WHERE BranchId = @BranchId";
        return _db.Query<Branch>(sql, new { @BranchId = id }).SingleOrDefault();
    }

    public Branch GetExtById(long id)
    {
        var sql = "SELECT * FROM Branch b"
            + " INNER JOIN FullAddress fa ON b.FullAddressId = fa.FullAddressId"
            + " WHERE b.BranchId = @BranchId;";
        return _db.Query<Branch, FullAddress, Branch>(sql,
            (branch, fullAddress) =>
            {
                branch.FullAddress = fullAddress;
                return branch;
            }, new { @BranchId = id },
            splitOn: "FullAddressId").SingleOrDefault();
    }

    public List<Branch> GetExtsByBusinessId(int businessId)
    {
        var sql = "SELECT * FROM Branch b"
            + " INNER JOIN FullAddress fa ON b.FullAddressId = fa.FullAddressId"
            + " INNER JOIN City c ON fa.CityId = c.CityId"
            + " INNER JOIN District d ON fa.DistrictId = d.DistrictId"
            + " WHERE b.BusinessId = @BusinessId;";
        return _db.Query<Branch, FullAddress, City, District, Branch>(sql,
            (branch, fullAddress, city, district) =>
            {
                branch.FullAddress = fullAddress;
                branch.FullAddress.City = city;
                branch.FullAddress.District = district;
                return branch;
            }, new { @BusinessId = businessId },
            splitOn: "FullAddressId,CityId,DistrictId").ToList();
    }

    public void Update(Branch branch)
    {
        var sql = "UPDATE Branch SET BusinessId = @BusinessId, FullAddressId = @FullAddressId, BranchOrder = @BranchOrder, BranchName = @BranchName, BranchCode = @BranchCode, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt"
            + " WHERE BranchId = @BranchId";
        _db.Execute(sql, branch);
    }
}

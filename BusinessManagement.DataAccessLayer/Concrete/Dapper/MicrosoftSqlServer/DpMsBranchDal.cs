using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
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

    public long Add(BranchDto branchDto)
    {
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
        return _db.Query<long>(sql, branchDto).Single();
    }

    public void Delete(long id)
    {
        var sql = "DELETE FROM Branch"
            + " WHERE BranchId = @BranchId";
        _db.Execute(sql, new { @BranchId = id });
    }

    public BranchDto GetByBranchCode(string branchCode)
    {
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
        return _db.Query<BranchDto>(sql, new { @BranchCode = branchCode }).SingleOrDefault();
    }

    public List<BranchDto> GetByBusinessId(int businessId)
    {
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
        return _db.Query<BranchDto>(sql, new { @BusinessId = businessId }).ToList();
    }
    public BranchDto GetByBusinessIdAndBranchName(int businessId, string branchName)
    {
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
        return _db.Query<BranchDto>(sql, new
        {
            @BusinessId = businessId,
            @BranchName = branchName,
        }).SingleOrDefault();
    }

    public BranchDto GetByBusinessIdAndBranchOrderOrBranchCode(int businessId, int branchOrder, string branchCode)
    {
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
        return _db.Query<BranchDto>(sql, new
        {
            @BusinessId = businessId,
            @BranchOrder = branchOrder,
            @BranchCode = branchCode,
        }).SingleOrDefault();
    }

    public BranchDto GetByBusinessIdAndMaxBranchOrder(int businessId)
    {
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
        return _db.Query<BranchDto>(sql, new
        {
            @BusinessId = businessId,
        }).SingleOrDefault();
    }

    public BranchDto GetById(long id)
    {
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
        return _db.Query<BranchDto>(sql, new { @BranchId = id }).SingleOrDefault();
    }

    public BranchExtDto GetExtById(long id)
    {
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
        return _db.Query<BranchExtDto>(sql, new { @BranchId = id }).SingleOrDefault();
    }

    public List<BranchExtDto> GetExtsByBusinessId(int businessId)
    {
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
        return _db.Query<BranchExtDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public void Update(BranchDto branchDto)
    {
        var sql = "UPDATE Branch SET"
            + " BusinessId = @BusinessId,"
            + " FullAddressId = @FullAddressId,"
            + " BranchOrder = @BranchOrder,"
            + " BranchName = @BranchName,"
            + " BranchCode = @BranchCode,"
            + " CreatedAt = @CreatedAt,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE BranchId = @BranchId";
        _db.Execute(sql, branchDto);
    }
}

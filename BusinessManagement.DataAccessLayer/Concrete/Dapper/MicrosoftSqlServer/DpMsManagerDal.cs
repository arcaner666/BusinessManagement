using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsManagerDal : IManagerDal
{
    private readonly IDbConnection _db;

    public DpMsManagerDal(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public Manager Add(Manager manager)
    {
        var sql = "INSERT INTO Manager (BusinessId, BranchId, NameSurname, Email, Phone, DateOfBirth, Gender, Notes, AvatarUrl, TaxOffice, TaxNumber, IdentityNumber, CreatedAt, UpdatedAt)"
            + " VALUES(@BusinessId, @BranchId, @NameSurname, @Email, @Phone, @DateOfBirth, @Gender, @Notes, @AvatarUrl, @TaxOffice, @TaxNumber, @IdentityNumber, @CreatedAt, @UpdatedAt) SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
        var id = _db.Query<long>(sql, manager).Single();
        manager.ManagerId = id;
        return manager;
    }

    public void Delete(long id)
    {
        var sql = "DELETE FROM Manager"
            + " WHERE ManagerId = @ManagerId";
        _db.Execute(sql, new { @ManagerId = id });
    }

    public List<Manager> GetByBusinessId(int businessId)
    {
        var sql = "SELECT * FROM Manager"
            + " WHERE BusinessId = @BusinessId";
        return _db.Query<Manager>(sql, new { @BusinessId = businessId }).ToList();
    }

    public Manager GetByBusinessIdAndPhone(int businessId, string phone)
    {
        var sql = "SELECT * FROM Manager"
            + " WHERE BusinessId = @BusinessId AND Phone = @Phone";
        return _db.Query<Manager>(sql, new
        {
            @BusinessId = businessId,
            @Phone = phone,
        }).SingleOrDefault();
    }

    public Manager GetById(long id)
    {
        var sql = "SELECT * FROM Manager"
            + " WHERE ManagerId = @ManagerId";
        return _db.Query<Manager>(sql, new { @ManagerId = id }).SingleOrDefault();
    }

    public List<Manager> GetExtsByBusinessId(int businessId)
    {
        var sql = "SELECT * FROM Manager m"
            + " INNER JOIN Business bu ON m.BusinessId = bu.BusinessId"
            + " INNER JOIN Branch br ON m.BranchId = br.BranchId"
            + " INNER JOIN FullAddress fa ON br.FullAddressId = fa.FullAddressId"
            + " WHERE m.BusinessId = @BusinessId";
        return _db.Query<Manager, Business, Branch, FullAddress, Manager>(sql,
            (manager, business, branch, fullAddress) =>
            {
                manager.Business = business;
                manager.Branch = branch;
                manager.Branch.FullAddress = fullAddress;
                return manager;
            }, new { @BusinessId = businessId },
            splitOn: "BusinessId,BranchId,FullAddressId").ToList();
    }

    public void Update(Manager manager)
    {
        var sql = "UPDATE Manager SET BusinessId = @BusinessId, BranchId = @BranchId, NameSurname = @NameSurname, Email = @Email, Phone = @Phone, DateOfBirth = @DateOfBirth, Gender = @Gender, Notes = @Notes, AvatarUrl = @AvatarUrl, TaxOffice = @TaxOffice, TaxNumber = @TaxNumber, IdentityNumber = @IdentityNumber, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt"
            + " WHERE ManagerId = @ManagerId";
        _db.Execute(sql, manager);
    }
}

using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
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

    public long Add(ManagerDto managerDto)
    {
        var sql = "INSERT INTO Manager ("
            + " BusinessId,"
            + " BranchId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " DateOfBirth,"
            + " Gender,"
            + " Notes,"
            + " AvatarUrl,"
            + " TaxOffice,"
            + " TaxNumber,"
            + " IdentityNumber,"
            + " CreatedAt,"
            + " UpdatedAt)"
            + " VALUES("
            + " @BusinessId,"
            + " @BranchId,"
            + " @NameSurname,"
            + " @Email,"
            + " @Phone,"
            + " @DateOfBirth,"
            + " @Gender,"
            + " @Notes,"
            + " @AvatarUrl,"
            + " @TaxOffice,"
            + " @TaxNumber,"
            + " @IdentityNumber,"
            + " @CreatedAt,"
            + " @UpdatedAt)"
            + " SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
        return _db.Query<long>(sql, managerDto).Single();
    }

    public void Delete(long id)
    {
        var sql = "DELETE FROM Manager"
            + " WHERE ManagerId = @ManagerId";
        _db.Execute(sql, new { @ManagerId = id });
    }

    public List<ManagerDto> GetByBusinessId(int businessId)
    {
        var sql = "SELECT"
            + " ManagerId,"
            + " BusinessId,"
            + " BranchId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " DateOfBirth,"
            + " Gender,"
            + " Notes,"
            + " AvatarUrl,"
            + " TaxOffice,"
            + " TaxNumber,"
            + " IdentityNumber,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Manager"
            + " WHERE BusinessId = @BusinessId";
        return _db.Query<ManagerDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public ManagerDto GetByBusinessIdAndPhone(int businessId, string phone)
    {
        var sql = "SELECT"
            + " ManagerId,"
            + " BusinessId,"
            + " BranchId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " DateOfBirth,"
            + " Gender,"
            + " Notes,"
            + " AvatarUrl,"
            + " TaxOffice,"
            + " TaxNumber,"
            + " IdentityNumber,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Manager"
            + " WHERE BusinessId = @BusinessId AND Phone = @Phone";
        return _db.Query<ManagerDto>(sql, new
        {
            @BusinessId = businessId,
            @Phone = phone,
        }).SingleOrDefault();
    }

    public ManagerDto GetById(long id)
    {
        var sql = "SELECT"
            + " ManagerId,"
            + " BusinessId,"
            + " BranchId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " DateOfBirth,"
            + " Gender,"
            + " Notes,"
            + " AvatarUrl,"
            + " TaxOffice,"
            + " TaxNumber,"
            + " IdentityNumber,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Manager"
            + " WHERE ManagerId = @ManagerId";
        return _db.Query<ManagerDto>(sql, new { @ManagerId = id }).SingleOrDefault();
    }

    public List<ManagerExtDto> GetExtsByBusinessId(int businessId)
    {
        var sql = "SELECT"
            + " m.ManagerId,"
            + " m.BusinessId,"
            + " m.BranchId,"
            + " m.NameSurname,"
            + " m.Email,"
            + " m.Phone,"
            + " m.DateOfBirth,"
            + " m.Gender,"
            + " m.Notes,"
            + " m.AvatarUrl,"
            + " m.TaxOffice,"
            + " m.TaxNumber,"
            + " m.IdentityNumber,"
            + " m.CreatedAt,"
            + " m.UpdatedAt,"
            + " bu.BusinessName,"
            + " fa.CityId,"
            + " fa.DistrictId,"
            + " fa.AddressText"
            + " FROM Manager m"
            + " INNER JOIN Business bu ON m.BusinessId = bu.BusinessId"
            + " INNER JOIN Branch br ON m.BranchId = br.BranchId"
            + " INNER JOIN FullAddress fa ON br.FullAddressId = fa.FullAddressId"
            + " WHERE m.BusinessId = @BusinessId";
        return _db.Query<ManagerExtDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public void Update(ManagerDto managerDto)
    {
        var sql = "UPDATE Manager SET"
            + " BusinessId = @BusinessId,"
            + " BranchId = @BranchId,"
            + " NameSurname = @NameSurname,"
            + " Email = @Email,"
            + " Phone = @Phone,"
            + " DateOfBirth = @DateOfBirth,"
            + " Gender = @Gender,"
            + " Notes = @Notes,"
            + " AvatarUrl = @AvatarUrl,"
            + " TaxOffice = @TaxOffice,"
            + " TaxNumber = @TaxNumber,"
            + " IdentityNumber = @IdentityNumber,"
            + " CreatedAt = @CreatedAt,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE ManagerId = @ManagerId";
        _db.Execute(sql, managerDto);
    }
}

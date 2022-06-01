using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsFlatDal : IFlatDal
{
    private readonly IDbConnection _db;

    public DpMsFlatDal(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public long Add(FlatDto flatDto)
    {
        var sql = "INSERT INTO Flat ("
            + " SectionId,"
            + " ApartmentId,"
            + " BusinessId,"
            + " BranchId,"
            + " HouseOwnerId,"
            + " TenantId,"
            + " FlatCode,"
            + " DoorNumber,"
            + " CreatedAt,"
            + " UpdatedAt)"
            + " VALUES("
            + " @SectionId,"
            + " @ApartmentId,"
            + " @BusinessId,"
            + " @BranchId,"
            + " @HouseOwnerId,"
            + " @TenantId,"
            + " @FlatCode,"
            + " @DoorNumber,"
            + " @CreatedAt,"
            + " @UpdatedAt)"
            + " SELECT CAST(SCOPE_IDENTITY() AS BIGINT);";
        return _db.Query<long>(sql, flatDto).Single();
    }

    public void Delete(long id)
    {
        var sql = "DELETE FROM Flat"
            + " WHERE FlatId = @FlatId";
        _db.Execute(sql, new { @FlatId = id });
    }

    public List<FlatDto> GetByApartmentId(long apartmentId)
    {
        var sql = "SELECT"
            + " FlatId,"
            + " SectionId,"
            + " ApartmentId,"
            + " BusinessId,"
            + " BranchId,"
            + " HouseOwnerId,"
            + " TenantId,"
            + " FlatCode,"
            + " DoorNumber,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Flat"
            + " WHERE ApartmentId = @ApartmentId";
        return _db.Query<FlatDto>(sql, new { @ApartmentId = apartmentId }).ToList();
    }

    public FlatDto GetByFlatCode(string flatCode)
    {
        var sql = "SELECT"
            + " FlatId,"
            + " SectionId,"
            + " ApartmentId,"
            + " BusinessId,"
            + " BranchId,"
            + " HouseOwnerId,"
            + " TenantId,"
            + " FlatCode,"
            + " DoorNumber,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Flat"
            + " WHERE FlatCode = @FlatCode";
        return _db.Query<FlatDto>(sql, new { @FlatCode = flatCode }).SingleOrDefault();
    }

    public FlatDto GetById(long id)
    {
        var sql = "SELECT"
            + " FlatId,"
            + " SectionId,"
            + " ApartmentId,"
            + " BusinessId,"
            + " BranchId,"
            + " HouseOwnerId,"
            + " TenantId,"
            + " FlatCode,"
            + " DoorNumber,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Flat"
            + " WHERE FlatId = @FlatId";
        return _db.Query<FlatDto>(sql, new { @FlatId = id }).SingleOrDefault();
    }

    public FlatExtDto GetExtById(long id)
    {
        var sql = "SELECT"
            + " f.FlatId,"
            + " f.SectionId,"
            + " f.ApartmentId,"
            + " f.BusinessId,"
            + " f.BranchId,"
            + " f.HouseOwnerId,"
            + " f.TenantId,"
            + " f.FlatCode,"
            + " f.DoorNumber,"
            + " f.CreatedAt,"
            + " f.UpdatedAt,"
            + " s.SectionName,"
            + " a.ApartmentName,"
            + " ho.NameSurname AS HouseOwnerNameSurname,"
            + " t.NameSurname AS TenantNameSurname"
            + " FROM Flat f"
            + " INNER JOIN Section s ON f.SectionId = s.SectionId"
            + " INNER JOIN Apartment a ON f.ApartmentId = a.ApartmentId"
            + " LEFT JOIN HouseOwner ho ON f.HouseOwnerId = ho.HouseOwnerId"
            + " LEFT JOIN Tenant t ON f.TenantId = t.TenantId"
            + " WHERE f.FlatId = @FlatId;";
        return _db.Query<FlatExtDto>(sql, new { @FlatId = id }).SingleOrDefault();
    }

    public List<FlatExtDto> GetExtsByBusinessId(int businessId)
    {
        var sql = "SELECT"
            + " f.FlatId,"
            + " f.SectionId,"
            + " f.ApartmentId,"
            + " f.BusinessId,"
            + " f.BranchId,"
            + " f.HouseOwnerId,"
            + " f.TenantId,"
            + " f.FlatCode,"
            + " f.DoorNumber,"
            + " f.CreatedAt,"
            + " f.UpdatedAt,"
            + " s.SectionName,"
            + " a.ApartmentName,"
            + " ho.NameSurname AS HouseOwnerNameSurname,"
            + " t.NameSurname AS TenantNameSurname"
            + " FROM Flat f"
            + " INNER JOIN Section s ON f.SectionId = s.SectionId"
            + " INNER JOIN Apartment a ON f.ApartmentId = a.ApartmentId"
            + " LEFT JOIN HouseOwner ho ON f.HouseOwnerId = ho.HouseOwnerId"
            + " LEFT JOIN Tenant t ON f.TenantId = t.TenantId"
            + " WHERE f.BusinessId = @BusinessId;";
        return _db.Query<FlatExtDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public void Update(FlatDto flatDto)
    {
        var sql = "UPDATE Flat SET"
            + " SectionId = @SectionId,"
            + " ApartmentId = @ApartmentId,"
            + " BusinessId = @BusinessId,"
            + " BranchId = @BranchId,"
            + " HouseOwnerId = @HouseOwnerId,"
            + " TenantId = @TenantId,"
            + " FlatCode = @FlatCode,"
            + " DoorNumber = @DoorNumber,"
            + " CreatedAt = @CreatedAt,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE FlatId = @FlatId";
        _db.Execute(sql, flatDto);
    }
}

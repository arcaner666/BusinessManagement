using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsApartmentDal : IApartmentDal
{
    private readonly IDbConnection _db;

    public DpMsApartmentDal(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public long Add(ApartmentDto apartmentDto)
    {
        var sql = "INSERT INTO Apartment ("
            + " SectionId,"
            + " BusinessId,"
            + " BranchId,"
            + " ManagerId,"
            + " ApartmentName,"
            + " ApartmentCode,"
            + " BlockNumber,"
            + " CreatedAt,"
            + " UpdatedAt)"
            + " VALUES("
            + " @SectionId,"
            + " @BusinessId,"
            + " @BranchId,"
            + " @ManagerId,"
            + " @ApartmentName,"
            + " @ApartmentCode,"
            + " @BlockNumber,"
            + " @CreatedAt,"
            + " @UpdatedAt)"
            + " SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
        return _db.Query<long>(sql, apartmentDto).Single();
    }

    public void Delete(long id)
    {
        var sql = "DELETE FROM Apartment"
            + " WHERE ApartmentId = @ApartmentId";
        _db.Execute(sql, new { @ApartmentId = id });
    }

    public ApartmentDto GetByApartmentCode(string apartmentCode)
    {
        var sql = "SELECT"
            + " ApartmentId,"
            + " SectionId,"
            + " BusinessId,"
            + " BranchId,"
            + " ManagerId,"
            + " ApartmentName,"
            + " ApartmentCode,"
            + " BlockNumber,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Apartment"
            + " WHERE ApartmentCode = @ApartmentCode";
        return _db.Query<ApartmentDto>(sql, new { @ApartmentCode = apartmentCode }).SingleOrDefault();
    }

    public List<ApartmentDto> GetByBusinessId(int businessId)
    {
        var sql = "SELECT"
            + " ApartmentId,"
            + " SectionId,"
            + " BusinessId,"
            + " BranchId,"
            + " ManagerId,"
            + " ApartmentName,"
            + " ApartmentCode,"
            + " BlockNumber,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Apartment"
            + " WHERE BusinessId = @BusinessId";
        return _db.Query<ApartmentDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public ApartmentDto GetById(long id)
    {
        var sql = "SELECT"
            + " ApartmentId,"
            + " SectionId,"
            + " BusinessId,"
            + " BranchId,"
            + " ManagerId,"
            + " ApartmentName,"
            + " ApartmentCode,"
            + " BlockNumber,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Apartment"
            + " WHERE ApartmentId = @ApartmentId";
        return _db.Query<ApartmentDto>(sql, new { @ApartmentId = id }).SingleOrDefault();
    }

    public List<ApartmentDto> GetBySectionId(int sectionId)
    {
        var sql = "SELECT"
            + " ApartmentId,"
            + " SectionId,"
            + " BusinessId,"
            + " BranchId,"
            + " ManagerId,"
            + " ApartmentName,"
            + " ApartmentCode,"
            + " BlockNumber,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Apartment"
            + " WHERE SectionId = @SectionId";
        return _db.Query<ApartmentDto>(sql, new { @SectionId = sectionId }).ToList();
    }

    public ApartmentExtDto GetExtById(long id)
    {
        var sql = "SELECT"
            + " a.ApartmentId,"
            + " a.SectionId,"
            + " a.BusinessId,"
            + " a.BranchId,"
            + " a.ManagerId,"
            + " a.ApartmentName,"
            + " a.ApartmentCode,"
            + " a.BlockNumber,"
            + " a.CreatedAt,"
            + " a.UpdatedAt,"
            + " s.SectionName,"
            + " m.NameSurname AS ManagerNameSurname"
            + " FROM Apartment a"
            + " INNER JOIN Section s ON a.SectionId = s.SectionId"
            + " INNER JOIN Manager m ON a.ManagerId = m.ManagerId"
            + " WHERE a.ApartmentId = @ApartmentId";
        return _db.Query<ApartmentExtDto>(sql, new { @ApartmentId = id }).SingleOrDefault();
    }

    public List<ApartmentExtDto> GetExtsByBusinessId(int businessId)
    {
        var sql = "SELECT"
            + " a.ApartmentId,"
            + " a.SectionId,"
            + " a.BusinessId,"
            + " a.BranchId,"
            + " a.ManagerId,"
            + " a.ApartmentName,"
            + " a.ApartmentCode,"
            + " a.BlockNumber,"
            + " a.CreatedAt,"
            + " a.UpdatedAt,"
            + " s.SectionName,"
            + " m.NameSurname AS ManagerNameSurname"
            + " FROM Apartment a"
            + " INNER JOIN Section s ON a.SectionId = s.SectionId"
            + " INNER JOIN Manager m ON a.ManagerId = m.ManagerId"
            + " WHERE a.BusinessId = @BusinessId";
        return _db.Query<ApartmentExtDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public void Update(ApartmentDto apartmentDto)
    {
        var sql = "UPDATE Apartment SET"
            + " SectionId = @SectionId,"
            + " BusinessId = @BusinessId,"
            + " BranchId = @BranchId,"
            + " ManagerId = @ManagerId,"
            + " ApartmentName = @ApartmentName,"
            + " ApartmentCode = @ApartmentCode,"
            + " BlockNumber = @BlockNumber,"
            + " CreatedAt = @CreatedAt,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE ApartmentId = @ApartmentId";
        _db.Execute(sql, apartmentDto);
    }
}

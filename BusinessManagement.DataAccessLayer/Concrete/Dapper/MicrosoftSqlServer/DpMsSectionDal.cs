using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsSectionDal : ISectionDal
{
    private readonly IDbConnection _db;

    public DpMsSectionDal(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public int Add(SectionDto sectionDto)
    {
        var sql = "INSERT INTO Section ("
            + " SectionGroupId,"
            + " BusinessId,"
            + " BranchId,"
            + " ManagerId,"
            + " FullAddressId,"
            + " SectionName,"
            + " SectionCode,"
            + " CreatedAt,"
            + " UpdatedAt)"
            + " VALUES("
            + " @SectionGroupId,"
            + " @BusinessId,"
            + " @BranchId,"
            + " @ManagerId,"
            + " @FullAddressId,"
            + " @SectionName,"
            + " @SectionCode,"
            + " @CreatedAt,"
            + " @UpdatedAt)"
            + " SELECT CAST(SCOPE_IDENTITY() AS INT);";
        return _db.Query<int>(sql, sectionDto).Single();
    }

    public void Delete(int id)
    {
        var sql = "DELETE FROM Section"
            + " WHERE SectionId = @SectionId";
        _db.Execute(sql, new { @SectionId = id });
    }

    public List<SectionDto> GetAll()
    {
        var sql = "SELECT"
            + " SectionId,"
            + " SectionGroupId,"
            + " BusinessId,"
            + " BranchId,"
            + " ManagerId,"
            + " FullAddressId,"
            + " SectionName,"
            + " SectionCode,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Section";
        return _db.Query<SectionDto>(sql).ToList();
    }

    public List<SectionDto> GetByBusinessId(int businessId)
    {
        var sql = "SELECT"
            + " SectionId,"
            + " SectionGroupId,"
            + " BusinessId,"
            + " BranchId,"
            + " ManagerId,"
            + " FullAddressId,"
            + " SectionName,"
            + " SectionCode,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Section"
            + " WHERE BusinessId = @BusinessId";
        return _db.Query<SectionDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public SectionDto GetById(int id)
    {
        var sql = "SELECT"
            + " SectionId,"
            + " SectionGroupId,"
            + " BusinessId,"
            + " BranchId,"
            + " ManagerId,"
            + " FullAddressId,"
            + " SectionName,"
            + " SectionCode,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Section"
            + " WHERE SectionId = @SectionId";
        return _db.Query<SectionDto>(sql, new { @SectionId = id }).SingleOrDefault();
    }

    public SectionDto GetBySectionCode(string sectionCode)
    {
        var sql = "SELECT"
            + " SectionId,"
            + " SectionGroupId,"
            + " BusinessId,"
            + " BranchId,"
            + " ManagerId,"
            + " FullAddressId,"
            + " SectionName,"
            + " SectionCode,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Section"
            + " WHERE SectionCode = @SectionCode";
        return _db.Query<SectionDto>(sql, new { @SectionCode = sectionCode }).SingleOrDefault();
    }

    public SectionExtDto GetExtById(int id)
    {
        var sql = "SELECT"
            + " s.SectionId,"
            + " s.SectionGroupId,"
            + " s.BusinessId,"
            + " s.BranchId,"
            + " s.ManagerId,"
            + " s.FullAddressId,"
            + " s.SectionName,"
            + " s.SectionCode,"
            + " s.CreatedAt,"
            + " s.UpdatedAt,"
            + " sg.SectionGroupName,"
            + " m.NameSurname AS ManagerNameSurname,"
            + " fa.CityId,"
            + " fa.DistrictId,"
            + " fa.AddressTitle,"
            + " fa.PostalCode,"
            + " fa.AddressText,"
            + " c.CityName,"
            + " d.DistrictName"
            + " FROM Section s"
            + " INNER JOIN SectionGroup sg ON s.SectionGroupId = sg.SectionGroupId"
            + " INNER JOIN Manager m ON s.ManagerId = m.ManagerId"
            + " INNER JOIN FullAddress fa ON s.FullAddressId = fa.FullAddressId"
            + " INNER JOIN City c ON fa.CityId = c.CityId"
            + " INNER JOIN District d ON fa.DistrictId = d.DistrictId"
            + " WHERE s.SectionId = @SectionId;";
        return _db.Query<SectionExtDto>(sql, new { @SectionId = id }).SingleOrDefault();
    }

    public List<SectionExtDto> GetExtsByBusinessId(int businessId)
    {
        var sql = "SELECT"
            + " s.SectionId,"
            + " s.SectionGroupId,"
            + " s.BusinessId,"
            + " s.BranchId,"
            + " s.ManagerId,"
            + " s.FullAddressId,"
            + " s.SectionName,"
            + " s.SectionCode,"
            + " s.CreatedAt,"
            + " s.UpdatedAt,"
            + " sg.SectionGroupName,"
            + " m.NameSurname AS ManagerNameSurname,"
            + " fa.CityId,"
            + " fa.DistrictId,"
            + " fa.AddressTitle,"
            + " fa.PostalCode,"
            + " fa.AddressText,"
            + " c.CityName,"
            + " d.DistrictName"
            + " FROM Section s"
            + " INNER JOIN SectionGroup sg ON s.SectionGroupId = sg.SectionGroupId"
            + " INNER JOIN Manager m ON s.ManagerId = m.ManagerId"
            + " INNER JOIN FullAddress fa ON s.FullAddressId = fa.FullAddressId"
            + " INNER JOIN City c ON fa.CityId = c.CityId"
            + " INNER JOIN District d ON fa.DistrictId = d.DistrictId"
            + " WHERE s.BusinessId = @BusinessId;";
        return _db.Query<SectionExtDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public void Update(SectionDto sectionDto)
    {
        var sql = "UPDATE Section SET"
            + " SectionGroupId = @SectionGroupId,"
            + " BusinessId = @BusinessId,"
            + " BranchId = @BranchId,"
            + " ManagerId = @ManagerId,"
            + " FullAddressId = @FullAddressId,"
            + " SectionName = @SectionName,"
            + " SectionCode = @SectionCode,"
            + " CreatedAt = @CreatedAt,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE SectionId = @SectionId";
        _db.Execute(sql, sectionDto);
    }
}

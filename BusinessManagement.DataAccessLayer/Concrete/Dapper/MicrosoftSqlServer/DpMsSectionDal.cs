using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
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

    public Section Add(Section section)
    {
        var sql = "INSERT INTO Section (SectionGroupId, BusinessId, BranchId, ManagerId, FullAddressId, SectionName, SectionCode, CreatedAt, UpdatedAt)"
            + " VALUES(@SectionGroupId, @BusinessId, @BranchId, @ManagerId, @FullAddressId, @SectionName, @SectionCode, @CreatedAt, @UpdatedAt) SELECT CAST(SCOPE_IDENTITY() as int);";
        var id = _db.Query<int>(sql, section).Single();
        section.SectionId = id;
        return section;
    }

    public void Delete(int id)
    {
        var sql = "DELETE FROM Section"
            + " WHERE SectionId = @SectionId";
        _db.Execute(sql, new { @SectionId = id });
    }

    public List<Section> GetAll()
    {
        var sql = "SELECT * FROM Section";
        return _db.Query<Section>(sql).ToList();
    }

    public List<Section> GetByBusinessId(int businessId)
    {
        var sql = "SELECT * FROM Section"
            + " WHERE BusinessId = @BusinessId";
        return _db.Query<Section>(sql, new { @BusinessId = businessId }).ToList();
    }

    public Section GetById(int id)
    {
        var sql = "SELECT * FROM Section"
            + " WHERE SectionId = @SectionId";
        return _db.Query<Section>(sql, new { @SectionId = id }).SingleOrDefault();
    }

    public Section GetBySectionCode(string sectionCode)
    {
        var sql = "SELECT * FROM Section"
            + " WHERE SectionCode = @SectionCode";
        return _db.Query<Section>(sql, new { @SectionCode = sectionCode }).SingleOrDefault();
    }

    public Section GetExtById(int id)
    {
        var sql = "SELECT * FROM Section s"
            + " INNER JOIN SectionGroup sg ON s.SectionGroupId = sg.SectionGroupId"
            + " INNER JOIN Manager m ON s.ManagerId = m.ManagerId"
            + " INNER JOIN FullAddress fa ON s.FullAddressId = fa.FullAddressId"
            + " INNER JOIN City c ON fa.CityId = c.CityId"
            + " INNER JOIN District d ON fa.DistrictId = d.DistrictId"
            + " WHERE s.SectionId = @SectionId;";
        return _db.Query<Section, SectionGroup, Manager, FullAddress, City, District, Section>(sql,
            (section, sectionGroup, manager, fullAddress, city, district) =>
            {
                section.SectionGroup = sectionGroup;
                section.Manager = manager;
                section.FullAddress = fullAddress;
                section.FullAddress.City = city;
                section.FullAddress.District = district;
                return section;
            }, new { @SectionId = id },
            splitOn: "SectionGroupId,ManagerId,FullAddressId,CityId,DistrictId").SingleOrDefault();
    }

    public List<Section> GetExtsByBusinessId(int businessId)
    {
        var sql = "SELECT * FROM Section s"
            + " INNER JOIN SectionGroup sg ON s.SectionGroupId = sg.SectionGroupId"
            + " INNER JOIN Manager m ON s.ManagerId = m.ManagerId"
            + " INNER JOIN FullAddress fa ON s.FullAddressId = fa.FullAddressId"
            + " INNER JOIN City c ON fa.CityId = c.CityId"
            + " INNER JOIN District d ON fa.DistrictId = d.DistrictId"
            + " WHERE s.BusinessId = @BusinessId;";
        return _db.Query<Section, SectionGroup, Manager, FullAddress, City, District, Section>(sql, 
            (section, sectionGroup, manager, fullAddress, city, district) => 
            {
                section.SectionGroup = sectionGroup;
                section.Manager = manager;
                section.FullAddress = fullAddress;
                section.FullAddress.City = city;
                section.FullAddress.District = district;
                return section;
            }, new { @BusinessId = businessId },
            splitOn: "SectionGroupId,ManagerId,FullAddressId,CityId,DistrictId").ToList();
    }

    public void Update(Section section)
    {
        var sql = "UPDATE Section SET SectionGroupId = @SectionGroupId, BusinessId = @BusinessId, BranchId = @BranchId, ManagerId = @ManagerId, FullAddressId = @FullAddressId, SectionName = @SectionName, SectionCode = @SectionCode, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt"
            + " WHERE SectionId = @SectionId";
        _db.Execute(sql, section);
    }
}

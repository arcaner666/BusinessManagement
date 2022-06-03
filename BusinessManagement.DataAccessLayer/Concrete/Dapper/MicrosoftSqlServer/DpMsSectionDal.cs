using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.ExtendedDatabaseModels;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsSectionDal : ISectionDal
{
    private readonly DapperContext _context;

    public DpMsSectionDal(DapperContext context)
    {
        _context = context;
    }

    public int Add(Section section)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<int>(sql, section).Single();
    }

    public void Delete(int id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM Section"
            + " WHERE SectionId = @SectionId";
        connection.Execute(sql, new { @SectionId = id });
    }

    public IEnumerable<Section> GetAll()
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<Section>(sql).ToList();
    }

    public IEnumerable<Section> GetByBusinessId(int businessId)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<Section>(sql, new { @BusinessId = businessId }).ToList();
    }

    public Section GetById(int id)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<Section>(sql, new { @SectionId = id }).SingleOrDefault();
    }

    public Section GetBySectionCode(string sectionCode)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<Section>(sql, new { @SectionCode = sectionCode }).SingleOrDefault();
    }

    public SectionExt GetExtById(int id)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<SectionExt>(sql, new { @SectionId = id }).SingleOrDefault();
    }

    public IEnumerable<SectionExt> GetExtsByBusinessId(int businessId)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<SectionExt>(sql, new { @BusinessId = businessId }).ToList();
    }

    public void Update(Section section)
    {
        using var connection = _context.CreateConnection();
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
        connection.Execute(sql, section);
    }
}

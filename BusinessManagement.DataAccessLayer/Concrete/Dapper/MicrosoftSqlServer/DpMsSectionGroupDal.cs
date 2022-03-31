using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsSectionGroupDal : ISectionGroupDal
{
    private readonly IDbConnection _db;

    public DpMsSectionGroupDal(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public SectionGroup Add(SectionGroup sectionGroup)
    {
        var sql = "INSERT INTO SectionGroup (BusinessId, BranchId, SectionGroupName, CreatedAt, UpdatedAt)"
            + " VALUES(@BusinessId, @BranchId, @SectionGroupName, @CreatedAt, @UpdatedAt) SELECT CAST(SCOPE_IDENTITY() as int);";
        var id = _db.Query<int>(sql, sectionGroup).Single();
        sectionGroup.SectionGroupId = id;
        return sectionGroup;
    }

    public void Delete(long id)
    {
        var sql = "DELETE FROM SectionGroup"
            + " WHERE SectionGroupId = @SectionGroupId";
        _db.Execute(sql, new { @SectionGroupId = id });
    }

    public List<SectionGroup> GetByBusinessId(int businessId)
    {
        var sql = "SELECT * FROM SectionGroup"
            + " WHERE BusinessId = @BusinessId";
        return _db.Query<SectionGroup>(sql, new { @BusinessId = businessId }).ToList();
    }

    public SectionGroup GetByBusinessIdAndSectionGroupName(int businessId, string sectionGroupName)
    {
        var sql = "SELECT * FROM SectionGroup"
            + " WHERE BusinessId = @BusinessId AND SectionGroupName = @SectionGroupName";
        return _db.Query<SectionGroup>(sql, new
        {
            @BusinessId = businessId,
            @SectionGroupName = sectionGroupName,
        }).SingleOrDefault();
    }

    public SectionGroup GetById(long id)
    {
        var sql = "SELECT * FROM SectionGroup"
            + " WHERE SectionGroupId = @SectionGroupId";
        return _db.Query<SectionGroup>(sql, new { @SectionGroupId = id }).SingleOrDefault();
    }

    public void Update(SectionGroup sectionGroup)
    {
        var sql = "UPDATE SectionGroup SET BusinessId = @BusinessId, BranchId = @BranchId, SectionGroupName = @SectionGroupName, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt"
            + " WHERE SectionGroupId = @SectionGroupId";
        _db.Execute(sql, sectionGroup);
    }
}

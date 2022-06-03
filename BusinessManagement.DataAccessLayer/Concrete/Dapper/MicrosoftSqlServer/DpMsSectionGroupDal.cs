using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsSectionGroupDal : ISectionGroupDal
{
    private readonly DapperContext _context;

    public DpMsSectionGroupDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(SectionGroup sectionGroup)
    {
        using var connection = _context.CreateConnection();
        var sql = "INSERT INTO SectionGroup ("
            + " BusinessId,"
            + " BranchId,"
            + " SectionGroupName,"
            + " CreatedAt,"
            + " UpdatedAt)"
            + " VALUES("
            + " @BusinessId,"
            + " @BranchId,"
            + " @SectionGroupName,"
            + " @CreatedAt,"
            + " @UpdatedAt)"
            + " SELECT CAST(SCOPE_IDENTITY() AS BIGINT);";
        return connection.Query<long>(sql, sectionGroup).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM SectionGroup"
            + " WHERE SectionGroupId = @SectionGroupId";
        connection.Execute(sql, new { @SectionGroupId = id });
    }

    public IEnumerable<SectionGroup> GetByBusinessId(int businessId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " SectionGroupId,"
            + " BusinessId,"
            + " BranchId,"
            + " SectionGroupName,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM SectionGroup"
            + " WHERE BusinessId = @BusinessId";
        return connection.Query<SectionGroup>(sql, new { @BusinessId = businessId }).ToList();
    }

    public SectionGroup GetByBusinessIdAndSectionGroupName(int businessId, string sectionGroupName)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " SectionGroupId,"
            + " BusinessId,"
            + " BranchId,"
            + " SectionGroupName,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM SectionGroup"
            + " WHERE BusinessId = @BusinessId AND SectionGroupName = @SectionGroupName";
        return connection.Query<SectionGroup>(sql, new
        {
            @BusinessId = businessId,
            @SectionGroupName = sectionGroupName,
        }).SingleOrDefault();
    }

    public SectionGroup GetById(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " SectionGroupId,"
            + " BusinessId,"
            + " BranchId,"
            + " SectionGroupName,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM SectionGroup"
            + " WHERE SectionGroupId = @SectionGroupId";
        return connection.Query<SectionGroup>(sql, new { @SectionGroupId = id }).SingleOrDefault();
    }

    public void Update(SectionGroup sectionGroup)
    {
        using var connection = _context.CreateConnection();
        var sql = "UPDATE SectionGroup SET"
            + " BusinessId = @BusinessId,"
            + " BranchId = @BranchId,"
            + " SectionGroupName = @SectionGroupName,"
            + " CreatedAt = @CreatedAt,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE SectionGroupId = @SectionGroupId";
        connection.Execute(sql, sectionGroup);
    }
}

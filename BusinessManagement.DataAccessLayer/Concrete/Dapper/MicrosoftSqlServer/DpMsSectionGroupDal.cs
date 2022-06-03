using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsSectionGroupDal : ISectionGroupDal
{
    private readonly DapperContext _context;

    public DpMsSectionGroupDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(SectionGroupDto sectionGroupDto)
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
        return connection.Query<long>(sql, sectionGroupDto).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM SectionGroup"
            + " WHERE SectionGroupId = @SectionGroupId";
        connection.Execute(sql, new { @SectionGroupId = id });
    }

    public IEnumerable<SectionGroupDto> GetByBusinessId(int businessId)
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
        return connection.Query<SectionGroupDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public SectionGroupDto GetByBusinessIdAndSectionGroupName(int businessId, string sectionGroupName)
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
        return connection.Query<SectionGroupDto>(sql, new
        {
            @BusinessId = businessId,
            @SectionGroupName = sectionGroupName,
        }).SingleOrDefault();
    }

    public SectionGroupDto GetById(long id)
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
        return connection.Query<SectionGroupDto>(sql, new { @SectionGroupId = id }).SingleOrDefault();
    }

    public void Update(SectionGroupDto sectionGroupDto)
    {
        using var connection = _context.CreateConnection();
        var sql = "UPDATE SectionGroup SET"
            + " BusinessId = @BusinessId,"
            + " BranchId = @BranchId,"
            + " SectionGroupName = @SectionGroupName,"
            + " CreatedAt = @CreatedAt,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE SectionGroupId = @SectionGroupId";
        connection.Execute(sql, sectionGroupDto);
    }
}

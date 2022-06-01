using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
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

    public long Add(SectionGroupDto sectionGroupDto)
    {
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
        return _db.Query<long>(sql, sectionGroupDto).Single();
    }

    public void Delete(long id)
    {
        var sql = "DELETE FROM SectionGroup"
            + " WHERE SectionGroupId = @SectionGroupId";
        _db.Execute(sql, new { @SectionGroupId = id });
    }

    public List<SectionGroupDto> GetByBusinessId(int businessId)
    {
        var sql = "SELECT"
            + " SectionGroupId,"
            + " BusinessId,"
            + " BranchId,"
            + " SectionGroupName,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM SectionGroup"
            + " WHERE BusinessId = @BusinessId";
        return _db.Query<SectionGroupDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public SectionGroupDto GetByBusinessIdAndSectionGroupName(int businessId, string sectionGroupName)
    {
        var sql = "SELECT"
            + " SectionGroupId,"
            + " BusinessId,"
            + " BranchId,"
            + " SectionGroupName,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM SectionGroup"
            + " WHERE BusinessId = @BusinessId AND SectionGroupName = @SectionGroupName";
        return _db.Query<SectionGroupDto>(sql, new
        {
            @BusinessId = businessId,
            @SectionGroupName = sectionGroupName,
        }).SingleOrDefault();
    }

    public SectionGroupDto GetById(long id)
    {
        var sql = "SELECT"
            + " SectionGroupId,"
            + " BusinessId,"
            + " BranchId,"
            + " SectionGroupName,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM SectionGroup"
            + " WHERE SectionGroupId = @SectionGroupId";
        return _db.Query<SectionGroupDto>(sql, new { @SectionGroupId = id }).SingleOrDefault();
    }

    public void Update(SectionGroupDto sectionGroupDto)
    {
        var sql = "UPDATE SectionGroup SET"
            + " BusinessId = @BusinessId,"
            + " BranchId = @BranchId,"
            + " SectionGroupName = @SectionGroupName,"
            + " CreatedAt = @CreatedAt,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE SectionGroupId = @SectionGroupId";
        _db.Execute(sql, sectionGroupDto);
    }
}

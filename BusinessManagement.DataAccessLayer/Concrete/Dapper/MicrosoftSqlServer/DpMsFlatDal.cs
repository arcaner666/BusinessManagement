using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.ExtendedDatabaseModels;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsFlatDal : IFlatDal
{
    private readonly DapperContext _context;

    public DpMsFlatDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(Flat flat)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<long>(sql, flat).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM Flat"
            + " WHERE FlatId = @FlatId";
        connection.Execute(sql, new { @FlatId = id });
    }

    public IEnumerable<Flat> GetByApartmentId(long apartmentId)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<Flat>(sql, new { @ApartmentId = apartmentId }).ToList();
    }

    public Flat GetByFlatCode(string flatCode)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<Flat>(sql, new { @FlatCode = flatCode }).SingleOrDefault();
    }

    public Flat GetById(long id)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<Flat>(sql, new { @FlatId = id }).SingleOrDefault();
    }

    public FlatExt GetExtById(long id)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<FlatExt>(sql, new { @FlatId = id }).SingleOrDefault();
    }

    public IEnumerable<FlatExt> GetExtsByBusinessId(int businessId)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<FlatExt>(sql, new { @BusinessId = businessId }).ToList();
    }

    public void Update(Flat flat)
    {
        using var connection = _context.CreateConnection();
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
        connection.Execute(sql, flat);
    }
}

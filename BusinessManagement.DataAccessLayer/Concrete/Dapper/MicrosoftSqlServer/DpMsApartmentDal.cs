using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.ExtendedDatabaseModels;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsApartmentDal : IApartmentDal
{
    private readonly DapperContext _context;

    public DpMsApartmentDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(Apartment apartment)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<long>(sql, apartment).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM Apartment"
            + " WHERE ApartmentId = @ApartmentId";
        connection.Execute(sql, new { @ApartmentId = id });
    }

    public Apartment GetByApartmentCode(string apartmentCode)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<Apartment>(sql, new { @ApartmentCode = apartmentCode }).SingleOrDefault();
    }

    public IEnumerable<Apartment> GetByBusinessId(int businessId)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<Apartment>(sql, new { @BusinessId = businessId }).ToList();
    }

    public Apartment GetById(long id)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<Apartment>(sql, new { @ApartmentId = id }).SingleOrDefault();
    }

    public IEnumerable<Apartment> GetBySectionId(int sectionId)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<Apartment>(sql, new { @SectionId = sectionId }).ToList();
    }

    public ApartmentExt GetExtById(long id)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<ApartmentExt>(sql, new { @ApartmentId = id }).SingleOrDefault();
    }

    public IEnumerable<ApartmentExt> GetExtsByBusinessId(int businessId)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<ApartmentExt>(sql, new { @BusinessId = businessId }).ToList();
    }

    public void Update(Apartment apartment)
    {
        using var connection = _context.CreateConnection();
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
        connection.Execute(sql, apartment);
    }
}

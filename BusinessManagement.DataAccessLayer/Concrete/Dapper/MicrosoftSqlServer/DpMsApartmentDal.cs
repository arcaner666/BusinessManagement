using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
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

    public Apartment Add(Apartment apartment)
    {
        var sql = "INSERT INTO Apartment (SectionId, BusinessId, BranchId, ManagerId, ApartmentName, ApartmentCode, BlockNumber, CreatedAt, UpdatedAt)"
            + " VALUES(@SectionId, @BusinessId, @BranchId, @ManagerId, @ApartmentName, @ApartmentCode, @BlockNumber, @CreatedAt, @UpdatedAt) SELECT CAST(SCOPE_IDENTITY() as int)";
        var id = _db.Query<int>(sql, apartment).Single();
        apartment.ApartmentId = id;
        return apartment;
    }

    public void Delete(long id)
    {
        var sql = "DELETE FROM Apartment"
            + " WHERE ApartmentId = @ApartmentId";
        _db.Execute(sql, new { @ApartmentId = id });
    }

    public Apartment GetByApartmentCode(string apartmentCode)
    {
        var sql = "SELECT * FROM Apartment"
            + " WHERE ApartmentCode = @ApartmentCode";
        return _db.Query<Apartment>(sql, new { @ApartmentCode = apartmentCode }).SingleOrDefault();
    }

    public List<Apartment> GetByBusinessId(int businessId)
    {
        var sql = "SELECT * FROM Apartment"
            + " WHERE BusinessId = @BusinessId";
        return _db.Query<Apartment>(sql, new { @BusinessId = businessId }).ToList();
    }

    public Apartment GetById(long id)
    {
        var sql = "SELECT * FROM Apartment"
            + " WHERE ApartmentId = @ApartmentId";
        return _db.Query<Apartment>(sql, new { @ApartmentId = id }).SingleOrDefault();
    }

    public List<Apartment> GetBySectionId(int sectionId)
    {
        var sql = "SELECT * FROM Apartment"
            + " WHERE SectionId = @SectionId";
        return _db.Query<Apartment>(sql, new { @SectionId = sectionId }).ToList();
    }

    public Apartment GetExtById(long id)
    {
        var sql = "SELECT * FROM Apartment a"
            + " INNER JOIN Section s ON a.SectionId = s.SectionId"
            + " INNER JOIN Manager m ON a.ManagerId = m.ManagerId"
            + " WHERE a.ApartmentId = @ApartmentId";
        return _db.Query<Apartment, Section, Manager, Apartment>(sql,
            (apartment, section, manager) =>
            {
                apartment.Section = section;
                apartment.Manager = manager;
                return apartment;
            }, new { @ApartmentId = id },
            splitOn: "SectionId,ManagerId").SingleOrDefault();
    }

    public List<Apartment> GetExtsByBusinessId(int businessId)
    {
        var sql = "SELECT * FROM Apartment a"
            + " INNER JOIN Section s ON a.SectionId = s.SectionId"
            + " INNER JOIN Manager m ON a.ManagerId = m.ManagerId"
            + " WHERE a.BusinessId = @BusinessId";
        return _db.Query<Apartment, Section, Manager, Apartment>(sql,
            (apartment, section, manager) =>
            {
                apartment.Section = section;
                apartment.Manager = manager;
                return apartment;
            }, new { @BusinessId = businessId },
            splitOn: "SectionId,ManagerId").ToList();
    }

    public void Update(Apartment apartment)
    {
        var sql = "UPDATE Apartment SET SectionId = @SectionId, BusinessId = @BusinessId, BranchId = @BranchId, ManagerId = @ManagerId, ApartmentName = @ApartmentName, ApartmentCode = @ApartmentCode, BlockNumber = @BlockNumber, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt"
            + " WHERE ApartmentId = @ApartmentId";
        _db.Execute(sql, apartment);
    }
}

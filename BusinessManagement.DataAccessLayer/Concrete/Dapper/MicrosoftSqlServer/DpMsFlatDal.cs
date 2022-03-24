using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer
{
    public class DpMsFlatDal : IFlatDal
    {
        private readonly IDbConnection _db;

        public DpMsFlatDal(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public Flat Add(Flat flat)
        {
            var sql = "INSERT INTO Flat (SectionId, ApartmentId, BusinessId, BranchId, HouseOwnerId, TenantId, FlatCode, DoorNumber, CreatedAt, UpdatedAt)"
                + " VALUES(@SectionId, @ApartmentId, @BusinessId, @BranchId, @HouseOwnerId, @TenantId, @FlatCode, @DoorNumber, @CreatedAt, @UpdatedAt) SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = _db.Query<int>(sql, flat).Single();
            flat.FlatId = id;
            return flat;
        }

        public void Delete(long id)
        {
            var sql = "DELETE FROM Flat"
                + " WHERE FlatId = @FlatId";
            _db.Execute(sql, new { @FlatId = id });
        }

        public List<Flat> GetByApartmentId(long apartmentId)
        {
            var sql = "SELECT * FROM Flat"
                + " WHERE ApartmentId = @ApartmentId";
            return _db.Query<Flat>(sql, new { @ApartmentId = apartmentId }).ToList();
        }

        public Flat GetByFlatCode(string flatCode)
        {
            var sql = "SELECT * FROM Flat"
                + " WHERE FlatCode = @FlatCode";
            return _db.Query<Flat>(sql, new { @FlatCode = flatCode }).SingleOrDefault();
        }

        public Flat GetById(long id)
        {
            var sql = "SELECT * FROM Flat"
                + " WHERE FlatId = @FlatId";
            return _db.Query<Flat>(sql, new { @FlatId = id }).SingleOrDefault();
        }

        public List<Flat> GetExtsByBusinessId(int businessId)
        {
            var sql = "SELECT * FROM Flat f"
                    + " INNER JOIN Section s ON f.SectionId = s.SectionId"
                    + " INNER JOIN Apartment a ON f.ApartmentId = a.ApartmentId"
                    + " LEFT JOIN HouseOwner o ON f.HouseOwnerId = o.HouseOwnerId"
                    + " LEFT JOIN Tenant t ON f.TenantId = t.TenantId"
                    + " WHERE f.BusinessId = @BusinessId;";
            return _db.Query<Flat, Section, Apartment, HouseOwner, Tenant, Flat>(sql,
                (flat, section, apartment, houseOwner, tenant) =>
                {
                    flat.Section = section;
                    flat.Apartment = apartment;
                    if (houseOwner != null)
                        flat.HouseOwner = houseOwner;
                    if (tenant != null)
                        flat.Tenant = tenant;
                    return flat;
                }, new { @BusinessId = businessId },
                splitOn: "SectionId,ApartmentId,HouseOwnerId,TenantId").ToList();
        }

        public void Update(Flat flat)
        {
            var sql = "UPDATE Flat SET SectionId = @SectionId, ApartmentId = @ApartmentId, BusinessId = @BusinessId, BranchId = @BranchId, HouseOwnerId = @HouseOwnerId, TenantId = @TenantId, FlatCode = @FlatCode, DoorNumber = @DoorNumber, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt"
                + " WHERE FlatId = @FlatId";
            _db.Execute(sql, flat);
        }
    }
}

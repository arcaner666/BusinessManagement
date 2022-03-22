using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer
{
    public class DpMsTenantDal : ITenantDal
    {
        private readonly IDbConnection _db;

        public DpMsTenantDal(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public Tenant Add(Tenant tenant)
        {
            var sql = "INSERT INTO Tenant (BusinessId, BranchId, AccountId, NameSurname, Email, Phone, DateOfBirth, Gender, Notes, AvatarUrl, CreatedAt, UpdatedAt)"
                + " VALUES(@BusinessId, @BranchId, @AccountId, @NameSurname, @Email, @Phone, @DateOfBirth, @Gender, @Notes, @AvatarUrl, @CreatedAt, @UpdatedAt) SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = _db.Query<int>(sql, tenant).Single();
            tenant.TenantId = id;
            return tenant;
        }

        public void Delete(long id)
        {
            var sql = "DELETE FROM Tenant"
                + " WHERE TenantId = @TenantId";
            _db.Execute(sql, new { @TenantId = id });
        }

        public Tenant GetById(long id)
        {
            var sql = "SELECT TenantId, BusinessId, BranchId, AccountId, NameSurname, Email, Phone, DateOfBirth, Gender, Notes, AvatarUrl, CreatedAt, UpdatedAt"
                + " FROM Tenant"
                + " WHERE TenantId = @TenantId";
            return _db.Query<Tenant>(sql, new { @TenantId = id }).SingleOrDefault();
        }

        public List<Tenant> GetExtsByBusinessId(int businessId)
        {
            var sql = "SELECT TenantId, BusinessId, BranchId, AccountId, NameSurname, Email, Phone, DateOfBirth, Gender, Notes, AvatarUrl, CreatedAt, UpdatedAt"
                + " FROM Tenant"
                + " WHERE BusinessId = @BusinessId";
            return _db.Query<Tenant>(sql, new { @BusinessId = businessId }).ToList();
        }

        public Tenant GetIfAlreadyExist(int businessId, long accountId)
        {
            var sql = "SELECT TenantId, BusinessId, BranchId, AccountId, NameSurname, Email, Phone, DateOfBirth, Gender, Notes, AvatarUrl, CreatedAt, UpdatedAt"
                + " FROM Tenant"
                + " WHERE BusinessId = @BusinessId AND AccountId = @AccountId";
            return _db.Query<Tenant>(sql, new
            {
                @BusinessId = businessId,
                @AccountId = accountId,
            }).SingleOrDefault();
        }

        public void Update(Tenant tenant)
        {
            var sql = "UPDATE Tenant SET BusinessId = @BusinessId, BranchId = @BranchId, AccountId = @AccountId, NameSurname = @NameSurname, Email = @Email, Phone = @Phone, DateOfBirth = @DateOfBirth, Gender = @Gender, Notes = @Notes, AvatarUrl = @AvatarUrl, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt"
                + " WHERE TenantId = @TenantId";
            _db.Execute(sql, tenant);
        }
    }
}

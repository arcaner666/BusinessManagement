using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer
{
    public class DpMsManagerDal : IManagerDal
    {
        private readonly IDbConnection _db;

        public DpMsManagerDal(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public Manager Add(Manager manager)
        {
            var sql = "INSERT INTO Manager (BusinessId, BranchId, NameSurname, Email, Phone, DateOfBirth, Gender, Notes, AvatarUrl, CreatedAt, UpdatedAt)"
                + " VALUES(@BusinessId, @BranchId, @NameSurname, @Email, @Phone, @DateOfBirth, @Gender, @Notes, @AvatarUrl, @CreatedAt, @UpdatedAt) SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = _db.Query<int>(sql, manager).Single();
            manager.ManagerId = id;
            return manager;
        }

        public List<Manager> GetByBusinessId(int businessId)
        {
            var sql = "SELECT * FROM Manager"
                + " WHERE BusinessId = @BusinessId";
            return _db.Query<Manager>(sql, new { @BusinessId = businessId }).ToList();
        }

        public Manager GetByBusinessIdAndPhone(int businessId, string phone)
        {
            var sql = "SELECT * FROM Manager"
                + " WHERE BusinessId = @BusinessId AND Phone = @Phone";
            return _db.Query<Manager>(sql, new
            {
                @BusinessId = businessId,
                @Phone = phone,
            }).SingleOrDefault();
        }

        public List<Manager> GetExtsByBusinessId(int businessId)
        {
            var sql = "SELECT * FROM Manager m"
                + " INNER JOIN Business bu ON m.BusinessId = bu.BusinessId"
                + " INNER JOIN Branch br ON m.BranchId = br.BranchId"
                + " INNER JOIN FullAddress fa ON br.FullAddressId = fa.FullAddressId"
                + " WHERE m.BusinessId = @BusinessId";
            return _db.Query<Manager, Business, Branch, FullAddress, Manager>(sql,
                (manager, business, branch, fullAddress) =>
                {
                    manager.Business = business;
                    manager.Branch = branch;
                    manager.Branch.FullAddress = fullAddress;
                    return manager;
                }, new { @BusinessId = businessId },
                splitOn: "BusinessId,BranchId,FullAddressId").ToList();
        }
    }
}

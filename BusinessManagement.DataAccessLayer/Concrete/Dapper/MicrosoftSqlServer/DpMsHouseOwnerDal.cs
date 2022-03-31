using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsHouseOwnerDal : IHouseOwnerDal
{
    private readonly IDbConnection _db;

    public DpMsHouseOwnerDal(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public HouseOwner Add(HouseOwner houseOwner)
    {
        var sql = "INSERT INTO HouseOwner (BusinessId, BranchId, AccountId, NameSurname, Email, Phone, DateOfBirth, Gender, Notes, AvatarUrl, CreatedAt, UpdatedAt)"
            + " VALUES(@BusinessId, @BranchId, @AccountId, @NameSurname, @Email, @Phone, @DateOfBirth, @Gender, @Notes, @AvatarUrl, @CreatedAt, @UpdatedAt) SELECT CAST(SCOPE_IDENTITY() as int)";
        var id = _db.Query<int>(sql, houseOwner).Single();
        houseOwner.HouseOwnerId = id;
        return houseOwner;
    }

    public void Delete(long id)
    {
        var sql = "DELETE FROM HouseOwner"
            + " WHERE HouseOwnerId = @HouseOwnerId";
        _db.Execute(sql, new { @HouseOwnerId = id });
    }

    public HouseOwner GetById(long id)
    {
        var sql = "SELECT * FROM HouseOwner"
            + " WHERE HouseOwnerId = @HouseOwnerId";
        return _db.Query<HouseOwner>(sql, new { @HouseOwnerId = id }).SingleOrDefault();
    }

    public List<HouseOwner> GetExtsByBusinessId(int businessId)
    {
        var sql = "SELECT * FROM HouseOwner"
            + " WHERE BusinessId = @BusinessId";
        return _db.Query<HouseOwner>(sql, new { @BusinessId = businessId } ).ToList();
    }

    public HouseOwner GetIfAlreadyExist(int businessId, long accountId)
    {
        var sql = "SELECT * FROM HouseOwner"
            + " WHERE BusinessId = @BusinessId AND AccountId = @AccountId";
        return _db.Query<HouseOwner>(sql, new
        {
            @BusinessId = businessId,
            @AccountId = accountId,
        }).SingleOrDefault();
    }

    public void Update(HouseOwner houseOwner)
    {
        var sql = "UPDATE HouseOwner SET BusinessId = @BusinessId, BranchId = @BranchId, AccountId = @AccountId, NameSurname = @NameSurname, Email = @Email, Phone = @Phone, DateOfBirth = @DateOfBirth, Gender = @Gender, Notes = @Notes, AvatarUrl = @AvatarUrl, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt"
            + " WHERE HouseOwnerId = @HouseOwnerId";
        _db.Execute(sql, houseOwner);
    }
}

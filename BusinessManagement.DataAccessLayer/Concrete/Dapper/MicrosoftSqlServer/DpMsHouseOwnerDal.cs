using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.ExtendedDatabaseModels;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsHouseOwnerDal : IHouseOwnerDal
{
    private readonly DapperContext _context;

    public DpMsHouseOwnerDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(HouseOwner houseOwner)
    {
        using var connection = _context.CreateConnection();
        var sql = "INSERT INTO HouseOwner ("
            + " BusinessId,"
            + " BranchId,"
            + " AccountId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " DateOfBirth,"
            + " Gender,"
            + " Notes,"
            + " AvatarUrl,"
            + " TaxOffice,"
            + " TaxNumber,"
            + " IdentityNumber,"
            + " StandartMaturity,"
            + " CreatedAt,"
            + " UpdatedAt)"
            + " VALUES("
            + " @BusinessId,"
            + " @BranchId,"
            + " @AccountId,"
            + " @NameSurname,"
            + " @Email,"
            + " @Phone,"
            + " @DateOfBirth,"
            + " @Gender,"
            + " @Notes,"
            + " @AvatarUrl,"
            + " @TaxOffice,"
            + " @TaxNumber,"
            + " @IdentityNumber,"
            + " @StandartMaturity,"
            + " @CreatedAt,"
            + " @UpdatedAt)"
            + " SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
        return connection.Query<long>(sql, houseOwner).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM HouseOwner"
            + " WHERE HouseOwnerId = @HouseOwnerId";
        connection.Execute(sql, new { @HouseOwnerId = id });
    }

    public HouseOwner GetByAccountId(long accountId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " HouseOwnerId,"
            + " BusinessId,"
            + " BranchId,"
            + " AccountId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " DateOfBirth,"
            + " Gender,"
            + " Notes,"
            + " AvatarUrl,"
            + " TaxOffice,"
            + " TaxNumber,"
            + " IdentityNumber,"
            + " StandartMaturity,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM HouseOwner"
            + " WHERE AccountId = @AccountId";
        return connection.Query<HouseOwner>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public IEnumerable<HouseOwner> GetByBusinessId(int businessId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " HouseOwnerId,"
            + " BusinessId,"
            + " BranchId,"
            + " AccountId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " DateOfBirth,"
            + " Gender,"
            + " Notes,"
            + " AvatarUrl,"
            + " TaxOffice,"
            + " TaxNumber,"
            + " IdentityNumber,"
            + " StandartMaturity,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM HouseOwner"
            + " WHERE BusinessId = @BusinessId";
        return connection.Query<HouseOwner>(sql, new { @BusinessId = businessId }).ToList();
    }

    public HouseOwner GetByBusinessIdAndAccountId(int businessId, long accountId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " HouseOwnerId,"
            + " BusinessId,"
            + " BranchId,"
            + " AccountId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " DateOfBirth,"
            + " Gender,"
            + " Notes,"
            + " AvatarUrl,"
            + " TaxOffice,"
            + " TaxNumber,"
            + " IdentityNumber,"
            + " StandartMaturity,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM HouseOwner"
            + " WHERE BusinessId = @BusinessId AND AccountId = @AccountId";
        return connection.Query<HouseOwner>(sql, new
        {
            @BusinessId = businessId,
            @AccountId = accountId,
        }).SingleOrDefault();
    }

    public HouseOwner GetById(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " HouseOwnerId,"
            + " BusinessId,"
            + " BranchId,"
            + " AccountId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " DateOfBirth,"
            + " Gender,"
            + " Notes,"
            + " AvatarUrl,"
            + " TaxOffice,"
            + " TaxNumber,"
            + " IdentityNumber,"
            + " StandartMaturity,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM HouseOwner"
            + " WHERE HouseOwnerId = @HouseOwnerId";
        return connection.Query<HouseOwner>(sql, new { @HouseOwnerId = id }).SingleOrDefault();
    }

    public HouseOwnerExt GetExtByAccountId(long accountId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " ho.HouseOwnerId,"
            + " ho.BusinessId,"
            + " ho.BranchId,"
            + " ho.AccountId,"
            + " ho.NameSurname,"
            + " ho.Email,"
            + " ho.Phone,"
            + " ho.DateOfBirth,"
            + " ho.Gender,"
            + " ho.Notes,"
            + " ho.AvatarUrl,"
            + " ho.TaxOffice,"
            + " ho.TaxNumber,"
            + " ho.IdentityNumber,"
            + " ho.StandartMaturity,"
            + " ho.CreatedAt,"
            + " ho.UpdatedAt,"
            + " a.AccountGroupId,"
            + " a.AccountOrder,"
            + " a.AccountName,"
            + " a.AccountCode,"
            + " a.Limit,"
            + " ag.AccountGroupName"
            + " FROM HouseOwner ho"
            + " INNER JOIN Account a ON ho.AccountId = a.AccountId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " WHERE ho.AccountId = @AccountId";
        return connection.Query<HouseOwnerExt>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public HouseOwnerExt GetExtById(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " ho.HouseOwnerId,"
            + " ho.BusinessId,"
            + " ho.BranchId,"
            + " ho.AccountId,"
            + " ho.NameSurname,"
            + " ho.Email,"
            + " ho.Phone,"
            + " ho.DateOfBirth,"
            + " ho.Gender,"
            + " ho.Notes,"
            + " ho.AvatarUrl,"
            + " ho.TaxOffice,"
            + " ho.TaxNumber,"
            + " ho.IdentityNumber,"
            + " ho.StandartMaturity,"
            + " ho.CreatedAt,"
            + " ho.UpdatedAt,"
            + " a.AccountGroupId,"
            + " a.AccountOrder,"
            + " a.AccountName,"
            + " a.AccountCode,"
            + " a.Limit,"
            + " ag.AccountGroupName"
            + " FROM HouseOwner ho"
            + " INNER JOIN Account a ON ho.AccountId = a.AccountId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " WHERE ho.HouseOwnerId = @HouseOwnerId";
        return connection.Query<HouseOwnerExt>(sql, new { @HouseOwnerId = id }).SingleOrDefault();
    }

    public IEnumerable<HouseOwnerExt> GetExtsByBusinessId(int businessId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " ho.HouseOwnerId,"
            + " ho.BusinessId,"
            + " ho.BranchId,"
            + " ho.AccountId,"
            + " ho.NameSurname,"
            + " ho.Email,"
            + " ho.Phone,"
            + " ho.DateOfBirth,"
            + " ho.Gender,"
            + " ho.Notes,"
            + " ho.AvatarUrl,"
            + " ho.TaxOffice,"
            + " ho.TaxNumber,"
            + " ho.IdentityNumber,"
            + " ho.StandartMaturity,"
            + " ho.CreatedAt,"
            + " ho.UpdatedAt,"
            + " a.AccountGroupId,"
            + " a.AccountOrder,"
            + " a.AccountName,"
            + " a.AccountCode,"
            + " a.Limit,"
            + " ag.AccountGroupName"
            + " FROM HouseOwner ho"
            + " INNER JOIN Account a ON ho.AccountId = a.AccountId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " WHERE ho.BusinessId = @BusinessId";
        return connection.Query<HouseOwnerExt>(sql, new { @BusinessId = businessId }).ToList();
    }

    public void Update(HouseOwner houseOwner)
    {
        using var connection = _context.CreateConnection();
        var sql = "UPDATE HouseOwner SET"
            + " BusinessId = @BusinessId,"
            + " BranchId = @BranchId,"
            + " AccountId = @AccountId,"
            + " NameSurname = @NameSurname,"
            + " Email = @Email,"
            + " Phone = @Phone,"
            + " DateOfBirth = @DateOfBirth,"
            + " Gender = @Gender,"
            + " Notes = @Notes,"
            + " AvatarUrl = @AvatarUrl,"
            + " TaxOffice = @TaxOffice,"
            + " TaxNumber = @TaxNumber,"
            + " IdentityNumber = @IdentityNumber,"
            + " StandartMaturity = @StandartMaturity,"
            + " CreatedAt = @CreatedAt,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE HouseOwnerId = @HouseOwnerId";
        connection.Execute(sql, houseOwner);
    }
}

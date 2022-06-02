using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsHouseOwnerDal : IHouseOwnerDal
{
    private readonly DapperContext _context;

    public DpMsHouseOwnerDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(HouseOwnerDto houseOwnerDto)
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
        return connection.Query<long>(sql, houseOwnerDto).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM HouseOwner"
            + " WHERE HouseOwnerId = @HouseOwnerId";
        connection.Execute(sql, new { @HouseOwnerId = id });
    }

    public HouseOwnerDto GetByAccountId(long accountId)
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
        return connection.Query<HouseOwnerDto>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public List<HouseOwnerDto> GetByBusinessId(int businessId)
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
        return connection.Query<HouseOwnerDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public HouseOwnerDto GetByBusinessIdAndAccountId(int businessId, long accountId)
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
        return connection.Query<HouseOwnerDto>(sql, new
        {
            @BusinessId = businessId,
            @AccountId = accountId,
        }).SingleOrDefault();
    }

    public HouseOwnerDto GetById(long id)
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
        return connection.Query<HouseOwnerDto>(sql, new { @HouseOwnerId = id }).SingleOrDefault();
    }

    public HouseOwnerExtDto GetExtByAccountId(long accountId)
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
        return connection.Query<HouseOwnerExtDto>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public HouseOwnerExtDto GetExtById(long id)
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
        return connection.Query<HouseOwnerExtDto>(sql, new { @HouseOwnerId = id }).SingleOrDefault();
    }

    public List<HouseOwnerExtDto> GetExtsByBusinessId(int businessId)
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
        return connection.Query<HouseOwnerExtDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public void Update(HouseOwnerDto houseOwnerDto)
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
        connection.Execute(sql, houseOwnerDto);
    }
}

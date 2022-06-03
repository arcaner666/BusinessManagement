using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.ExtendedDatabaseModels;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsBankDal : IBankDal
{
    private readonly DapperContext _context;

    public DpMsBankDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(Bank bank)
    {
        using var connection = _context.CreateConnection();
        var sql = "INSERT INTO Bank (" 
            + " BusinessId," 
            + " BranchId,"
            + " AccountId,"
            + " FullAddressId,"
            + " CurrencyId,"
            + " BankName,"
            + " BankBranchName,"
            + " BankCode,"
            + " BankBranchCode,"
            + " BankAccountCode,"
            + " Iban,"
            + " OfficerName,"
            + " StandartMaturity,"
            + " CreatedAt,"
            + " UpdatedAt)"
            + " VALUES("
            + " @BusinessId," 
            + " @BranchId,"
            + " @AccountId,"
            + " @FullAddressId,"
            + " @CurrencyId,"
            + " @BankName,"
            + " @BankBranchName,"
            + " @BankCode,"
            + " @BankBranchCode,"
            + " @BankAccountCode,"
            + " @Iban,"
            + " @OfficerName,"
            + " @StandartMaturity,"
            + " @CreatedAt,"
            + " @UpdatedAt)"
            + " SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
        return connection.Query<long>(sql, bank).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM Bank"
            + " WHERE BankId = @BankId";
        connection.Execute(sql, new { @BankId = id });
    }

    public Bank GetByAccountId(long accountId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " BankId,"
            + " BusinessId,"
            + " BranchId,"
            + " AccountId,"
            + " FullAddressId,"
            + " CurrencyId,"
            + " BankName,"
            + " BankBranchName,"
            + " BankCode,"
            + " BankBranchCode,"
            + " BankAccountCode,"
            + " Iban,"
            + " OfficerName,"
            + " StandartMaturity,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Bank"
            + " WHERE AccountId = @AccountId";
        return connection.Query<Bank>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public IEnumerable<Bank> GetByBusinessId(int businessId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " BankId,"
            + " BusinessId,"
            + " BranchId,"
            + " AccountId,"
            + " FullAddressId,"
            + " CurrencyId,"
            + " BankName,"
            + " BankBranchName,"
            + " BankCode,"
            + " BankBranchCode,"
            + " BankAccountCode,"
            + " Iban,"
            + " OfficerName,"
            + " StandartMaturity,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Bank"
            + " WHERE BusinessId = @BusinessId";
        return connection.Query<Bank>(sql, new { @BusinessId = businessId }).ToList();
    }

    public Bank GetByBusinessIdAndIban(int businessId, string iban)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " BankId,"
            + " BusinessId,"
            + " BranchId,"
            + " AccountId,"
            + " FullAddressId,"
            + " CurrencyId,"
            + " BankName,"
            + " BankBranchName,"
            + " BankCode,"
            + " BankBranchCode,"
            + " BankAccountCode,"
            + " Iban,"
            + " OfficerName,"
            + " StandartMaturity,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Bank"
            + " WHERE BusinessId = @BusinessId AND Iban = @Iban";
        return connection.Query<Bank>(sql, new
        {
            @BusinessId = businessId,
            @Iban = iban,
        }).SingleOrDefault();
    }

    public Bank GetById(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " BankId,"
            + " BusinessId,"
            + " BranchId,"
            + " AccountId,"
            + " FullAddressId,"
            + " CurrencyId,"
            + " BankName,"
            + " BankBranchName,"
            + " BankCode,"
            + " BankBranchCode,"
            + " BankAccountCode,"
            + " Iban,"
            + " OfficerName,"
            + " StandartMaturity,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Bank"
            + " WHERE BankId = @BankId";
        return connection.Query<Bank>(sql, new { @BankId = id }).SingleOrDefault();
    }

    public BankExt GetExtByAccountId(long accountId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT" 
            + " b.BankId,"
            + " b.BusinessId,"
            + " b.BranchId,"
            + " b.AccountId,"
            + " b.FullAddressId,"
            + " b.CurrencyId,"
            + " b.BankName,"
            + " b.BankBranchName,"
            + " b.BankCode,"
            + " b.BankBranchCode,"
            + " b.BankAccountCode,"
            + " b.Iban,"
            + " b.OfficerName,"
            + " b.StandartMaturity,"
            + " b.CreatedAt,"
            + " b.UpdatedAt,"
            + " br.BranchName,"
            + " a.AccountGroupId,"
            + " a.AccountOrder,"
            + " a.AccountName,"
            + " a.AccountCode,"
            + " a.Limit,"
            + " ag.AccountGroupName,"
            + " fa.CityId,"
            + " fa.DistrictId,"
            + " fa.AddressText,"
            + " cu.CurrencyName"
            + " FROM Bank b"
            + " INNER JOIN Branch br ON b.BranchId = br.BranchId"
            + " INNER JOIN Account a ON b.AccountId = a.AccountId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " INNER JOIN FullAddress fa ON b.FullAddressId = fa.FullAddressId"
            + " INNER JOIN City c ON fa.CityId = c.CityId"
            + " INNER JOIN District d ON fa.DistrictId = d.DistrictId"
            + " INNER JOIN Currency cu ON b.CurrencyId = cu.CurrencyId"
            + " WHERE b.AccountId = @AccountId;";
        return connection.Query<BankExt>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public BankExt GetExtById(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " b.BankId,"
            + " b.BusinessId,"
            + " b.BranchId,"
            + " b.AccountId,"
            + " b.FullAddressId,"
            + " b.CurrencyId,"
            + " b.BankName,"
            + " b.BankBranchName,"
            + " b.BankCode,"
            + " b.BankBranchCode,"
            + " b.BankAccountCode,"
            + " b.Iban,"
            + " b.OfficerName,"
            + " b.StandartMaturity,"
            + " b.CreatedAt,"
            + " b.UpdatedAt,"
            + " br.BranchName,"
            + " a.AccountGroupId,"
            + " a.AccountOrder,"
            + " a.AccountName,"
            + " a.AccountCode,"
            + " a.Limit,"
            + " ag.AccountGroupName,"
            + " fa.CityId,"
            + " fa.DistrictId,"
            + " fa.AddressText,"
            + " cu.CurrencyName"
            + " FROM Bank b"
            + " INNER JOIN Branch br ON b.BranchId = br.BranchId"
            + " INNER JOIN Account a ON b.AccountId = a.AccountId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " INNER JOIN FullAddress fa ON b.FullAddressId = fa.FullAddressId"
            + " INNER JOIN City c ON fa.CityId = c.CityId"
            + " INNER JOIN District d ON fa.DistrictId = d.DistrictId"
            + " INNER JOIN Currency cu ON b.CurrencyId = cu.CurrencyId"
            + " WHERE b.BankId = @BankId;";
        return connection.Query<BankExt>(sql, new { @BankId = id }).SingleOrDefault();
    }

    public IEnumerable<BankExt> GetExtsByBusinessId(int businessId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " b.BankId,"
            + " b.BusinessId,"
            + " b.BranchId,"
            + " b.AccountId,"
            + " b.FullAddressId,"
            + " b.CurrencyId,"
            + " b.BankName,"
            + " b.BankBranchName,"
            + " b.BankCode,"
            + " b.BankBranchCode,"
            + " b.BankAccountCode,"
            + " b.Iban,"
            + " b.OfficerName,"
            + " b.StandartMaturity,"
            + " b.CreatedAt,"
            + " b.UpdatedAt,"
            + " br.BranchName,"
            + " a.AccountGroupId,"
            + " a.AccountOrder,"
            + " a.AccountName,"
            + " a.AccountCode,"
            + " a.Limit,"
            + " ag.AccountGroupName,"
            + " fa.CityId,"
            + " fa.DistrictId,"
            + " fa.AddressText,"
            + " cu.CurrencyName"
            + " FROM Bank b"
            + " INNER JOIN Branch br ON b.BranchId = br.BranchId"
            + " INNER JOIN Account a ON b.AccountId = a.AccountId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " INNER JOIN FullAddress fa ON b.FullAddressId = fa.FullAddressId"
            + " INNER JOIN City c ON fa.CityId = c.CityId"
            + " INNER JOIN District d ON fa.DistrictId = d.DistrictId"
            + " INNER JOIN Currency cu ON b.CurrencyId = cu.CurrencyId"
            + " WHERE b.BusinessId = @BusinessId;";
        return connection.Query<BankExt>(sql, new { @BusinessId = businessId }).ToList();
    }

    public void Update(Bank bank)
    {
        using var connection = _context.CreateConnection();
        var sql = "UPDATE Bank SET" 
            + " BusinessId = @BusinessId,"
            + " BranchId = @BranchId,"
            + " AccountId = @AccountId,"
            + " FullAddressId = @FullAddressId,"
            + " CurrencyId = @CurrencyId,"
            + " BankName = @BankName,"
            + " BankBranchName = @BankBranchName,"
            + " BankCode = @BankCode,"
            + " BankBranchCode = @BankBranchCode,"
            + " BankAccountCode = @BankAccountCode,"
            + " Iban = @Iban,"
            + " OfficerName = @OfficerName,"
            + " StandartMaturity = @StandartMaturity,"
            + " CreatedAt = @CreatedAt,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE BankId = @BankId";
        connection.Execute(sql, bank);
    }
}

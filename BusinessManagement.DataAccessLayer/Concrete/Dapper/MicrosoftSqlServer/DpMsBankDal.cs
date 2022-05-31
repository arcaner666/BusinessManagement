using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsBankDal : IBankDal
{
    private readonly IDbConnection _db;

    public DpMsBankDal(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public long Add(BankDto bankDto)
    {
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
        return _db.Query<long>(sql, bankDto).Single();
    }

    public void Delete(long id)
    {
        var sql = "DELETE FROM Bank"
            + " WHERE BankId = @BankId";
        _db.Execute(sql, new { @BankId = id });
    }

    public BankDto GetByAccountId(long accountId)
    {
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
        return _db.Query<BankDto>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public List<BankDto> GetByBusinessId(int businessId)
    {
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
        return _db.Query<BankDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public BankDto GetByBusinessIdAndIban(int businessId, string iban)
    {
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
        return _db.Query<BankDto>(sql, new
        {
            @BusinessId = businessId,
            @Iban = iban,
        }).SingleOrDefault();
    }

    public BankDto GetById(long id)
    {
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
        return _db.Query<BankDto>(sql, new { @BankId = id }).SingleOrDefault();
    }

    public BankExtDto GetExtByAccountId(long accountId)
    {
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
        return _db.Query<BankExtDto>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public BankExtDto GetExtById(long id)
    {
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
        return _db.Query<BankExtDto>(sql, new { @BankId = id }).SingleOrDefault();
    }

    public List<BankExtDto> GetExtsByBusinessId(int businessId)
    {
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
        return _db.Query<BankExtDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public void Update(BankDto bankDto)
    {
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
        _db.Execute(sql, bankDto);
    }
}

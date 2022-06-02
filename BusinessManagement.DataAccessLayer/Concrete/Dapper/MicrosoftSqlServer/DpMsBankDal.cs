﻿using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsBankDal : IBankDal
{
    private readonly DapperContext _context;

    public DpMsBankDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(BankDto bankDto)
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
        return connection.Query<long>(sql, bankDto).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM Bank"
            + " WHERE BankId = @BankId";
        connection.Execute(sql, new { @BankId = id });
    }

    public BankDto GetByAccountId(long accountId)
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
        return connection.Query<BankDto>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public List<BankDto> GetByBusinessId(int businessId)
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
        return connection.Query<BankDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public BankDto GetByBusinessIdAndIban(int businessId, string iban)
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
        return connection.Query<BankDto>(sql, new
        {
            @BusinessId = businessId,
            @Iban = iban,
        }).SingleOrDefault();
    }

    public BankDto GetById(long id)
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
        return connection.Query<BankDto>(sql, new { @BankId = id }).SingleOrDefault();
    }

    public BankExtDto GetExtByAccountId(long accountId)
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
        return connection.Query<BankExtDto>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public BankExtDto GetExtById(long id)
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
        return connection.Query<BankExtDto>(sql, new { @BankId = id }).SingleOrDefault();
    }

    public List<BankExtDto> GetExtsByBusinessId(int businessId)
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
        return connection.Query<BankExtDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public void Update(BankDto bankDto)
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
        connection.Execute(sql, bankDto);
    }
}

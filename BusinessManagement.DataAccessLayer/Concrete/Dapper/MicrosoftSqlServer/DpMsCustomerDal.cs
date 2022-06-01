using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsCustomerDal : ICustomerDal
{
    private readonly IDbConnection _db;

    public DpMsCustomerDal(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public long Add(CustomerDto customerDto)
    {
        var sql = "INSERT INTO Customer ("
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
            + " AppointmentsMade,"
            + " ProductsPurchased,"
            + " LastPurchaseDate,"
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
            + " @AppointmentsMade,"
            + " @ProductsPurchased,"
            + " @LastPurchaseDate,"
            + " @CreatedAt,"
            + " @UpdatedAt)"
            + " SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
        return _db.Query<long>(sql, customerDto).Single();
    }

    public CustomerDto GetByAccountId(long accountId)
    {
        var sql = "SELECT"
            + " CustomerId,"
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
            + " AppointmentsMade,"
            + " ProductsPurchased,"
            + " LastPurchaseDate,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Customer"
            + " WHERE AccountId = @AccountId";
        return _db.Query<CustomerDto>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public List<CustomerDto> GetByBusinessId(int businessId)
    {
        var sql = "SELECT"
            + " CustomerId,"
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
            + " AppointmentsMade,"
            + " ProductsPurchased,"
            + " LastPurchaseDate,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Customer"
            + " WHERE BusinessId = @BusinessId";
        return _db.Query<CustomerDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public CustomerDto GetByBusinessIdAndSystemUserId(int businessId, long systemUserId)
    {
        var sql = "SELECT"
            + " CustomerId,"
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
            + " AppointmentsMade,"
            + " ProductsPurchased,"
            + " LastPurchaseDate,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Customer"
            + " WHERE BusinessId = @BusinessId AND SystemUserId = @SystemUserId";
        return _db.Query<CustomerDto>(sql, new
        {
            @BusinessId = businessId,
            @SystemUserId = systemUserId,
        }).SingleOrDefault();
    }

    public CustomerDto GetById(long id)
    {
        var sql = "SELECT"
            + " CustomerId,"
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
            + " AppointmentsMade,"
            + " ProductsPurchased,"
            + " LastPurchaseDate,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Customer"
            + " WHERE CustomerId = @CustomerId";
        return _db.Query<CustomerDto>(sql, new { @CustomerId = id }).SingleOrDefault();
    }

    public List<CustomerDto> GetExtsByBusinessId(int businessId)
    {
        var sql = "SELECT"
            + " c.CustomerId,"
            + " c.BusinessId,"
            + " c.BranchId,"
            + " c.AccountId,"
            + " c.NameSurname,"
            + " c.Email,"
            + " c.Phone,"
            + " c.DateOfBirth,"
            + " c.Gender,"
            + " c.Notes,"
            + " c.AvatarUrl,"
            + " c.TaxOffice,"
            + " c.TaxNumber,"
            + " c.IdentityNumber,"
            + " c.StandartMaturity,"
            + " c.AppointmentsMade,"
            + " c.ProductsPurchased,"
            + " c.LastPurchaseDate,"
            + " c.CreatedAt,"
            + " c.UpdatedAt"
            + " FROM Customer c"
            + " WHERE BusinessId = @BusinessId";
        return _db.Query<CustomerDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public void Update(CustomerDto customerDto)
    {
        var sql = "UPDATE Customer SET"
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
            + " AppointmentsMade = @AppointmentsMade,"
            + " ProductsPurchased = @ProductsPurchased,"
            + " LastPurchaseDate = @LastPurchaseDate,"
            + " CreatedAt = @CreatedAt,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE CustomerId = @CustomerId";
        _db.Execute(sql, customerDto);
    }
}

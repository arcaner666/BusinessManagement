using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
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

    public Customer Add(Customer customer)
    {
        var sql = "INSERT INTO Customer (BusinessId, BranchId, AccountId, NameSurname, Email, Phone, DateOfBirth, Gender, Notes, AvatarUrl, TaxOffice, TaxNumber, IdentityNumber, StandartMaturity, AppointmentsMade, ProductsPurchased, LastPurchaseDate, CreatedAt, UpdatedAt)"
            + " VALUES(@BusinessId, @BranchId, @AccountId, @NameSurname, @Email, @Phone, @DateOfBirth, @Gender, @Notes, @AvatarUrl, @TaxOffice, @TaxNumber, @IdentityNumber, @StandartMaturity, @AppointmentsMade, @ProductsPurchased, @LastPurchaseDate, @CreatedAt, @UpdatedAt) SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
        var id = _db.Query<long>(sql, customer).Single();
        customer.CustomerId = id;
        return customer;
    }

    public Customer GetByAccountId(long accountId)
    {
        var sql = "SELECT * FROM Customer"
            + " WHERE AccountId = @AccountId";
        return _db.Query<Customer>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public List<Customer> GetByBusinessId(int businessId)
    {
        var sql = "SELECT * FROM Customer"
            + " WHERE BusinessId = @BusinessId";
        return _db.Query<Customer>(sql, new { @BusinessId = businessId }).ToList();
    }

    public Customer GetByBusinessIdAndSystemUserId(int businessId, long systemUserId)
    {
        var sql = "SELECT * FROM Customer"
            + " WHERE BusinessId = @BusinessId AND SystemUserId = @SystemUserId";
        return _db.Query<Customer>(sql, new
        {
            @BusinessId = businessId,
            @SystemUserId = systemUserId,
        }).SingleOrDefault();
    }

    public Customer GetById(long id)
    {
        var sql = "SELECT * FROM Customer"
            + " WHERE CustomerId = @CustomerId";
        return _db.Query<Customer>(sql, new { @CustomerId = id }).SingleOrDefault();
    }

    public List<Customer> GetExtsByBusinessId(int businessId)
    {
        var sql = "SELECT * FROM Customer"
            + " WHERE BusinessId = @BusinessId";
        return _db.Query<Customer>(sql, new { @BusinessId = businessId }).ToList();
    }

    public void Update(Customer customer)
    {
        var sql = "UPDATE Customer SET BusinessId = @BusinessId, BranchId = @BranchId, AccountId = @AccountId, NameSurname = @NameSurname, Email = @Email, Phone = @Phone, DateOfBirth = @DateOfBirth, Gender = @Gender, Notes = @Notes, AvatarUrl = @AvatarUrl, TaxOffice = @TaxOffice, TaxNumber = @TaxNumber, IdentityNumber = @IdentityNumber, StandartMaturity = @StandartMaturity, AppointmentsMade = @AppointmentsMade, ProductsPurchased = @ProductsPurchased, LastPurchaseDate = @LastPurchaseDate, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt"
            + " WHERE CustomerId = @CustomerId";
        _db.Execute(sql, customer);
    }
}

using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsCustomerDal : ICustomerDal
{
    private readonly DapperContext _context;

    public DpMsCustomerDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(Customer customer)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<long>(sql, customer).Single();
    }

    public Customer GetByAccountId(long accountId)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<Customer>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public IEnumerable<Customer> GetByBusinessId(int businessId)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<Customer>(sql, new { @BusinessId = businessId }).ToList();
    }

    public Customer GetByBusinessIdAndSystemUserId(int businessId, long systemUserId)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<Customer>(sql, new
        {
            @BusinessId = businessId,
            @SystemUserId = systemUserId,
        }).SingleOrDefault();
    }

    public Customer GetById(long id)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<Customer>(sql, new { @CustomerId = id }).SingleOrDefault();
    }

    public IEnumerable<Customer> GetExtsByBusinessId(int businessId)
    {
        using var connection = _context.CreateConnection();
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
        return connection.Query<Customer>(sql, new { @BusinessId = businessId }).ToList();
    }

    public void Update(Customer customer)
    {
        using var connection = _context.CreateConnection();
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
        connection.Execute(sql, customer);
    }
}

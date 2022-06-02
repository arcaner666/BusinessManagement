using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsFullAddressDal : IFullAddressDal
{
    private readonly DapperContext _context;

    public DpMsFullAddressDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(FullAddressDto fullAddressDto)
    {
        using var connection = _context.CreateConnection();
        var sql = "INSERT INTO FullAddress ("
            + " CityId,"
            + " DistrictId,"
            + " AddressTitle,"
            + " PostalCode,"
            + " AddressText,"
            + " CreatedAt,"
            + " UpdatedAt)"
            + " VALUES("
            + " @CityId,"
            + " @DistrictId,"
            + " @AddressTitle,"
            + " @PostalCode,"
            + " @AddressText,"
            + " @CreatedAt,"
            + " @UpdatedAt)"
            + " SELECT CAST(SCOPE_IDENTITY() AS BIGINT);";
        return connection.Query<long>(sql, fullAddressDto).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM FullAddress"
            + " WHERE FullAddressId = @FullAddressId";
        connection.Execute(sql, new { @FullAddressId = id });
    }

    public FullAddressDto GetByAddressText(string addressText)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " FullAddressId,"
            + " CityId,"
            + " DistrictId,"
            + " AddressTitle,"
            + " PostalCode,"
            + " AddressText,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM FullAddress"
            + " WHERE AddressText = @AddressText";
        return connection.Query<FullAddressDto>(sql, new { @AddressText = addressText }).SingleOrDefault();
    }

    public FullAddressDto GetById(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " FullAddressId,"
            + " CityId,"
            + " DistrictId,"
            + " AddressTitle,"
            + " PostalCode,"
            + " AddressText,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM FullAddress"
            + " WHERE FullAddressId = @FullAddressId";
        return connection.Query<FullAddressDto>(sql, new { @FullAddressId = id }).SingleOrDefault();
    }

    public void Update(FullAddressDto fullAddressDto)
    {
        using var connection = _context.CreateConnection();
        var sql = "UPDATE FullAddress SET"
            + " CityId = @CityId,"
            + " DistrictId = @DistrictId,"
            + " AddressTitle = @AddressTitle,"
            + " PostalCode = @PostalCode,"
            + " AddressText = @AddressText,"
            + " CreatedAt = @CreatedAt,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE FullAddressId = @FullAddressId";
        connection.Execute(sql, fullAddressDto);
    }
}

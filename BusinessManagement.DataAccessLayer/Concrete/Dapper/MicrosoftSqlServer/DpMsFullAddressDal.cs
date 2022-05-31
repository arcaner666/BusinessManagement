using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsFullAddressDal : IFullAddressDal
{
    private readonly IDbConnection _db;

    public DpMsFullAddressDal(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public long Add(FullAddressDto fullAddressDto)
    {
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
        return _db.Query<long>(sql, fullAddressDto).Single();
    }

    public void Delete(long id)
    {
        var sql = "DELETE FROM FullAddress"
            + " WHERE FullAddressId = @FullAddressId";
        _db.Execute(sql, new { @FullAddressId = id });
    }

    public FullAddressDto GetByAddressText(string addressText)
    {
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
        return _db.Query<FullAddressDto>(sql, new { @AddressText = addressText }).SingleOrDefault();
    }

    public FullAddressDto GetById(long id)
    {
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
        return _db.Query<FullAddressDto>(sql, new { @FullAddressId = id }).SingleOrDefault();
    }

    public void Update(FullAddressDto fullAddressDto)
    {
        var sql = "UPDATE FullAddress SET"
            + " CityId = @CityId,"
            + " DistrictId = @DistrictId,"
            + " AddressTitle = @AddressTitle,"
            + " PostalCode = @PostalCode,"
            + " AddressText = @AddressText,"
            + " CreatedAt = @CreatedAt,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE FullAddressId = @FullAddressId";
        _db.Execute(sql, fullAddressDto);
    }
}

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

    public FullAddress Add(FullAddress fullAddress)
    {
        var sql = "INSERT INTO FullAddress (CityId, DistrictId, AddressTitle, PostalCode, AddressText, CreatedAt, UpdatedAt)"
            + " VALUES(@CityId, @DistrictId, @AddressTitle, @PostalCode, @AddressText, @CreatedAt, @UpdatedAt) SELECT CAST(SCOPE_IDENTITY() AS BIGINT);";
        var id = _db.Query<long>(sql, fullAddress).Single();
        fullAddress.FullAddressId = id;
        return fullAddress;
    }

    public void Delete(long id)
    {
        var sql = "DELETE FROM FullAddress"
            + " WHERE FullAddressId = @FullAddressId";
        _db.Execute(sql, new { @FullAddressId = id });
    }

    public FullAddress GetByAddressText(string addressText)
    {
        var sql = "SELECT * FROM FullAddress"
            + " WHERE AddressText = @AddressText";
        return _db.Query<FullAddress>(sql, new { @AddressText = addressText }).SingleOrDefault();
    }

    public FullAddress GetById(long id)
    {
        var sql = "SELECT * FROM FullAddress"
            + " WHERE FullAddressId = @FullAddressId";
        return _db.Query<FullAddress>(sql, new { @FullAddressId = id }).SingleOrDefault();
    }

    public void Update(FullAddress fullAddress)
    {
        var sql = "UPDATE FullAddress SET CityId = @CityId, DistrictId = @DistrictId, AddressTitle = @AddressTitle, PostalCode = @PostalCode, AddressText = @AddressText, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt"
            + " WHERE FullAddressId = @FullAddressId";
        _db.Execute(sql, fullAddress);
    }
}

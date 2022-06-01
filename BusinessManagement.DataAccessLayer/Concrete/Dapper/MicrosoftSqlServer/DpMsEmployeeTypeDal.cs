﻿using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsEmployeeTypeDal : IEmployeeTypeDal
{
    private readonly IDbConnection _db;

    public DpMsEmployeeTypeDal(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public List<EmployeeTypeDto> GetAll()
    {
        var sql = "SELECT"
            + " EmployeeTypeId,"
            + " EmployeeTypeName"
            + " FROM EmployeeType";
        return _db.Query<EmployeeTypeDto>(sql).ToList();
    }
}

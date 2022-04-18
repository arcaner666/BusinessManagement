﻿using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface ITenantBl
{
    IDataResult<TenantDto> Add(TenantDto tenantDto);
    IResult Delete(long id);
    IDataResult<List<TenantDto>> GetByBusinessId(int businessId);
    IDataResult<TenantDto> GetById(long id);
    IResult Update(TenantDto tenantDto);
}
﻿using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface ITenantDal
{
    long Add(Tenant tenant);
    void Delete(long id);
    Tenant GetByAccountId(long accountId);
    IEnumerable<Tenant> GetByBusinessId(int businessId);
    Tenant GetByBusinessIdAndAccountId(int businessId, long accountId);
    Tenant GetById(long id);
    TenantExt GetExtByAccountId(long accountId);
    TenantExt GetExtById(long id);
    IEnumerable<TenantExt> GetExtsByBusinessId(int businessId);
    void Update(Tenant tenant);
}

﻿using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IOperationClaimBl
{
    IDataResult<IEnumerable<OperationClaimDto>> GetAll();
    IDataResult<OperationClaimDto> GetByOperationClaimName(string operationClaimName);
}

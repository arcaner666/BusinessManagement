using BusinessManagement.BusinessLayer.Utilities.Interceptors;
using BusinessManagement.BusinessLayer.Utilities.Results;
using Castle.DynamicProxy;
using System.Transactions;

namespace BusinessManagement.BusinessLayer.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using TransactionScope transactionScope = new();
            try
            {
                invocation.Proceed();
                var result = invocation.ReturnValue as IResult;
                if (result.Success)
                {
                    transactionScope.Complete();
                }
            }
            catch (Exception)
            {
                transactionScope.Dispose();
                throw;
            }
        }
    }
}

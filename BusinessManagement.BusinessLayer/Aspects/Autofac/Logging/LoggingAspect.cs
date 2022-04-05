using BusinessManagement.BusinessLayer.CrossCuttingConcerns.Logging;
using BusinessManagement.BusinessLayer.Utilities.Interceptors;
using Castle.DynamicProxy;

namespace BusinessManagement.BusinessLayer.Aspects.Autofac.Logging;

public class LoggingAspect : MethodInterception
{
    private readonly ILoggerManager _logger;
    public LoggingAspect(ILoggerManager logger)
    {
        _logger = logger;
    }
    protected override void OnException(IInvocation invocation, Exception exception) 
    {
        _logger.LogError(exception.Message);
    }
}

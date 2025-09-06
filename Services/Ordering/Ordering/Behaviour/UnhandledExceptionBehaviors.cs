using MediatR;

namespace Ordering.Behaviour
{
    //Global exception handler for all middleware request 
    // We are basically intercepting all middleware requests and certainly log any unhandled exceptio, without polluting the handlers with try catch block
    public class UnhandledExceptionBehaviors<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehaviors(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogError(ex,$"Unhandles exception occured with Request Name: {requestName}, {request}");
                throw;
            }
        }
    }
}

using Application.Common.Exceptions;
using Ardalis.GuardClauses;

namespace Web.Infrastructure
{
    public class GraphQLExceptionFilter : IErrorFilter
    {
        private readonly Dictionary<Type, Func<IError, IError>> _exceptionHandlers;

        public GraphQLExceptionFilter()
        {
            _exceptionHandlers = new()
            {
                { typeof(ValidationException), e => e.WithMessage(e.Exception.Message) },
                { typeof(NotFoundException), e => e.WithMessage(e.Exception.Message)},
                { typeof(UnauthorizedAccessException), e => e.WithMessage(e.Exception.Message) },
                { typeof(ForbiddenAccessException), e => e.WithMessage(e.Exception.Message) },
            };
        }

        public IError OnError(IError error)
        {
            if (error.Exception != null) 
            {
                var exceptionType = error.Exception.GetType();

                if (_exceptionHandlers.TryGetValue(exceptionType, out var handler))
                {
                    return handler.Invoke(error).RemoveExtensions();
                }
            }
            
            return error;
        }
    }
}

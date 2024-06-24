using System.Reflection;
using Application.Common.Exceptions;
using Application.Common.Identity;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse>(
    IUser user) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        List<AuthorizeAttribute> authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>().ToList();

        if (authorizeAttributes.Any())
        {
            // Must be authenticated user
            if (user.Id == null)
            {
                throw new UnauthorizedAccessException();
            }

            // Role-based authorization
            List<AuthorizeAttribute> authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles)).ToList();

            if (authorizeAttributesWithRoles.Any())
            {
                bool authorized = false;

                foreach (var roles in authorizeAttributesWithRoles.Select(a => a.Roles.Split(',')))
                {
                    foreach (var role in roles)
                    {
                        bool isInRole = user.Roles?.Contains(role) ?? false;
                        if (isInRole)
                        {
                            authorized = true;
                            break;
                        }
                    }
                }

                // Must be a member of at least one role in roles
                if (!authorized)
                {
                    throw new ForbiddenAccessException();
                }
            }
        }

        // User is authorized / authorization not required
        return await next();
    }
}

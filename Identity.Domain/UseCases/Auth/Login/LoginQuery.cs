using MediatR;
using Identity.Domain.DTOs;

namespace IdentityAPI.UseCases.Auth;

public record LoginQuery(SignInDTO Model) : IRequest<SsoDTO>;

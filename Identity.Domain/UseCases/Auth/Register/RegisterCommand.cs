using MediatR;
using Identity.Domain.DTOs;

namespace IdentityAPI.UseCases.Auth;

public record RegisterCommand(SignUpDTO Model) : IRequest<Unit>;

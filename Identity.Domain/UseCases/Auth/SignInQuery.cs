using Identity.Domain.DTOs;
using MediatR;

namespace Identity.Domain.UseCases.Auth;

public record SignInQuery(SignInDTO SignInDTO) : IRequest<SsoDTO>;

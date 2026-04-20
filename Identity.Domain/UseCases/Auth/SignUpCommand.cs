using Identity.Domain.DTOs;
using MediatR;

namespace Identity.Domain.UseCases.Auth;

public record SignUpCommand(SignUpDTO SignUpDTO) : IRequest<bool>;

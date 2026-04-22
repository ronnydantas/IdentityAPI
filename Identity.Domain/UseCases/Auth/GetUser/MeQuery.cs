using MediatR;
using Identity.Domain.DTOs;

namespace Identity.Domain.UseCases.Auth.GetUser;

public record MeQuery() : IRequest<UserInfoDTO>;

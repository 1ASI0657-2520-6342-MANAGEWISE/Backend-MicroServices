using Domain.Entities;
using MediatR;

namespace Application.Commands;

public record SignInCommand(string Username, string Password) : IRequest<User?>;
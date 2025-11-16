using Domain.Entities;
using MediatR;

// Adjust as needed

namespace Application.Commands;

public record SignUpCommand(string Username, string Password, int UserRole) : IRequest<User>;

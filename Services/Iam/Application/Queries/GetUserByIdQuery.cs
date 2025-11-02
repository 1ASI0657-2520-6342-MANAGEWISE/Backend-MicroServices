using Application.DTOs;
using MediatR;

// Adjust as needed

namespace Application.Queries;

public record GetUserByIdQuery(int Id) : IRequest<UserDto?>;

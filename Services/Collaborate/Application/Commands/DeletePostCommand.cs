using MediatR;

namespace AidManager.Collaborate.Application.Commands;

// Returns bool indicating success
public record DeletePostCommand(int Id) : IRequest<bool>;

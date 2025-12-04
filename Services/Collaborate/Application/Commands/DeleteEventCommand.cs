using MediatR;

namespace AidManager.Collaborate.Application.Commands;

// Returns bool indicating success
public record DeleteEventCommand(int EventId) : IRequest<bool>;

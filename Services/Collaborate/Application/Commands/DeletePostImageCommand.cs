using MediatR;

namespace AidManager.Collaborate.Application.Commands;

// Returns bool indicating success
public record DeletePostImageCommand(int PostId, int PostImageId) : IRequest<bool>;

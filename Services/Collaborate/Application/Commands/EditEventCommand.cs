using MediatR;
using AidManager.Collaborate.Application.DTOs; // For EventDto
using System;

namespace AidManager.Collaborate.Application.Commands;

// Assuming this command returns the updated EventDto
public record EditEventCommand(
    int Id,
    string Name,
    DateTime Date, // Added Date based on typical edit scenarios
    string Location,
    string Description
    // Color was not in the original, can be added if needed
) : IRequest<EventDto?>;

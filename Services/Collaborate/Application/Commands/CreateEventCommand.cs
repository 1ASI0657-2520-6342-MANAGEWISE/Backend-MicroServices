using MediatR;
using AidManager.Collaborate.Application.DTOs; // For EventDto
using System;
using System.Collections.Generic;


namespace AidManager.Collaborate.Application.Commands;

// Assuming this command returns the created EventDto
public record CreateEventCommand(
    string Name,
    DateTime Date, // Changed from string to DateTime
    string Location,
    string Description,
    string Color,
    int ProjectId
) : IRequest<EventDto>;

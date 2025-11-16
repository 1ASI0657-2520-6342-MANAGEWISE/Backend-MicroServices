using System;

namespace AidManager.API.Services.Profiles.Application.DTOs
{
    public record DeletedUserResource(
        int Id,
        string Name,
        int Age,
        string Email,
        string Phone,
        string Password,
        string ProfileImg,
        string Role,
        int CompanyId,
        string CompanyName,
        string CompanyEmail,
        string CompanyCountry,
        DateTime DeletedAt
    );
}

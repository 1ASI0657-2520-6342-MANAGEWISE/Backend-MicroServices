namespace AidManager.API.Services.Profiles.Application.DTOs
{
    public record UserDto(
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
        string CompanyCountry
    );
}

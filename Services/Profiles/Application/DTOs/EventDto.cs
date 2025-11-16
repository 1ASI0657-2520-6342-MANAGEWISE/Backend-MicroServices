namespace AidManager.API.Services.Profiles.Application.DTOs
{
    public record EventDto(
        int Id,
        string Name,
        string Date,
        string Location,
        string Description,
        string Color,
        int ProjectId
    );
}

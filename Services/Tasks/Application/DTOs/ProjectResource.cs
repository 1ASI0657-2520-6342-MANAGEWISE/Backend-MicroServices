

// Assuming User DTO might come from a shared kernel or another Bounded Context in a real scenario.
// For now, TeamMemberDto will be used for UserList as discussed.

namespace Application.DTOs
{
    public record ProjectResource(
        int Id,
        DateOnly AuditDate, // Renamed from 'Audit' to match entity's 'AuditDate'
        string Name,
        string? Description, // Assuming description can be null
        DateOnly ProjectDate,
        TimeOnly ProjectTime,
        string ProjectLocation,
        int CompanyId,
        List<TeamMemberDto> UserList, // Changed from List<User> to List<TeamMemberDto>
        List<string> ImageUrl, // This should align with ProjectImage value object. If ProjectImage contains Id and Url, this might be List<ProjectImageDto> or similar.
                               // Given the command `AddProjectImageCommand(int ProjectId, List<string> ImagesUrl)`, List<string> here is consistent for output.
        double Rating
    );
}

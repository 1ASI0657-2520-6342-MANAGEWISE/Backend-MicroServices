namespace Application.DTOs
{
    public record TeamMemberDto(
        int Id,
        string Name,
        string Email,
        string Phone,
        string ImageUrl
    );
}

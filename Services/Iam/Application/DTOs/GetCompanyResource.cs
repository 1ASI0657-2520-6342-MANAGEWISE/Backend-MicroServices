namespace Application.DTOs
{
    public record GetCompanyResource(int Id, string CompanyName, string Country, string Email, int ManagerId, string TeamRegisterCode);
}

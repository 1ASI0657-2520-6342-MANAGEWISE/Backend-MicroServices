namespace AidManager.API.Services.Profiles.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
                public string? FirstName { get; set; }
                public string? LastName { get; set; }
        public int Age { get; set; }
                public string? Email { get; set; }
                public string? Phone { get; set; }
                public string? Password { get; set; }
                public string? ProfileImg { get; set; }
        public int CompanyId { get; set; }
        public int Role { get; set; } // Consider using an Enum for Role if applicable
    }
}

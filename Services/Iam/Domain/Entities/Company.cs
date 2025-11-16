namespace Domain.Entities
{
    public class Company
    {
        public int Id { get; private set; }
        public string CompanyName { get; private set; }
        public string Country { get; private set; }
        public string Email { get; private set; }
        public int ManagerId { get; private set; }
        public string TeamRegisterCode { get; private set; }

        // Private constructor for EF Core
        private Company() { }

        public Company(string companyName, string country, string email, int managerId)
        {
            CompanyName = companyName;
            Country = country;
            Email = email;
            ManagerId = managerId;
            TeamRegisterCode = GenerateRegisterCode();
        }

        public void UpdateCompanyDetails(string companyName, string country, string email)
        {
            CompanyName = companyName;
            Country = country;
            Email = email;
        }

        private string GenerateRegisterCode()
        {
            Guid guid = Guid.NewGuid();
            string base64Guid = Convert.ToBase64String(guid.ToByteArray());
            // Replace URL-unsafe characters and shorten to 15 characters
            string shortGuid = base64Guid.Replace("/", "_").Replace("+", "-").Substring(0, 15);
            return shortGuid;
        }
    }
}

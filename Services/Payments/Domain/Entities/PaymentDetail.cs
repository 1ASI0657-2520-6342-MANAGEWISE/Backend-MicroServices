namespace AidManager.Services.Payments.Domain.Entities
{
    public class PaymentDetail
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CVV { get; set; }
    }
}

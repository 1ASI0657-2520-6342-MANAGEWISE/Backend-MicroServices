namespace AidManager.Services.Payments.Application.DTOs
{
    public record PaymentDetailDto(
        int Id,
        int UserId,
        string CardHolderName,
        string CardNumber,
        string ExpirationDate,
        string CVV
    );
}

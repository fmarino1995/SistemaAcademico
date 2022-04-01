namespace SistemaAcademicoApplication.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email);
    }
}

using Domain.Entities;

namespace SistemaAcademicoApplication.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EMailRequest mailRequest);
    }
}

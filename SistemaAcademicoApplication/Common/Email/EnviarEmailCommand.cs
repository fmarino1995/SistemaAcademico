using Domain.Entities;
using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoApplication.Interfaces;

namespace SistemaAcademicoApplication.Common.Email
{
    public class EnviarEmailCommand : IRequest<Response<bool>>
    {
        public EMailRequest EmailRequest { get; set; }
    }

    public class EnviarEmailCommandHandler : IRequestHandler<EnviarEmailCommand, Response<bool>>
    {
        private readonly IEmailService _emailService;

        public EnviarEmailCommandHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<Response<bool>> Handle(EnviarEmailCommand request, CancellationToken cancellationToken)
        {
            var errorResponse = new Response<bool>(false);

            try
            {

                if (request.EmailRequest == null)
                    throw new ArgumentNullException(nameof(EMailRequest));

                await _emailService.SendEmailAsync(request.EmailRequest);

                return new Response<bool>(true);
            }
            catch (Exception ex)
            {
                errorResponse.AddError("Falha ao enviar email : " + ex.Message);
                return errorResponse;
            }
        }
    }
}

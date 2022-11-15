// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using SistemaAcademicoApplication.Interfaces;

namespace SistemaAcademicoCore.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;

        public RegisterConfirmationModel(UserManager<ApplicationUser> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public bool DisplayConfirmAccountLink { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string EmailConfirmationUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string email, string senha, string returnUrl = null)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }
            returnUrl = returnUrl ?? Url.Content("~/");

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            Email = email;
            EMailRequest EmailModel;

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            EmailConfirmationUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                protocol: Request.Scheme);

            if (!string.IsNullOrEmpty(senha))
            {
                EmailModel = new EMailRequest
                {
                    ToEmail = email,
                    Subject = "Confirmação de conta - Sistema Acadêmico",
                    Body = "<h2><strong>Seja bem vindo ao Sistema Academico!</strong></h2> <br/>" +
                            "Sua conta foi criado com sucesso!<br/>" +
                            $"<p>Nome de Usuário : {user.Email}</p><br/>" +
                            $"<p>Senha temporária : {senha}</p><br/>" +
                            $"<p>Confirme sua conta <a href='{HtmlEncoder.Default.Encode(EmailConfirmationUrl)}'>clicando aqui</a></p><br/>",
                    UserName = user.NomeCompleto,
                    Attachments = null
                };
            }
            else
            {
                EmailModel = new EMailRequest
                {
                    ToEmail = email,
                    Subject = "Confirmação de conta - Sistema Acadêmico",
                    Body = "<h2><strong>Seja bem vindo ao Sistema Academico!</strong></h2> <br/>" +
                            "Sua conta foi criado com sucesso!<br/>" +
                            $"<p>Nome de Usuário : {user.Email}</p><br/>" +
                            $"<p>Confirme sua conta <a href='{HtmlEncoder.Default.Encode(EmailConfirmationUrl)}'>clicando aqui</a></p><br/>",
                    UserName = user.NomeCompleto,
                    Attachments = null
                };

            }

            await _emailService.SendEmailAsync(EmailModel);

            return Page();
        }
    }
}

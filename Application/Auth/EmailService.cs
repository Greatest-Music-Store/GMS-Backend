using SendGrid;
using SendGrid.Helpers.Mail;
namespace GMS_Backend.Application.Auth;
public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendResetPasswordEmail(string email, string resetLink, int[] codes)
    {
        var apiKey = _configuration["SendGrid:ApiKey"];

        var client = new SendGridClient(apiKey);

        var from = new EmailAddress(
            "lucas.jacchetti@gmail.com",
            "GMS");

        var to = new EmailAddress(email);

        var subject = "Recuperação de senha";

        var plainTextContent = $"Clique para redefinir sua senha: {resetLink}";

        var htmlContent = $"""
            <h1>Recuperação de senha</h1>

            <p>
                Clique no botão abaixo para redefinir sua senha.
            </p>
            <h1>
                {codes[0]} {codes[1]} {codes[2]} {codes[3]}
            <h1>

            <a href="{resetLink}">
                Redefinir senha
            </a>
            """;

        var msg = MailHelper.CreateSingleEmail(
            from,
            to,
            subject,
            plainTextContent,
            htmlContent);

        await client.SendEmailAsync(msg);
    }
}
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

    public async Task SendResetPasswordEmail(string email, string code)
    {
        var apiKey = _configuration["SendGrid:ApiKey"];

        var client = new SendGridClient(apiKey);

        var from = new EmailAddress(
            "lucas.jacchetti@gmail.com",
            "GMS");

        var to = new EmailAddress(email);

        var subject =
            "Recuperação de senha";

        var plainTextContent = $"""
            Seu código de recuperação é:

            {code}

            Este código expira em 10 minutos.
            """;

        var htmlContent = $"""
            <h1>Recuperação de senha</h1>

            <p>
                Utilize o código abaixo para redefinir sua senha:
            </p>

            <div style="
                font-size:32px;
                font-weight:bold;
                letter-spacing:4px;
                padding:16px;
                text-align:center;
                border:1px solid #ddd;
                border-radius:8px;">
                {code}
            </div>

            <p>
                Este código expira em 30 minutos.
            </p>
            """;

        var msg = MailHelper.CreateSingleEmail(
            from,
            to,
            subject,
            plainTextContent,
            htmlContent);

        var response = await client.SendEmailAsync(msg);
        Console.WriteLine($"Status: {response.StatusCode}");
    }
}
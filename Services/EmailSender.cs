using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace NetCoreMvc.Services;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string name, string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage();

        // PM> dotnet user-secrets init --project "Path to project"
        // PM> dotnet user-secrets set "EmailConfiguration:SmtpUsername" "xxxxx" --project "Project file system path"
        // PM> dotnet user-secrets set "EmailConfiguration:SmtpPassword" "xxxxx" --project "Project file system path"

        string smtpUsername = _configuration["EmailConfiguration:SmtpUsername"];
        string smtpPassword = _configuration["EmailConfiguration:SmtpPassword"];

        message.From.AddRange(new List<MailboxAddress>()
        {
            new MailboxAddress(name: "NetCore Mvc", address: smtpUsername)
        });

        message.To.AddRange(new List<MailboxAddress>()
        {
            new MailboxAddress(name: name, address: email)
        });

        message.Importance = MessageImportance.High;
        message.Subject = subject;
        message.Body = new TextPart(TextFormat.Html)
        {
            Text = htmlMessage
        };

        using var client = new SmtpClient();

        client.Connect("smtp.gmail.com", 465, true);
        client.AuthenticationMechanisms.Remove("XOAUTH2");
        client.Authenticate(smtpUsername, smtpPassword);

        await client.SendAsync(message);

        client.Disconnect(true);
    }
}

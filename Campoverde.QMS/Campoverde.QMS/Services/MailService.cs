using System.Net;
using System.Net.Mail;

namespace Campoverde.QMS.Services;

public class MailService : IMailService
{
    private readonly IConfiguration _configuration;

    public MailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendMailToMany(string[] toaddress, string[] ccaddress, string?[] bccaddress, string subject, string body)
    {
        string from = _configuration["EmailConfiguration:SmtpUsername"];
        string host = _configuration["EmailConfiguration:SmtpServer"];
        var user = _configuration["EmailConfiguration:SmtpUsername"];
        var password = _configuration["EmailConfiguration:SmtpPassword"];
        int smtpPort = Convert.ToInt16(_configuration["EmailConfiguration:SmtpPort"]);

        MailMessage mailMessage = new MailMessage();

        mailMessage.From = new MailAddress(from);

        foreach (string toadd in toaddress)
        {
            if (toadd.Length > 0)
            {
                mailMessage.To.Add(new MailAddress(toadd));
            }
        }

        if (ccaddress != null)
        {
            foreach (string ccadd in ccaddress)
            {
                if (ccadd.Length > 0)
                {
                    mailMessage.CC.Add(ccadd);
                }
            }
        }

        if (bccaddress != null)
        {
            foreach (string bccadd in bccaddress)
            {
                if (bccadd.Length > 0)
                {
                    mailMessage.Bcc.Add(bccadd);
                }
            }
        }

        //Set additional options
        mailMessage.Priority = MailPriority.High;
        //Text/HTML
        mailMessage.IsBodyHtml = true;

        mailMessage.Subject = subject;
        mailMessage.Body = body;

        SmtpClient smtpClient = new SmtpClient();
        smtpClient.Host = host;
        smtpClient.Port = smtpPort;
        //************ Adding Auth************//
        NetworkCredential NetworkCred = new NetworkCredential(user, password);
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = NetworkCred;
        smtpClient.EnableSsl = true;


        try
        {
            // Send the email
            smtpClient.Send(mailMessage);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }

    public void SendMail(string toaddress, string ccaddress, string subject, string body)
    {
        string from = _configuration["EmailConfiguration:SmtpUsername"];
        string host = _configuration["EmailConfiguration:SmtpServer"];
        var user = _configuration["EmailConfiguration:SmtpUsername"];
        var password = _configuration["EmailConfiguration:SmtpPassword"];
        int smtpPort = Convert.ToInt16(_configuration["EmailConfiguration:SmtpPort"]);

        MailMessage mailMessage = new MailMessage();

        mailMessage.From = new MailAddress(from);
        if (toaddress != null)
        {

            mailMessage.To.Add(new MailAddress(toaddress));

        }

        if (ccaddress != null)
        {
            mailMessage.CC.Add(ccaddress);

        }
        //Set additional options
        mailMessage.Priority = MailPriority.High;
        //Text/HTML
        mailMessage.IsBodyHtml = true;

        mailMessage.Subject = subject;
        mailMessage.Body = body;

        SmtpClient smtpClient = new SmtpClient();
        smtpClient.Host = host;
        smtpClient.Port = 587;
        //************ Adding Auth************//
        NetworkCredential NetworkCred = new NetworkCredential(user, password);
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = NetworkCred;
        smtpClient.EnableSsl = true;


        try
        {
            // Send the email
            smtpClient.Send(mailMessage);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }
}


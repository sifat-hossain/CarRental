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

    public void SendMail(string toaddress, string ccaddress, string subject, string name)
    {
        string from = _configuration["EmailConfiguration:SmtpUsername"];
        string host = _configuration["EmailConfiguration:SmtpServer"];
        var user = _configuration["EmailConfiguration:SmtpUsername"];
        var password = _configuration["EmailConfiguration:SmtpPassword"];
        int smtpPort = Convert.ToInt16(_configuration["EmailConfiguration:SmtpPort"]);

        MailMessage mailMessage = new()
        {
            From = new MailAddress(from)
        };
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
        mailMessage.Body = EmailBody(name);

        SmtpClient smtpClient = new()
        {
            Host = host,
            Port = 587
        };
        //************ Adding Auth************//
        NetworkCredential NetworkCred = new(user, password);
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
    private static string EmailBody(string name)
    {
        string bodyText = $@"
<!DOCTYPE html>
<html lang=""""en"""">
<head>
    <meta charset=""""UTF-8"""">
    <meta name=""""viewport"""" content=""""width=device-width, initial-scale=1.0"""">
    <style>
        body {{
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f4f4f4;
        }}
        .email-container {{
            max-width: 600px;
            margin: 0 auto;
            background-color: #ffffff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }}
        .header {{
            background-color: #000000;
            color: #ffffff;
            padding: 10px 20px;
            text-align: center;
            border-top-left-radius: 8px;
            border-top-right-radius: 8px;
        }}
        .content {{
            padding: 20px;
            line-height: 1.6;
            color: #333333;
        }}
        .footer {{
            padding: 10px 20px;
            text-align: center;
            font-size: 12px;
            color: #777777;
        }}
        .button {{
            display: inline-block;
            padding: 10px 20px;
            margin: 20px 0;
            background-color: #007BFF;
            color: #ffffff;
            text-decoration: none;
            border-radius: 5px;
        }}
    </style>
    <title>Welcome to Campoverde Car Hire</title>
</head>
<body>
    <div class=""""email-container"""">
        <div class=""""header"""">
            <h1>Welcome to Campoverde Car Hire</h1>
        </div>
        <div class=""""content"""">
            <p>Hi {{Name of the guy who filled up the form}},</p>
            <p>Thank you for reaching out and for your interest in our Car Hire / Airport Transfers. Your inquiry and the opportunity to provide you with a quote are greatly appreciated.</p>
            <p>I'm currently out of the office, but I'll be sure to review your request as soon as I return. Rest assured, I'll get back to you with the requested quote promptly.</p>
            <p>In the meantime, if you have any urgent matters or further questions, please feel free to contact Marcus at +34 772 67 75 08 or Yorkie at +34 622 42 86 67 or by clicking the WhatsApp link on our website.</p>
            <p>Thank you for your patience and understanding.</p>
            <p>Best regards,</p>
            <p>Campoverde Car Hire</p>
        </div>
        <div class=""""footer"""">
            <p>&copy; 2024 Campoverde Car Hire. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";
        return bodyText;
    }
}


namespace Campoverde.QMS.Interfaces;

public interface IMailService
{
    public void SendMailToMany(string[] toaddress, string[] ccaddress, string[] bccaddress, string subject, string body);
    public void SendMail(string toaddress, string ccaddress, string subject, string body);
}

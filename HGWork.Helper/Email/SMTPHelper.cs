using System.Net;
using System.Net.Mail;

namespace HGWork.Helper.Email
{
    public static class SMTPHelper
    {
        public static bool SendMail(string MailTo, string contentEmail, string Subject, string MailBCC = null)
        {
            string myEmail = "canhbaotruycap2022@gmail.com";
            string myPass = "tyhgfmconntwpmsw";
            MailMessage msg = new MailMessage()
            {
                From = new MailAddress(myEmail),
                Body = contentEmail,
                IsBodyHtml = true,
                Subject = Subject

            };
            msg.To.Add(MailTo);
            if (!string.IsNullOrWhiteSpace(MailBCC)) msg.Bcc.Add(MailBCC);

            SmtpClient client = new SmtpClient()
            {
                //UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(myEmail, myPass),
                Timeout = 20000
            };
            try
            {
                client.Send(msg);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                msg.Dispose();
            }
        }
    }
}

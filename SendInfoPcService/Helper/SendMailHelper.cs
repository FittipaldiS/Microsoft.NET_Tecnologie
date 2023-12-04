using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Imap;
using Org.BouncyCastle.Crypto;
using System.Xml.Linq;
using System.Net.Mail;

namespace SendEmailService.Helper
{
    //To create Fake mail https://ethereal.email/create

    public class SendMailHelper
    {
        /// <summary>
        /// The method sends emails via Smtp
        /// </summary>
        /// <param name="nameFrom">The name address used to send the email</param>
        /// <param name="nameTo">The name address used for those who are to receive the email</param>
        /// <param name="ToEmail">The mail address used for those who are to receive the email</param>
        /// <param name="FromAdresse">The mail address used to send the email</param>
        /// <param name="password">The email password for authentication</param>
        /// <param name="subject">The subject of the email</param>
        /// <param name="bodyText">The text content of the email</param>
        /// <param name="hostServer">The host server to send the mail</param>
        /// <returns></returns>
        public bool SendMailSmtp(string nameFrom, string nameTo, string ToEmail, string FromAdresse, string password, string subject, string bodyText, string hostServer, string attchmentFilePath)
        {

            //How to create a message http://www.mimekit.net/docs/html/Creating-Messages.htm

            var message = new MimeMessage();
            message.Sender = MailboxAddress.Parse(FromAdresse);
            message.From.Add(MailboxAddress.Parse(FromAdresse));
            message.To.Add(MailboxAddress.Parse(ToEmail));
            message.Subject = subject.ToString();

            //message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            //{
            //    Text = bodyText
            //};

            // Set the plain-text version of the message text
            var builder = new BodyBuilder();

            builder.TextBody = bodyText;

            builder.Attachments.Add(attchmentFilePath);

            message.Body = builder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                var IsSend = false;
                //Problem by connect https://www.mailenable.com/kb/content/article.asp?ID=ME020575
                try
                {
                    //ATTENTION:!! Must be deactive the antivirus!!!!!!!!!!
                    TryToConnect(hostServer, client);

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate(FromAdresse, password);

                    client.Send(message);

                    client.Disconnect(true);

                    IsSend = true;
                }
                catch (System.Exception ex)
                {
                    IsSend = false;
                    Console.WriteLine("Exception caught in SendEmail(): {0}",
                    ex.ToString());
                }

                return IsSend;
            }
        }

        private static void TryToConnect(string hostServer, MailKit.Net.Smtp.SmtpClient client)
        {
            try
            {
                client.Connect(hostServer, 587, SecureSocketOptions.StartTls);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GetMailImap(string nameFrom, string nameTo, string ToEmail, string FromAdresse, string password, string subject, string bodyText, string hostServer)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(nameFrom, FromAdresse));
            message.To.Add(new MailboxAddress(nameTo, ToEmail));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = $"{bodyText}"
            };

            using (var client = new ImapClient())
            {
                var IsSend = false;

                try
                {
                    client.Connect(hostServer, 993, true);
                    client.Authenticate(FromAdresse, password);

                    client.Disconnect(true);

                    IsSend = true;
                }
                catch (System.Exception ex)
                {
                    IsSend = false;
                    Console.WriteLine("Exception caught in SendEmail(): {0}",
                    ex.ToString());
                }

                return IsSend;
            }
        }
    }
}

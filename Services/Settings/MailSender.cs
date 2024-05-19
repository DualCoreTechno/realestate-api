using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Services.Settings
{
    public static class MailSender
    {
        public static bool SendEmail(string recipientEmail, string subject, string body, bool isHTML, string? mailCC = null, 
            string? mailBCC = null, string? replyTo = null, byte[]? attachmentFile = null, string? attachmentName = null)
        {
            try
            {
                IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

                string senderEmail = config["SenderMailDetails:MailId"];
                string senderPassword = config["SenderMailDetails:Password"];

                // Instantiate a new instance of MailMessage
                MailMessage mailMessage = new();

                if (!string.IsNullOrEmpty(senderEmail))
                {
                    // Set the sender address of the mail message
                    mailMessage.From = new MailAddress(senderEmail, senderEmail, System.Text.Encoding.UTF8);
                }

                foreach (string semail in recipientEmail.Split(','))
                {
                    if (semail.Trim() != string.Empty)
                        mailMessage.To.Add(semail);
                }

                // Set the subject of the mail message
                mailMessage.Subject = subject;

                // Set the body of the mail message
                mailMessage.Body = body;

                // Set the format of the mail message body as HTML
                mailMessage.IsBodyHtml = isHTML;

                if (mailCC != string.Empty && mailCC != null)
                    mailMessage.CC.Add(mailCC);

                if (mailBCC != string.Empty && mailBCC != null)
                    mailMessage.Bcc.Add(mailBCC);

                if (replyTo != string.Empty && replyTo != null)
                    mailMessage.ReplyToList.Add(replyTo);

                // Set the priority of the mail message to normal
                mailMessage.Priority = MailPriority.Normal;

                // Instantiate a new instance of SmtpClient
                SmtpClient smtpClient = new()
                {
                    // set host name
                    Host = "smtp.office365.com",
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(senderEmail, senderPassword),
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                if (attachmentFile != null)
                {
                    mailMessage.Attachments.Add(new Attachment(new MemoryStream(attachmentFile), attachmentName));
                }

                // Send the mail message
                smtpClient.Send(mailMessage);
                mailMessage.Attachments.Dispose();

                return true;
            }
            catch (Exception e)
            {
                var a = e;
                return false;
            }
        }

        public static void SendEmailNew()
        {
            // Sender's email address and password (for authentication)
            string senderEmail = "renishribadiya10@outlook.com";
            string senderPassword = "Renish@321";

            // Recipient's email address
            string recipientEmail = "Renish.patel11@gmail.com";

            // Create a MailMessage object
            MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail);
            mailMessage.Subject = "Subject of the email";
            mailMessage.Body = "Body of the email";

            // Create a SmtpClient object
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 465; // Use 587 for Gmail
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
            smtpClient.EnableSsl = true; // Enable SSL for secure connection

            try
            {
                // Send the email
                smtpClient.Send(mailMessage);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
    }
}
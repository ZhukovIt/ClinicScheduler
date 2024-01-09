using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace ClinicScheduler
{
    class SendMail
    {
        public string ErrorMessage;
        private string SmtpServer;
        private string FromAddress;
        private string FromPassword;
        private string SenderAddress; 
        private bool EnableSSL;
        private int Port;
        private string MailBodyEncoding;
        private string AttachedFilesDirectory;
        private bool IsBodyHTML = false;

        public SendMail(string pSmtpServer,
                        string pFromAddress,
                        string pFromPassword,
                        string pSenderAddress,
                        bool pEnableSSL,
                        int pPort,
                        string pMailBodyEncoding,
                        bool pIsBodyHTML)
        {
            SmtpServer = pSmtpServer;                   //сервер для отправки почты
            FromAddress = pFromAddress;                 //адрес отправителя
            FromPassword = pFromPassword;               //пароль отправителя (для подключению к SMTP серверу)
            SenderAddress = pSenderAddress;             //адрес отправителя (сообщение об ошибке сюда же)
            EnableSSL = pEnableSSL;                     //использовать SSL
            Port = pPort;                               //использовать порт
            MailBodyEncoding = pMailBodyEncoding;       //адреса, кому отсылать копии
            IsBodyHTML = pIsBodyHTML;
        }


        public bool Send(string ptoAddress, string pCopyToAddress, string pletterCaption, string pletterMessage)
        {
            List<string> Address = new List<string>();
            Address.Add(ptoAddress);
            List<string> CopyAddress = new List<string>();
            CopyAddress.Add(pCopyToAddress);
            return Send(Address, CopyAddress, pletterCaption, pletterMessage, null);
        }

        public bool Send(string ptoAddress, string pCopyToAddress, string pletterCaption, string pletterMessage, List<string> pattachFile)
        {
            List<string> Address = new List<string>();
            Address.Add(ptoAddress);
            List<string> CopyAddress = new List<string>();
            CopyAddress.Add(pCopyToAddress);
            return Send(Address, CopyAddress, pletterCaption, pletterMessage, null);
        }

        public bool Send(List<string> ptoAddresses, List<string> pCopyToAddresses, string pletterCaption, string pletterMessage)
        {
            return Send(ptoAddresses, pCopyToAddresses, pletterCaption, pletterMessage, null);
        }

        public bool Send(List<string> ptoAddresses, List<string> pCopyToAddresses, string pletterCaption, string pletterMessage, List<string> pattachFile)
        {
            MailMessage mail = new MailMessage();
            try
            {
                if (pletterCaption == "" && pletterMessage == "")
                    return false;
                mail.From = new MailAddress(FromAddress);
                if (pCopyToAddresses != null || pCopyToAddresses.Count != 0)
                {
                    for (int i = 0; i < pCopyToAddresses.Count; i++)
                        mail.CC.Add(new MailAddress(pCopyToAddresses[i]));
                }
                if (ptoAddresses != null || ptoAddresses.Count != 0)
                {
                    for (int i = 0; i < ptoAddresses.Count; i++)
                        mail.To.Add(new MailAddress(ptoAddresses[i]));
                }
                mail.Subject = pletterCaption;
                mail.Body = pletterMessage;
                mail.Sender = new MailAddress(SenderAddress);
                mail.Sender = new MailAddress(FromAddress);
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mail.IsBodyHtml = IsBodyHTML;
                mail.BodyEncoding = Encoding.GetEncoding(MailBodyEncoding);
                if (pattachFile != null && pattachFile.Count > 0)
                    for (int i = 0; i < pattachFile.Count; i++)
                        mail.Attachments.Add(new Attachment(pattachFile[i]));
                SmtpClient client = new SmtpClient();

                client.Host = SmtpServer;
                client.Port = Port;
                client.EnableSsl = EnableSSL;
                client.Credentials = new NetworkCredential(FromAddress.Split('@')[0], FromPassword);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);

                ErrorMessage = "";
                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                return false;
            }
            finally
            {
                mail.Dispose();
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using Store.Domain.Abstract;
using Store.Domain.Entities;

namespace Store.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "orders@example.com";
        public string MailFromAddress = "productstore@example.com";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpPassword";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"D:\product_store_emails";
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(ShoppingCart cart, ShippingDetails shippingInfo)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;

                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);
                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("New order processed")
                    .AppendLine("---")
                    .AppendLine("Goods");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Product.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (total: {2:c})", line.Quantity, line.Product.Name, subtotal);
                }

                body.AppendFormat("Total cost: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("---")
                    .AppendLine("delivery:")
                    .AppendLine(shippingInfo.FirstName)
                    .AppendLine(shippingInfo.LastName)
                    .AppendLine(shippingInfo.Email)
                    .AppendLine(shippingInfo.Country)
                    .AppendLine(shippingInfo.City)
                    .AppendLine(shippingInfo.Address ?? "")
                    .AppendLine(shippingInfo.Phone.ToString())
                    .AppendLine(shippingInfo.PostalCode.ToString())
                    .AppendLine("---")
                    .AppendFormat("Gift wrap: {0}", shippingInfo.GiftWrap ? "Yes" : "No");

                MailMessage mailMessage = new MailMessage(emailSettings.MailFromAddress, emailSettings.MailToAddress, "New order sent!", body.ToString());

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                    mailMessage.HeadersEncoding = Encoding.UTF8;
                }

                smtpClient.Send(mailMessage);
            }
        }
    }
}

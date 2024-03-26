using MegaProjekt.Core.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MegaProjekt.Core.Services
{
    public class MailService : IMailService
    {
        private IConfiguration _configuration;


        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("xtheanother@gmail.com", "noreply");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
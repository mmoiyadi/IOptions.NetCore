using Microsoft.Extensions.Options;
using Options.NetCore.Options;
using Options.NetCore.Services.Interfaces;
using System;


namespace Options.NetCore.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private EmailOptions emailOptionsVal;
        private EmailOptions adminEmailOptionsVal;
        
        public EmailService(IOptionsMonitor<EmailOptions> emailOptions)
        {
            emailOptions.OnChange(config => {
                emailOptionsVal = config;
             });
            emailOptionsVal = emailOptions.Get("Email");
            adminEmailOptionsVal = emailOptions.Get("AdminEmail");
            
        }
        public void Send(string report)
        {
            Console.WriteLine($"Sending report titled {emailOptionsVal.Subject} " +
                                $"to {emailOptionsVal.Recepient}");
            throw new Exception("Exception in sending mail");
        }
        public void SendAdmin(string text)
        {
            Console.WriteLine($"Sending mail titled {adminEmailOptionsVal.Subject} " +
                                $"to {adminEmailOptionsVal.Recepient} to the admin");
        }
    }
}

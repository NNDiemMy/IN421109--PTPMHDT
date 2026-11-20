using System;
using EduHub.Application.Services;

namespace EduHub.Infrastructure.Services
{
    public class SmsNotificationService : INotificationService
    {
        public string Channel => "sms";

        public void Send(string message)
        {
            Console.WriteLine($"[SMS Notification] {message}");
        }
    }
}

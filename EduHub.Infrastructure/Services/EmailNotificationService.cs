using System;
using EduHub.Application.Services;

namespace EduHub.Infrastructure.Services
{
    public class EmailNotificationService : INotificationService
    {
        public string Channel => "email";

        public void Send(string message)
        {
            Console.WriteLine($"[Email Notification] {message}");
        }
    }
}

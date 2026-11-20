using System;
using System.Collections.Generic;
using System.Linq;

namespace EduHub.Application.Services
{
    public class NotificationServiceFactory
    {
        private readonly IEnumerable<INotificationService> _services;

        public NotificationServiceFactory(IEnumerable<INotificationService> services)
        {
            _services = services;
        }

        public virtual INotificationService Create(string channel)
        {
            if (string.IsNullOrWhiteSpace(channel))
                throw new ArgumentException("Kênh thông báo không được để trống", nameof(channel));

            var service = _services.FirstOrDefault(s => s.Channel.Equals(channel.Trim(), StringComparison.OrdinalIgnoreCase));
            if (service == null)
            {
                throw new NotSupportedException($"Kênh thông báo '{channel}' không được hỗ trợ");
            }

            return service;
        }
    }
}

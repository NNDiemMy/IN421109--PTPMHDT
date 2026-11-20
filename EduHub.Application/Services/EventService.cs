using System;
using System.Collections.Generic;
using EduHub.Application.Repositories;
using EduHub.Domain.Entities;

namespace EduHub.Application.Services
{
    public class EventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly NotificationServiceFactory _notificationFactory;

        public EventService(IEventRepository eventRepository, NotificationServiceFactory notificationFactory)
        {
            _eventRepository = eventRepository;
            _notificationFactory = notificationFactory;
        }

        public Event CreateEvent(Guid clubId, string title, DateTime date, int capacity, string channel)
        {
            var evt = Event.Create(clubId, title, date, capacity);
            _eventRepository.Add(evt);

            var notifier = _notificationFactory.Create(channel);
            notifier.Send($"Sự kiện mới: {evt.Title}");

            return evt;
        }

        public Event? GetById(Guid id) => _eventRepository.GetById(id);

        public IEnumerable<Event> GetByClubId(Guid clubId) => _eventRepository.GetByClubId(clubId);

        public bool DeleteEvent(Guid id) => _eventRepository.Delete(id);
    }
}

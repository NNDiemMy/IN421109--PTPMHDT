using System;
using System.Collections.Generic;
using EduHub.Application.Repositories;
using EduHub.Domain.Entities;

namespace EduHub.Application.Services
{
    public class EventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly NotificationServiceFactory _notificationServiceFactory;

        public EventService(IEventRepository eventRepository, NotificationServiceFactory notificationServiceFactory)
        {
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
            _notificationServiceFactory = notificationServiceFactory ?? throw new ArgumentNullException(nameof(notificationServiceFactory));
        }

        public Event CreateEvent(Guid clubId, string title, DateTime date, int capacity, string channel)
        {
            var @event = Event.Create(clubId, title, date, capacity);
            _eventRepository.Add(@event);

            var notificationService = _notificationServiceFactory.Create(channel);
            notificationService.Send($"Sự kiện mới: {@event.Title}");

            return @event;
        }

        public Event? GetById(Guid id)
        {
            return _eventRepository.GetById(id);
        }

        public IEnumerable<Event> GetByClubId(Guid clubId)
        {
            return _eventRepository.GetByClubId(clubId);
        }

        public bool DeleteEvent(Guid id)
        {
            return _eventRepository.Delete(id);
        }
    }
}

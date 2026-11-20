using System;
using System.Collections.Generic;
using System.Linq;
using EduHub.Application.Repositories;
using EduHub.Domain.Entities;

namespace EduHub.Infrastructure.Repositories
{
    public class InMemoryEventRepository : IEventRepository
    {
        private readonly List<Event> _events = new();
        private readonly object _lock = new();

        public void Add(Event @event)
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));
            lock (_lock)
            {
                _events.Add(@event);
            }
        }

        public Event? GetById(Guid id)
        {
            lock (_lock)
            {
                return _events.FirstOrDefault(e => e.Id == id);
            }
        }

        public IEnumerable<Event> GetByClubId(Guid clubId)
        {
            lock (_lock)
            {
                return _events.Where(e => e.ClubId == clubId).ToList();
            }
        }

        public bool Delete(Guid id)
        {
            lock (_lock)
            {
                var item = _events.FirstOrDefault(e => e.Id == id);
                if (item == null) return false;
                return _events.Remove(item);
            }
        }
    }
}

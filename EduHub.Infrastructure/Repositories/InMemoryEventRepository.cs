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

        public void Add(Event @event)
        {
            _events.Add(@event);
        }

        public Event? GetById(Guid id)
        {
            return _events.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Event> GetByClubId(Guid clubId)
        {
            return _events.Where(e => e.ClubId == clubId).ToList();
        }

        public bool Delete(Guid id)
        {
            var evt = GetById(id);
            if (evt == null) return false;
            return _events.Remove(evt);
        }
    }
}

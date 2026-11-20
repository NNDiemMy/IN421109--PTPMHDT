using System;
using System.Collections.Generic;
using EduHub.Domain.Entities;

namespace EduHub.Application.Repositories
{
    public interface IEventRepository
    {
        void Add(Event @event);
        Event? GetById(Guid id);
        IEnumerable<Event> GetByClubId(Guid clubId);
        bool Delete(Guid id);
    }
}

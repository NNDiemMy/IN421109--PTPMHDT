using System;

namespace EduHub.Domain.Entities
{
    public class Event
    {
        public Guid Id { get; }
        public Guid ClubId { get; }
        public string Title { get; }
        public DateTime Date { get; }
        public int Capacity { get; }

        private Event(Guid id, Guid clubId, string title, DateTime date, int capacity)
        {
            Id = id;
            ClubId = clubId;
            Title = title;
            Date = date;
            Capacity = capacity;
        }

        public static Event Create(Guid clubId, string title, DateTime date, int capacity)
        {
            if (clubId == Guid.Empty)
                throw new ArgumentException("Mã câu lạc bộ không hợp lệ", nameof(clubId));

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Tiêu đề không được để trống", nameof(title));

            if (capacity <= 0)
                throw new ArgumentException("Sức chứa phải lớn hơn 0", nameof(capacity));

            return new Event(Guid.NewGuid(), clubId, title.Trim(), date, capacity);
        }
    }
}

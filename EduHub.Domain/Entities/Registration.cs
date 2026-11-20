using System;
using EduHub.Domain.Enums;

namespace EduHub.Domain.Entities
{
    public class Registration
    {
        public Guid Id { get; }
        public Guid MemberId { get; }
        public Guid EventId { get; }
        public DateTimeOffset RegisteredAt { get; }
        public RegistrationStatus Status { get; private set; }

        private Registration(Guid id, Guid memberId, Guid eventId)
        {
            Id = id;
            MemberId = memberId;
            EventId = eventId;
            RegisteredAt = DateTimeOffset.UtcNow;
            Status = RegistrationStatus.Pending;
        }

        public static Registration Create(Guid memberId, Guid eventId)
        {
            if (memberId == Guid.Empty)
                throw new ArgumentException("Mã thành viên không hợp lệ", nameof(memberId));
            if (eventId == Guid.Empty)
                throw new ArgumentException("Mã sự kiện không hợp lệ", nameof(eventId));

            return new Registration(Guid.NewGuid(), memberId, eventId);
        }

        public void Confirm()
        {
            if (Status != RegistrationStatus.Pending)
                throw new InvalidOperationException("Chỉ phiếu Pending mới được xác nhận");
            Status = RegistrationStatus.Confirmed;
        }

        public void Cancel()
        {
            if (Status == RegistrationStatus.Cancelled)
                throw new InvalidOperationException("Phiếu đã hủy, không thay đổi được");
            Status = RegistrationStatus.Cancelled;
        }
    }
}

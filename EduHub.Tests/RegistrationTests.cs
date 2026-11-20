using System;
using EduHub.Domain.Entities;
using EduHub.Domain.Enums;
using Xunit;

namespace EduHub.Tests
{
    public class RegistrationTests
    {
        [Fact]
        public void Create_ValidInput_ShouldHavePendingStatus()
        {
            var r = Registration.Create(Guid.NewGuid(), Guid.NewGuid());
            Assert.Equal(RegistrationStatus.Pending, r.Status);
        }

        [Fact]
        public void Confirm_WhenPending_ShouldChangeToConfirmed()
        {
            var r = Registration.Create(Guid.NewGuid(), Guid.NewGuid());
            r.Confirm();
            Assert.Equal(RegistrationStatus.Confirmed, r.Status);
        }

        // Tình huống bắt buộc của đề bài
        [Fact]
        public void Confirm_WhenCancelled_ShouldThrowExceptionAndRemainCancelled()
        {
            var memberId = Guid.NewGuid();
            var eventId = Guid.NewGuid();

            var r = Registration.Create(memberId, eventId);
            r.Cancel();

            Assert.Throws<InvalidOperationException>(() => r.Confirm());
            Assert.Equal(RegistrationStatus.Cancelled, r.Status);
        }

        [Fact]
        public void Cancel_WhenPending_ShouldChangeToCancelled()
        {
            var r = Registration.Create(Guid.NewGuid(), Guid.NewGuid());
            r.Cancel();
            Assert.Equal(RegistrationStatus.Cancelled, r.Status);
        }

        [Fact]
        public void Create_EmptyGuid_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Registration.Create(Guid.Empty, Guid.NewGuid()));
            Assert.Throws<ArgumentException>(() => Registration.Create(Guid.NewGuid(), Guid.Empty));
        }
    }
}

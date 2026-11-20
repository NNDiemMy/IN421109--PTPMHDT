using System;
using EduHub.Domain.Entities;
using EduHub.Domain.Enums;
using Xunit;

namespace EduHub.Tests
{
    public class RegistrationTests
    {
        [Fact]
        public void Create_WithValidData_ShouldInitializeWithPendingStatus()
        {
            // Arrange
            var memberId = Guid.NewGuid();
            var eventId = Guid.NewGuid();

            // Act
            var registration = Registration.Create(memberId, eventId);

            // Assert
            Assert.NotEqual(Guid.Empty, registration.Id);
            Assert.Equal(memberId, registration.MemberId);
            Assert.Equal(eventId, registration.EventId);
            Assert.Equal(RegistrationStatus.Pending, registration.Status);
        }

        [Fact]
        public void Confirm_WhenStatusIsPending_ShouldChangeStatusToConfirmed()
        {
            // Arrange
            var registration = Registration.Create(Guid.NewGuid(), Guid.NewGuid());

            // Act
            registration.Confirm();

            // Assert
            Assert.Equal(RegistrationStatus.Confirmed, registration.Status);
        }

        [Fact]
        public void Confirm_WhenCancelled_ShouldThrowInvalidOperationExceptionAndStatusRemainsCancelled()
        {
            // Arrange
            var memberId = Guid.NewGuid();
            var eventId = Guid.NewGuid();
            var r = Registration.Create(memberId, eventId);
            r.Cancel();

            // Act & Assert (Tình huống bắt buộc của đề bài)
            Assert.Throws<InvalidOperationException>(() => r.Confirm());
            Assert.Equal(RegistrationStatus.Cancelled, r.Status);
        }

        [Fact]
        public void Cancel_WhenPendingOrConfirmed_ShouldSetStatusToCancelled()
        {
            // Arrange & Act
            var r1 = Registration.Create(Guid.NewGuid(), Guid.NewGuid());
            r1.Cancel();

            var r2 = Registration.Create(Guid.NewGuid(), Guid.NewGuid());
            r2.Confirm();
            r2.Cancel();

            // Assert
            Assert.Equal(RegistrationStatus.Cancelled, r1.Status);
            Assert.Equal(RegistrationStatus.Cancelled, r2.Status);
        }

        [Fact]
        public void Create_WithEmptyGuid_ShouldThrowArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => Registration.Create(Guid.Empty, Guid.NewGuid()));
            Assert.Throws<ArgumentException>(() => Registration.Create(Guid.NewGuid(), Guid.Empty));
        }
    }
}

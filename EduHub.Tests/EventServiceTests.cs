using System;
using EduHub.Application.Repositories;
using EduHub.Application.Services;
using EduHub.Domain.Entities;
using Moq;
using Xunit;

namespace EduHub.Tests
{
    public class EventServiceTests
    {
        [Fact]
        public void CreateEvent_WhenDataIsValid_ShouldCallAddAndSendExactlyOnce()
        {
            // Arrange
            var mockRepo = new Mock<IEventRepository>();
            var mockNotificationService = new Mock<INotificationService>();
            mockNotificationService.Setup(n => n.Channel).Returns("email");

            var factory = new NotificationServiceFactory(new[] { mockNotificationService.Object });
            var eventService = new EventService(mockRepo.Object, factory);

            var clubId = Guid.NewGuid();
            var title = "Hội thảo Công nghệ 2026";
            var date = DateTime.UtcNow.AddDays(7);
            var capacity = 150;
            var channel = "email";

            // Act
            var result = eventService.CreateEvent(clubId, title, date, capacity, channel);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(title, result.Title);
            Assert.Equal(clubId, result.ClubId);
            Assert.Equal(capacity, result.Capacity);

            // Kiểm tra Add và Send được gọi đúng 1 lần (theo yêu cầu đề bài)
            mockRepo.Verify(r => r.Add(It.Is<Event>(e => e.Title == title && e.ClubId == clubId)), Times.Once);
            mockNotificationService.Verify(n => n.Send(It.Is<string>(msg => msg.Contains(title))), Times.Once);
        }

        [Fact]
        public void CreateEvent_WhenTitleIsEmpty_ShouldThrowArgumentException()
        {
            // Arrange
            var mockRepo = new Mock<IEventRepository>();
            var mockNotification = new Mock<INotificationService>();
            var factory = new NotificationServiceFactory(new[] { mockNotification.Object });
            var service = new EventService(mockRepo.Object, factory);

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                service.CreateEvent(Guid.NewGuid(), "", DateTime.UtcNow.AddDays(1), 50, "email"));
        }

        [Fact]
        public void CreateEvent_WhenCapacityIsZeroOrNegative_ShouldThrowArgumentException()
        {
            // Arrange
            var mockRepo = new Mock<IEventRepository>();
            var mockNotification = new Mock<INotificationService>();
            var factory = new NotificationServiceFactory(new[] { mockNotification.Object });
            var service = new EventService(mockRepo.Object, factory);

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                service.CreateEvent(Guid.NewGuid(), "Seminar", DateTime.UtcNow.AddDays(1), 0, "email"));
        }
    }
}

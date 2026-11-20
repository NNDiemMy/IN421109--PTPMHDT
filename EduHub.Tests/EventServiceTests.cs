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
        public void CreateEvent_ValidInput_ShouldCallAddAndSendOnce()
        {
            // Arrange
            var mockRepo = new Mock<IEventRepository>();
            var mockNotification = new Mock<INotificationService>();
            mockNotification.Setup(x => x.Channel).Returns("email");

            var factory = new NotificationServiceFactory(new[] { mockNotification.Object });
            var service = new EventService(mockRepo.Object, factory);

            var clubId = Guid.NewGuid();
            var title = "Seminar OOP";
            var date = DateTime.Now.AddDays(2);
            var capacity = 50;

            // Act
            var result = service.CreateEvent(clubId, title, date, capacity, "email");

            // Assert
            Assert.NotNull(result);
            mockRepo.Verify(r => r.Add(It.IsAny<Event>()), Times.Once);
            mockNotification.Verify(n => n.Send(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void CreateEvent_EmptyTitle_ShouldThrowException()
        {
            var mockRepo = new Mock<IEventRepository>();
            var mockNotification = new Mock<INotificationService>();
            var factory = new NotificationServiceFactory(new[] { mockNotification.Object });
            var service = new EventService(mockRepo.Object, factory);

            Assert.Throws<ArgumentException>(() => 
                service.CreateEvent(Guid.NewGuid(), "", DateTime.Now, 10, "email"));
        }
    }
}

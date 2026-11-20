namespace EduHub.Application.Services
{
    public interface INotificationService
    {
        string Channel { get; }
        void Send(string message);
    }
}

namespace Hdd.ToastExampleApp
{
    public interface IToastNotificationService
    {
        void Notify(string message, bool isError);
    }
}

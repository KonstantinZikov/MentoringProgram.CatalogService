namespace BLL.MQ
{
    public interface IRabbitListener : IDisposable
    {
        void Start();

        void Stop();

        void IDisposable.Dispose() => Stop();
    }
}

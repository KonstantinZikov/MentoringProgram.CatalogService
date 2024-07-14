namespace Application.MQ
{
    public class RabbitMessage<TData>
    {
        public required TData Data { get; set; }
    }
}

namespace Subscriber
{
    public interface IMessageHandler<in T>
    {
        void Handle(T message);
    }
}

using System.Threading.Tasks;

namespace Subscriber
{
    public interface IMessageHandler<in T>
    {
        Task Handle(T message);
    }
}

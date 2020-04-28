using System.Threading.Tasks;

namespace ServiceBus.Web.App.Contracts
{
    /// <summary>
    /// Message Service
    /// </summary>
    /// <typeparam name="T">Type of Message</typeparam>
    public interface IMessageService<T> where T:new()
    {
        /// <summary>
        /// Publish the Message to Message Broker
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns>Task</returns>
        Task Publish(T message);
    }
}

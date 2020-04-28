using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ServiceBus.Web.App.Contracts;
using ServiceBus.Web.App.Contracts.Models;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.Web.App.Services
{
    /// <summary>
    /// Message Service to Publish Messages to Topics
    /// </summary>
    /// <typeparam name="T">Type of Mesage</typeparam>
    public class TopicsService<T> : IMessageService<T> where T : new()
    {
        private readonly ServiceBusSettings _serviceBusSettings;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="servicebusSettings"></param>
        public TopicsService(IOptions<ServiceBusSettings> servicebusSettings)
        {
            _serviceBusSettings = servicebusSettings.Value;
        }

        /// <summary>
        /// Publish Message to Topic
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns>Task</returns>
        public async Task Publish(T message)
        {
            var topicsClient = new TopicClient(_serviceBusSettings.ConnectionString, _serviceBusSettings.TopicName);
            string messageBody = JsonConvert.SerializeObject(message);
            var messageBodyBytes = new Message(Encoding.UTF8.GetBytes(messageBody));
            await topicsClient.SendAsync(messageBodyBytes);
            await topicsClient.CloseAsync();
        }
    }
}

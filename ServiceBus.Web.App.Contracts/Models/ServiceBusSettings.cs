namespace ServiceBus.Web.App.Contracts.Models
{
    /// <summary>
    /// Service Bus Settings
    /// </summary>
    public class ServiceBusSettings
    {
        public string ConnectionString { get; set; }

        public string TopicName { get; set; }

        public string Subscription { get; set; }
    }
}

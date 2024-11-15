using Confluent.Kafka;
using FoodDelivery.Kafka.Adapters.Orders.In;
using FoodDelivery.Kafka.Application.Mappers.Orders;
using FoodDelivery.Kafka.Common;
using FoodDelivery.Kafka.Contracts.Application.Mappers.Orders;
using FoodDelivery.Kafka.Contracts.Ports.Orders.In;
using FoodDelivery.Kafka.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodDelivery.Kafka.Register;

/// <summary>
/// Методы расширения для регистрации обработчиков в DI
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрация маппинга для сообщения кафки.
    /// </summary>
    /// <param name="serviceCollection">контейнер.</param>
    /// <param name="configuration">конфигурация</param>
    /// <returns>контейнер с зрегеным маппингом.</returns>
    public static IServiceCollection RegisterKafkaOrderMessagesAndHandlers(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
          serviceCollection
            .AddSingleton<IOrdersMessagesMapper, OrdersMessagesMapper>()
            .AddSingleton<IMessageSerializer, MessageSerializer>();
          
          serviceCollection.Configure<KafkaTopicOptions>(configuration.GetSection(nameof(KafkaTopicOptions)));
          serviceCollection.AddKafkaClient();
          serviceCollection.AddSingleton<IOrderEventsProducer, OrderEventsProducer>();
          serviceCollection.AddSingleton<IOrderEventsConsumer, OrderEventsConsumer>();
          return serviceCollection;
    }
}
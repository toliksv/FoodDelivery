using System.Reflection;
using System.Text;
using Avro.Specific;
using Confluent.Kafka;
using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.Kafka.Application.Headers.Orders;
using FoodDelivery.Kafka.Application.Messages.Orders;
using FoodDelivery.Kafka.Common;
using FoodDelivery.Kafka.Contracts.Application.Mappers.Orders;
using FoodDelivery.Kafka.Contracts.Ports.Orders.In;
using FoodDelivery.Kafka.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static System.Threading.Tasks.ValueTask;

namespace FoodDelivery.Kafka.Adapters.Orders.In;

internal class OrderEventsConsumer : IOrderEventsConsumer
{
    private readonly IConsumer<string, byte[]> _consumer;
    private readonly IOrdersMessagesMapper _ordersMessagesMapper;
    private readonly IMessageSerializer _messageSerializer;
    private readonly ILogger<OrderEventsConsumer> _logger;

    public OrderEventsConsumer(IConsumer<string, byte[]> consumer, IOrdersMessagesMapper ordersMessagesMapper,
        IMessageSerializer messageSerializer, IOptions<KafkaTopicOptions> kafkaTopicOptions, ILogger<OrderEventsConsumer> logger)
    {
        _consumer = consumer ?? throw new ArgumentNullException(nameof(consumer));
        _ordersMessagesMapper = ordersMessagesMapper ?? throw new ArgumentNullException(nameof(ordersMessagesMapper));
        _messageSerializer = messageSerializer ?? throw new ArgumentNullException(nameof(messageSerializer));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        if (string.IsNullOrWhiteSpace(kafkaTopicOptions?.Value.OrderEventsTopic))
        {
            throw new ArgumentNullException(nameof(kafkaTopicOptions));
        }

        _consumer.Subscribe(kafkaTopicOptions.Value.OrderEventsTopic);
    }


    public ValueTask<OrderEventBase> ConsumeOrderEvent(CancellationToken cancellationToken)
    {
        var result = _consumer.Consume(cancellationToken);
        var eventMessage = GetOrderEventMessage(result.Message);
        if (eventMessage is null)
        {
            return FromResult<OrderEventBase>(null);
        }
        
        return eventMessage switch
        {
            CreateOrderMessage createOrderMessage => FromResult<OrderEventBase>(
                _ordersMessagesMapper.Map(createOrderMessage)),
            AddOrderItemMessage addOrderItemMessage => FromResult<OrderEventBase>(
                _ordersMessagesMapper.Map(addOrderItemMessage)),
            ChangeOrderItemQuantityMessage changeOrderItemQuantityMessage => FromResult<OrderEventBase>(
                _ordersMessagesMapper.Map(changeOrderItemQuantityMessage)),
            RemoveOrderItemMessage removeOrderItemMessage => FromResult<OrderEventBase>(
                _ordersMessagesMapper.Map(removeOrderItemMessage)),
            SetOrderStatusMessage setOrderStatusMessage => FromResult<OrderEventBase>(
                _ordersMessagesMapper.Map(setOrderStatusMessage)),
            _ => throw new InvalidOperationException($"Неизвестный тип сообщения {eventMessage.GetType().FullName}"),
        };
    }

    private ISpecificRecord GetOrderEventMessage(Message<string, byte[]> message)
    {
        if (message is null)
        {
            _logger.LogWarning("Консумер не вернул сообщения!");
            return null;
        }
        
        if (!message.Headers.TryGetLastBytes(OrderEventHeadersKeys.OrderEventTypeKey, out var messageTypeBytes))
        {
            _logger.LogWarning("Сообщение с ключом {MessageKey}, заголовках не найден тип сообщения", message.Key);
            return null;
        }

        var messageType = Encoding.UTF8.GetString(messageTypeBytes);
        _logger.LogInformation("Получено сообщение c ключом {MessageKey} типа {MessageType}", message.Key, messageType);
        var type = Type.GetType(messageType, true, true);
        return Map(type, message.Value);
    }

    private ISpecificRecord Map(Type messageType, byte[] messagesBytes)
    {
        var method = typeof(IMessageSerializer).GetMethod(nameof(IMessageSerializer.Deserialize));
        var genericMethod = method.MakeGenericMethod(messageType);
        return (ISpecificRecord)genericMethod.Invoke(_messageSerializer, new object[] { messagesBytes });
    }
}
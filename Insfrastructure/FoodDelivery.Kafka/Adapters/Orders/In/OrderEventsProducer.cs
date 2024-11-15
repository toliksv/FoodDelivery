using System.Text;
using Avro.Specific;
using Confluent.Kafka;
using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.Kafka.Application.Headers.Orders;
using FoodDelivery.Kafka.Common;
using FoodDelivery.Kafka.Contracts.Application.Mappers.Orders;
using FoodDelivery.Kafka.Contracts.Ports.Orders.In;
using FoodDelivery.Kafka.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FoodDelivery.Kafka.Adapters.Orders.In;

internal class OrderEventsProducer : IOrderEventsProducer
{
    private readonly IMessageSerializer _messageSerializer;
    private readonly ILogger<OrderEventsProducer> _logger;
    private readonly string _orderEventsTopic;
    private readonly IOrdersMessagesMapper _ordersMessagesMapper;
    private readonly IProducer<string, byte[]> _producer;

    public OrderEventsProducer(IMessageSerializer messageSerializer, IOrdersMessagesMapper ordersMessagesMapper,
        IProducer<string, byte[]> producer, IOptions<KafkaTopicOptions> orderEventTopic, ILogger<OrderEventsProducer> logger)
    {
        _messageSerializer = messageSerializer ?? throw new ArgumentNullException(nameof(messageSerializer));
        _ordersMessagesMapper = ordersMessagesMapper ?? throw new ArgumentNullException(nameof(ordersMessagesMapper));
        _producer = producer ?? throw new ArgumentNullException(nameof(producer));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _orderEventsTopic = orderEventTopic?.Value.OrderEventsTopic ?? throw new ArgumentNullException(nameof(orderEventTopic));
        if (string.IsNullOrWhiteSpace(_orderEventsTopic))
        {
            throw new ArgumentNullException(nameof(_orderEventsTopic));
        }
    }

    public async Task Produce<TOrderEvent>(TOrderEvent orderEvent, CancellationToken cancellationToken)
        where TOrderEvent : OrderEventBase
    {
        if (orderEvent == null) throw new ArgumentNullException(nameof(orderEvent));
        var message = GetOrderMessage(orderEvent);
        var serialized = _messageSerializer.Serialize(message);
        var result = await _producer.ProduceAsync(_orderEventsTopic, new Message<string, byte[]>
        {
            Key = orderEvent.ClientId.ToString(),
            Value = serialized,
            Headers = new Headers()
            {
                { OrderEventHeadersKeys.OrderEventTypeKey, Encoding.UTF8.GetBytes(message.GetType().AssemblyQualifiedName ?? string.Empty) },
            }
        }, cancellationToken);
        _logger.LogInformation("Отправлено сообщение {@OrderEvent}", orderEvent);
    }

    private ISpecificRecord GetOrderMessage<TOrderEvent>(TOrderEvent orderEvent)
        where TOrderEvent : OrderEventBase
    {
        return orderEvent switch
        {
            CreateOrderEvent createOrderEvent => _ordersMessagesMapper.Map(createOrderEvent),
            AddOrderItemEvent addOrderItemEvent => _ordersMessagesMapper.Map(addOrderItemEvent),
            ChangeOrderItemQuantityEvent changeOrderItemQuantityEvent => _ordersMessagesMapper.Map(
                changeOrderItemQuantityEvent),
            RemoveOrderItemEvent removeOrderItemEvent => _ordersMessagesMapper.Map(removeOrderItemEvent),
            SetOrderStatusEvent setOrderStatusEvent => _ordersMessagesMapper.Map(setOrderStatusEvent),
            _ => throw new InvalidOperationException($"Неизвестный тип {orderEvent.GetType().FullName} события")
        };
    }
}
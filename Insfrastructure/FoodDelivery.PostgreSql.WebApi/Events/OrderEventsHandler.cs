using Confluent.Kafka;
using FoodDelivery.Core.Contracts.Ports.Orders.Out;
using FoodDelivery.Kafka.Common;
using FoodDelivery.Kafka.Contracts.Application.Mappers.Orders;
using FoodDelivery.Kafka.Contracts.Ports.Orders.In;
using FoodDelivery.Kafka.Options;
using Microsoft.Extensions.Options;

namespace FoodDelivery.PostgreSql.WebApi.Events;

/// <summary>
/// Обработчик сообщений заказов.
/// </summary>
internal class OrderEventsHandler : BackgroundService
{
    private readonly IOrderEventsConsumer _orderEventsConsumer;
    private readonly IOrdersEventsSourceRepository _ordersEventsSourceRepository;
    private readonly ILogger<OrderEventsHandler> _logger;

    public OrderEventsHandler(IOrderEventsConsumer orderEventsConsumer,
        IOrdersEventsSourceRepository ordersEventsSourceRepository, ILogger<OrderEventsHandler> logger)
    {
        _orderEventsConsumer = orderEventsConsumer ?? throw new ArgumentNullException(nameof(orderEventsConsumer));
        _ordersEventsSourceRepository = ordersEventsSourceRepository ??
                                        throw new ArgumentNullException(nameof(ordersEventsSourceRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var orderEvent = await _orderEventsConsumer.ConsumeOrderEvent(stoppingToken);
                await _ordersEventsSourceRepository.SaveEvent(orderEvent, stoppingToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Произошла ошибка при получении сообщения");
            }
        }
    }
}
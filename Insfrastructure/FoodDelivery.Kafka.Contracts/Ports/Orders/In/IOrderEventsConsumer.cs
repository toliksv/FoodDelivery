using FoodDelivery.Core.Contracts.Application.Events.Orders;

namespace FoodDelivery.Kafka.Contracts.Ports.Orders.In;

/// <summary>
/// Получение событий заказа из очереди kafka.
/// </summary>
public interface IOrderEventsConsumer
{
    /// <summary>
    /// Получает событие из очереди и приводит его к базовому.
    /// </summary>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns>полученное событие <see cref="OrderEventBase"/>.</returns>
    ValueTask<OrderEventBase> ConsumeOrderEvent(CancellationToken cancellationToken);
}
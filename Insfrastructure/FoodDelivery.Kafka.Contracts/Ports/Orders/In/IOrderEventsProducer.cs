using FoodDelivery.Core.Contracts.Application.Events.Orders;

namespace FoodDelivery.Kafka.Contracts.Ports.Orders.In;

/// <summary>
/// Отправка событий по заказу в кафка.
/// </summary>
public interface IOrderEventsProducer
{
    /// <summary>
    /// ОТправка события закза в Kafka.
    /// </summary>
    /// <param name="orderEvent">событие заказа.</param>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <typeparam name="TOrderEvent">тип события.</typeparam>
    /// <returns>ожидание отправки.</returns>
    Task Produce<TOrderEvent>(TOrderEvent orderEvent, CancellationToken cancellationToken)
        where TOrderEvent : OrderEventBase;
}
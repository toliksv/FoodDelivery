using System;
using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Orders;

namespace FoodDelivery.Core.Contracts.Ports.Orders.Out;

/// <summary>
/// Работа с хранением заказов
/// </summary>
public interface IOrdersEventsSourceRepository
{
    /// <summary>
    /// Сохранение события.
    /// </summary>
    /// <param name="orderEventBase">сохраняемое событие.</param>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns>ожидание сохранения события.</returns>
    Task SaveEvent<TOrderEvent>(TOrderEvent orderEventBase, CancellationToken cancellationToken)
        where TOrderEvent : OrderEventBase;

    /// <summary>
    /// Получить события по заказу.
    /// </summary>
    /// <param name="orderId">идентификатор заказа.</param>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns>список событий по заказу.</returns>
    Task<List<OrderEventBase>> GetOrderEvents(int orderId, CancellationToken cancellationToken);   

    /// <summary>
    /// Получить все заказы клиента.
    /// </summary>
    /// <param name="clientId">идентификатор клиента.</param>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns>список событий от клиента.</returns>
    Task<List<OrderEventBase>> GetClientEvents(int clientId, CancellationToken cancellationToken);
}

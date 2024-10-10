using System;
using RestEase;
using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.Core.WebApi.Application.Queries.Orders.Web;

namespace FoodDelivery.Core.WebApi.Ports.Orders.Out;

/// <summary>
/// Интерфейс к контролеру PostgreSql.WebApi.
/// </summary>
[BasePath("api/postgre-sql-adapter/orders/events")]
public interface IOrderStorageController
{
    /// <summary>
    /// Запись события создания заказа.
    /// </summary>
    /// <param name="createOrderEvent">событие создания заказа.</param>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns>ожидание выполнения</returns>
    [Post("write/create")]
    Task WriteCreateOrderEvent([Body]CreateOrderEvent createOrderEvent, CancellationToken cancellationToken);

    /// <summary>
    /// Запись события установки статуса заказу.
    /// </summary>
    /// <param name="setOrderStatusEvent">событие установки статуса.</param>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns>ожидание выполнения</returns>
    [Put("write/set-status")]
    Task WriteSetOrderStatusEvent([Body]SetOrderStatusEvent setOrderStatusEvent, CancellationToken cancellationToken);

    /// <summary>
    /// Запись добавление блюда в заказ.
    /// </summary>
    /// <param name="addOrderItemEvent">событие добавления блюда.</param>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns>ожидание выполнения.</returns>
    [Post("write/add-order-item")]
    Task WriteAddOrderItemEvent([Body]AddOrderItemEvent addOrderItemEvent, CancellationToken cancellationToken);

    /// <summary>
    /// Запись события удаления блюда из меню
    /// </summary>
    /// <param name="addOrderItemEvent">событие удаления блюда из меню.</param>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns>ожидание выполнения</returns>
    [Delete("write/remove-order-item")]
    Task WriteRemoveOrderItemEvent([Body]RemoveOrderItemEvent addOrderItemEvent, CancellationToken cancellationToken);

    /// <summary>
    /// Запись события изменения количества блюда в заказе.
    /// </summary>
    /// <param name="addOrderItemEvent">событие изменения количества блюда.</param>
    /// <param name="cancellationToken">токе отмены</param>
    /// <returns>ожидание выполнения</returns>
    [Put("write/change-order-item-quantity")]
    public Task WriteChangeOrderItemQuantityEvent([Body]ChangeOrderItemQuantityEvent addOrderItemEvent, CancellationToken cancellationToken);

    /// <summary>
    /// Получение событий по заказу.
    /// </summary>
    /// <param name="orderId">идентификатор заказа.</param>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns>список заказов.</returns>
    [Get("query/by-order-id/{orderId}")]
    public Task<OrderEventsQueryStorageResponse> GetOrderEventsByOrderId([Path("orderId")]int orderId, CancellationToken cancellationToken);
            
    /// <summary>
    /// Получение событий по клиенту.
    /// </summary>
    /// <param name="clientId">Идентификатор клиента.</param>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns>список заказов.</returns>
    [Get("query/by-client-id/{clientId}")]
    public Task<OrderEventsQueryStorageResponse> GetOrderEventsByClientId([Path("clientId")]int clientId, CancellationToken cancellationToken);
}

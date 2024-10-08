using System;
using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.Core.Contracts.Application.Queries.Orders.Web;
using FoodDelivery.Core.WebApi.Ports.Orders.Out;


namespace FoodDelivery.Core.WebApi.Ports.Orders.In;

/// <summary>
/// Обертка <see cref="IOrderStorageController" /> для ValueValueTask.
/// </summary>
public interface IOrderStorageControllerWrapper
{
        /// <summary>
    /// Запись события создания заказа.
    /// </summary>
    /// <param name="createOrderEvent">событие создания заказа.</param>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns>ожидание выполнения</returns>    
    ValueTask WriteCreateOrderEvent(CreateOrderEvent createOrderEvent, CancellationToken cancellationToken);

    /// <summary>
    /// Запись события установки статуса заказу.
    /// </summary>
    /// <param name="setOrderStatusEvent">событие установки статуса.</param>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns>ожидание выполнения</returns>    
    ValueTask WriteSetOrderStatusEvent(SetOrderStatusEvent setOrderStatusEvent, CancellationToken cancellationToken);

    /// <summary>
    /// Запись добавление блюда в заказ.
    /// </summary>
    /// <param name="addOrderItemEvent">событие добавления блюда.</param>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns>ожидание выполнения.</returns>    
    ValueTask WriteAddOrderItemEvent(AddOrderItemEvent addOrderItemEvent, CancellationToken cancellationToken);

    /// <summary>
    /// Запись события удаления блюда из меню
    /// </summary>
    /// <param name="addOrderItemEvent">событие удаления блюда из меню.</param>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns>ожидание выполнения</returns>    
    ValueTask WriteRemoveOrderItemEvent(RemoveOrderItemEvent addOrderItemEvent, CancellationToken cancellationToken);

    /// <summary>
    /// Запись события изменения количества блюда в заказе.
    /// </summary>
    /// <param name="addOrderItemEvent">событие изменения количества блюда.</param>
    /// <param name="cancellationToken">токе отмены</param>
    /// <returns>ожидание выполнения</returns>    
    public ValueTask WriteChangeOrderItemQuantityEvent(ChangeOrderItemQuantityEvent addOrderItemEvent, CancellationToken cancellationToken);

    /// <summary>
    /// Получение событий по заказу.
    /// </summary>
    /// <param name="orderId">идентификатор заказа.</param>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns>список заказов.</returns>    
    public ValueTask<OrderEventsQueryStorageResponse> GetOrderEventsByOrderId(int orderId, CancellationToken cancellationToken);
            
    /// <summary>
    /// Получение событий по клиенту.
    /// </summary>
    /// <param name="clientId">Идентификатор клиента.</param>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns>список заказов.</returns>    
    public ValueTask<OrderEventsQueryStorageResponse> GetOrderEventsByClientId(int clientId, CancellationToken cancellationToken);
}

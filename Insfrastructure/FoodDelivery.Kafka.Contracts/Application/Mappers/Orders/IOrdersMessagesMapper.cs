using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.Kafka.Application.Messages.Orders;

namespace FoodDelivery.Kafka.Contracts.Application.Mappers.Orders;

/// <summary>
///     Маппиинг между событиями закзаов и сообщениями кафки.
/// </summary>
public interface IOrdersMessagesMapper
{
    /// <summary>
    ///     Маппинг события добавления блюда в заказ на сообщение добавления блюда в заказ.
    /// </summary>
    /// <param name="source">
    ///     <see cref="AddOrderItemEvent" />
    /// </param>
    /// <returns>сообщение</returns>
    AddOrderItemMessage Map(AddOrderItemEvent source);

    /// <summary>
    ///     Маппинг сообщения добавления блюда в заказ на событие добавления блюда в заказ.
    /// </summary>
    /// <param name="source">сообщение.</param>
    /// <returns>
    ///     <see cref="AddOrderItemEvent" />
    /// </returns>
    AddOrderItemEvent Map(AddOrderItemMessage source);

    /// <summary>
    ///     Маппинг события изменения количества блюда в заказе, на сообщение изменения количества блюда в заказе.
    /// </summary>
    /// <param name="source">
    ///     <see cref="ChangeOrderItemQuantityEvent" />
    /// </param>
    /// <returns>сообщение.</returns>
    ChangeOrderItemQuantityMessage Map(ChangeOrderItemQuantityEvent source);

    /// <summary>
    ///     Маппинг сообщения изменения количества блюда в заказе, на событие изменения количества блюда в заказе.
    /// </summary>
    /// <param name="source">сообщение.</param>
    /// <returns>
    ///     <see cref="ChangeOrderItemQuantityEvent" />
    /// </returns>
    ChangeOrderItemQuantityEvent Map(ChangeOrderItemQuantityMessage source);

    /// <summary>
    ///     Маппинг события создания заказа на сообщение создания заказа.
    /// </summary>
    /// <param name="source">
    ///     <see cref="CreateOrderEvent" />
    /// </param>
    /// <returns>сообщение.</returns>
    CreateOrderMessage Map(CreateOrderEvent source);

    /// <summary>
    ///     Маппинг сообщения создания заказа на событие создания заказа.
    /// </summary>
    /// <param name="source"></param>
    /// <returns>
    ///     <see cref="CreateOrderEvent" />
    /// </returns>
    CreateOrderEvent Map(CreateOrderMessage source);

    /// <summary>
    ///     Маппинг события удаления блюда в заказе на сообщение удаления блюда в заказе.
    /// </summary>
    /// <param name="source">
    ///     <see cref="RemoveOrderItemEvent" />
    /// </param>
    /// <returns>сообщение.</returns>
    RemoveOrderItemMessage Map(RemoveOrderItemEvent source);

    /// <summary>
    ///     Маппинг сообщения удаления блюда в заказе на событие удаления блюда в заказе.
    /// </summary>
    /// <param name="source">сообщение.</param>
    /// <returns>
    ///     <see cref="RemoveOrderItemEvent" />
    /// </returns>
    RemoveOrderItemEvent Map(RemoveOrderItemMessage source);

    /// <summary>
    ///     Маппинг события установки статуса заказу на сообщение установки статуса заказу.
    /// </summary>
    /// <param name="source">
    ///     <see cref="SetOrderStatusEvent" />
    /// </param>
    /// <returns>сообщение.</returns>
    SetOrderStatusMessage Map(SetOrderStatusEvent source);

    /// <summary>
    ///     Маппинг сообщения установки статуса заказу на событие установки статуса заказу.
    /// </summary>
    /// <param name="source">сообщение.</param>
    /// <returns>
    ///     <see cref="SetOrderStatusEvent" />
    /// </returns>
    SetOrderStatusEvent Map(SetOrderStatusMessage source);
}
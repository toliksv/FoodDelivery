using System;

namespace FoodDelivery.Core.Contracts.Application.Events.Orders;

/// <summary>
/// Событие удаления блюда из заказа.
/// </summary>
public class RemoveOrderItemEvent : OrderEventBase
{
    /// <summary>
    /// Идентификатор блюда в заказе.
    /// </summary>
    public int MenuItemId { get; set; }
}

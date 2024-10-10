using System;

namespace FoodDelivery.Core.Contracts.Application.Events.Orders;

/// <summary>
/// Добавление блюда в заказ.
/// </summary>
public class AddOrderItemEvent : OrderEventBase
{
    /// <summary>
    /// Идентификатор блюда в меню.
    /// </summary>
    public int MenuItemId  { get; set; }

    /// <summary>
    /// Количество блюд в заказе.
    /// </summary>
    public int Quantity { get; set; }
}

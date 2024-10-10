using System;

namespace FoodDelivery.Core.Contracts.Application.Events.Orders;

/// <summary>
/// Изменение количества заказанного блюда. 
/// </summary>
public class ChangeOrderItemQuantityEvent : OrderEventBase
{
    /// <summary>
    /// Идентификатор блюда.
    /// </summary>
    public int MenuItemId { get; set; }

    /// <summary>
    /// Новое количество данного блюда в заказе.
    /// </summary>
    public int NewQuantity { get; set; }
}

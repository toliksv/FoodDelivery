using System;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Orders;

namespace FoodDelivery.Core.Contracts.Application.Events.Orders;

/// <summary>
/// Событие установки статуса заказу.
/// </summary>
public class SetOrderStatusEvent : OrderEventBase
{
    /// <summary>
    /// Новый статус заказа.
    /// </summary>
    public OrderStatus NewStatus { get; set; }
}

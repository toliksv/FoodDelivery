using System;
using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.PostgreSql.Application.Models.Orders;

namespace FoodDelivery.PostgreSql.Extensions;

/// <summary>
/// Методы расширения для событий.
/// </summary>
internal static class OrderEventExtensions
{
    /// <summary>
    /// Получение <see cref="OrderEventType"/> от события.
    /// </summary>
    /// <param name="orderEvent">событие.</param>
    /// <returns>значение перечисления <see cref="OrderEventType"/>.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static OrderEventType GetOrderEventType(this OrderEventBase orderEvent)
        => orderEvent switch 
        {
            CreateOrderEvent => OrderEventType.CreatingOrder,
            SetOrderStatusEvent => OrderEventType.SettingStatusToOrder,
            AddOrderItemEvent => OrderEventType.AddingOrderItem,
            RemoveOrderItemEvent => OrderEventType.RemovingOrderItem,
            ChangeOrderItemQuantityEvent => OrderEventType.ChangingOrderItemQuantity,
            _ => throw new InvalidOperationException("Неизвестный тип события"),
        };
}

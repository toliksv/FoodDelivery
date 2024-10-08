using System;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Orders;
using LinqToDB.Mapping;

namespace FoodDelivery.PostgreSql.Application.Models.Orders;

/// <summary>
/// JSON представление для расширяющихся свойств события.
/// </summary>
public class OrderEventData
{   
    /// <summary>
    /// Новый статус заказа.
    /// </summary>
    public OrderStatus? NewOrderStatus { get; set;}

    /// <summary>
    /// Идентификатор блюда в меню.
    /// </summary>
    public int? MenuItemId  { get; set; }

    /// <summary>
    /// Количество блюд в заказе.
    /// </summary>
    public int? Quantity { get; set; }

    /// <summary>
    /// Новое количество данного блюда в заказе.
    /// </summary>
    public int? NewQuantity { get; set;}
}

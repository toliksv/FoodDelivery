using System;

namespace FoodDelivery.Core.Contracts.Domain.Aggregates.Orders;

public class Order
{
    /// <summary>
    /// Идентификатор заказа.
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Идентификатор клиента.
    /// </summary>
    public int ClientId { get; set; }            

    /// <summary>
    /// Состав заказа.
    /// </summary>
    public List<OrderItem> Items { get; set; }= new List<OrderItem>();

    /// <summary>
    /// Статус заказа.
    /// </summary>
    public OrderStatus Status { get; set;}
}

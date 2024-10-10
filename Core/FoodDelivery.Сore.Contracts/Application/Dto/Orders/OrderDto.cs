using System;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Orders;
using FoodDelivery.Core.Contracts.Application.Commands.Orders.Web;

namespace FoodDelivery.Core.Contracts.Application.Dto.Orders;

public class OrderDto
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
   /// Статус заказа.
   /// </summary>
    public OrderStatus OrderStatus { get; set;}

    /// <summary>
    /// Блюда в заказе
    /// </summary>
    public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();

}

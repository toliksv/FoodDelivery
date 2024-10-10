using System;
using FoodDelivery.Core.Contracts.Application.Dto.Orders;

namespace FoodDelivery.Core.Contracts.Application.Commands.Orders.Web;

/// <summary>
/// Web представления команды создания заказа.
/// </summary>
public class CreateOrderCommandRequest : OrderCommandRequestBase
{
    /// <summary>
    /// Пункты меню.
    /// </summary>
    public List<OrderItemDto> Items { get; set;}
}

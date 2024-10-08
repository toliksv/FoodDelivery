using System;
using FoodDelivery.Core.Contracts.Application.Dto.Orders;

namespace FoodDelivery.Core.Contracts.Application.Commands.Orders.Web;

/// <summary>
/// WEB представление для добавления блюд в заказ.
/// </summary>
public class AddItemsToOrderCommandRequest : OrderUpdateCommandRequestBase
{
    /// <summary>
    /// Позиции для добавления заказа.
    /// </summary>
    public List<OrderItemDto> OrderItems { get; set; }
}

using System;
using FoodDelivery.Core.Contracts.Application.Dto.Orders;

namespace FoodDelivery.Core.Contracts.Application.Commands.Orders.Web;

/// <summary>
/// WEB представление команды изменения количества блюда в заказе.
/// </summary>
public class ChangeOrderItemQuantityCommandRequest : OrderUpdateCommandRequestBase
{
    /// <summary>
    /// Позиции с новыми значениями количества.
    /// </summary>
    public List<OrderItemDto> ItemsWithNewQuantity { get; set; }
}

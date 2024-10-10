using System;

namespace FoodDelivery.Core.Contracts.Application.Commands.Orders.Web;

/// <summary>
/// WEB представление команды удаления блюда из заказа.
/// </summary>
public class RemoveItemsFromOrderCommandRequest : OrderUpdateCommandRequestBase
{
    /// <summary>
    /// Идентификаторы блюд к удалению.
    /// </summary>
    public List<int> MenuItemsIds { get; set;}
}

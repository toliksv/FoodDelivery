using System;

namespace FoodDelivery.Core.Contracts.Application.Commands.Orders;

/// <summary>
/// Удалить позиции из заказа.
/// </summary>
public class RemoveItemsFromOrderCommand
{
    public RemoveItemsFromOrderCommand(int orderId, int clientId, List<int> menuItemsIds)
    {
        OrderId = orderId;
        ClientId = clientId;
        MenuItemsIds = menuItemsIds ?? throw new ArgumentNullException(nameof(menuItemsIds));
    }

    /// <summary>
    /// Идентификатор заказа.
    /// </summary>
    public int OrderId { get; }    

    /// <summary>
    /// Идентификатор клиента.
    /// </summary>
    public int ClientId { get; }

    /// <summary>
    /// Идентификаторы блюд к удалению.
    /// </summary>
    public List<int> MenuItemsIds { get; }
}

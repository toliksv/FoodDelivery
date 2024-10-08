using System;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Orders;

namespace FoodDelivery.Core.Contracts.Application.Commands.Orders;

/// <summary>
/// Изменения количества позиции в заказе.
/// </summary>
public class ChangeOrderItemQuantityCommand
{
    public ChangeOrderItemQuantityCommand(int orderId, int clientId, List<OrderItem> itemsWithNewQuantity)
    {
        OrderId = orderId;
        ClientId = clientId;
        ItemsWithNewQuantity = itemsWithNewQuantity ?? throw new ArgumentNullException(nameof(itemsWithNewQuantity));
    }

    /// <summary>
    /// Идентификатор заказа.
    /// </summary>
    public int OrderId { get;}   

    /// <summary>
    /// Идентификатор клиента.
    /// </summary>
    public int ClientId { get; }

    /// <summary>
    /// Позиции с новыми значениями количества.
    /// </summary>
    public List<OrderItem> ItemsWithNewQuantity { get;}
}

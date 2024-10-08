using System;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Orders;

namespace FoodDelivery.Core.Contracts.Application.Commands.Orders;

/// <summary>
///  Команда добавления позиций в заказ.
/// </summary>
public class AddItemsToOrderCommand
{    
    public AddItemsToOrderCommand(int orderId, int clientId, List<OrderItem> orderItems)
    {
        if (orderItems is null)
        {
            throw new ArgumentNullException(nameof(orderItems));
        }

        OrderId = orderId;
        ClientId = clientId;
        Items = orderItems;
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
    /// Позиции для добавления в заказ.
    /// </summary>
    public List<OrderItem> Items { get; }
}

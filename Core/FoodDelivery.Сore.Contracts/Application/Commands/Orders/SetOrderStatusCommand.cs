using System;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Orders;

namespace FoodDelivery.Core.Contracts.Application.Commands.Orders;

/// <summary>
/// Команда обновления заказа.
/// </summary>
public class SetOrderStatusCommand
{
    public SetOrderStatusCommand(int orderId, int clientId, OrderStatus status)
    {
        OrderId = orderId;
        ClientId = clientId;

        if (status == OrderStatus.Undefined || !Enum.IsDefined<OrderStatus>(status))
        {
            throw new ArgumentException("Неизвестный статус заказа");
        }

        Status = status;
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
    /// Статус заказа.
    /// </summary>
    public OrderStatus Status { get; }    
}

using System;
using System.Data;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Orders;

namespace FoodDelivery.Core.Contracts.Application.Commands.Orders;

/// <summary>
/// Создание заказа.
/// </summary>
public class CreateOrderCommand
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="clientId">идентификатор клиента.</param>
    /// <param name="orderItems">пункты заказа.</param>
    public CreateOrderCommand(int clientId, List<OrderItem> orderItems)
    {
        ClientId = clientId;
        Items = orderItems;
    }

    /// <summary>
    /// Идентификатор клиента.
    /// </summary>
    public int ClientId { get; }

    /// <summary>
    /// 
    /// </summary>
    public List<OrderItem> Items { get;}
}

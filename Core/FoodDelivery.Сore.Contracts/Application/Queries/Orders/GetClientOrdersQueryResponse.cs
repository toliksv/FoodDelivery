using System;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Orders;

namespace FoodDelivery.Core.Contracts.Application.Queries.Orders;

/// <summary>
/// Ответ на запрос всех заказов клиента.
/// </summary>
public class GetClientOrdersQueryResponse
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="orders">список заказов.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public GetClientOrdersQueryResponse(List<Order> orders)
    {
        Orders = orders ?? throw new ArgumentNullException(nameof(orders));
    }

    /// <summary>
    /// Заказы клиента.
    /// </summary>
    public List<Order> Orders { get; }
}

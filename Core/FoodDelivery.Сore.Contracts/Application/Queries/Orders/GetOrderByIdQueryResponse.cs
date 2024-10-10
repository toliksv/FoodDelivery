using System;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Orders;

namespace FoodDelivery.Core.Contracts.Application.Queries.Orders;

/// <summary>
/// Получение заказа по идентификатору.
/// </summary>
public class GetOrderByIdQueryResponse
{
    public GetOrderByIdQueryResponse(Order order)
    {
        Order = order ?? throw new ArgumentNullException(nameof(order));
    }

    /// <summary>
    /// Заказ.
    /// </summary>
    public Order Order { get;}    
}
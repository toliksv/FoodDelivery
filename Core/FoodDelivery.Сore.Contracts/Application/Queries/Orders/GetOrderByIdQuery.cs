using System;

namespace FoodDelivery.Core.Contracts.Application.Queries.Orders;

/// <summary>
/// Запрос получения заказа по id
/// </summary>
public class GetOrderByIdQuery
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="orderId">идентификатор заказа.</param>
    public GetOrderByIdQuery(int orderId) 
    {
        OrderId = orderId;   
    }

    /// <summary>
    /// Идентификатор заказа.
    /// </summary>
    public int OrderId { get; }
}

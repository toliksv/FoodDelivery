using System;
using FoodDelivery.Core.Contracts.Application.Dto.Orders;

namespace FoodDelivery.Core.Contracts.Application.Queries.Orders.Web;

/// <summary>
/// Ответ на запрос заказа по идентификатору.
/// </summary>
public class GetOrderByIdResponse
{
    /// <summary>
    /// Заказ.
    /// </summary>
    public OrderDto Order { get; set; }
}

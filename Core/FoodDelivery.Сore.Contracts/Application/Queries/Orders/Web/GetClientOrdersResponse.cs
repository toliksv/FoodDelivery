using System;
using FoodDelivery.Core.Contracts.Application.Dto.Orders;

namespace FoodDelivery.Core.Contracts.Application.Queries.Orders.Web;

/// <summary>
/// Ответ на запрос заказов клиента.
/// </summary>
public class GetClientOrdersResponse
{
    /// <summary>
    /// клиентские заказы.
    /// </summary>
    public List<OrderDto> ClientOrders { get; set;}
}

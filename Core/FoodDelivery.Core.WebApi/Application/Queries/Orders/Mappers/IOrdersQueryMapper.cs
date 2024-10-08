using System;
using FoodDelivery.Core.Contracts.Application.Queries.Orders;
using FoodDelivery.Core.Contracts.Application.Queries.Orders.Web;
using FoodDelivery.Core.Contracts.Ports.Orders.In;

namespace FoodDelivery.Core.WebApi.Application.Queries.Orders.Mappers;

/// <summary>
/// Маппинг ответов от обработчика <see cref="IOrdersQueriesHandler" /> на web ответ.
/// </summary>
public interface IOrdersQueryMapper
{
    /// <summary>
    /// Маппинг ответа обработчика по запросу заказа по id на web ответ.
    /// </summary>
    /// <param name="source"><see cref="GetOrderByIdQueryResponse" /></param>
    /// <returns><see cref="GetOrderByIdResponse"/></returns>
    GetOrderByIdResponse Map(GetOrderByIdQueryResponse source);

    /// <summary>
    /// Маппинг ответа обработчика по запросу заказов клиента на web ответ.
    /// </summary>
    /// <param name="source"><see cref="GetClientOrdersQueryResponse" /></param>
    /// <returns><see cref="GetClientOrdersResponse"/></returns>
    GetClientOrdersResponse Map(GetClientOrdersQueryResponse source);
}

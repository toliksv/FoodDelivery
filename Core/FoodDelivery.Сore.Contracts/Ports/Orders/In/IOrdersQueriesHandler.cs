using System;
using FoodDelivery.Core.Contracts.Application.Queries.Orders;

namespace FoodDelivery.Core.Contracts.Ports.Orders.In;

/// <summary>
/// Обработчик запросов по заказу.
/// </summary>
public interface IOrdersQueriesHandler
{
    /// <summary>
    /// Обработка запроса на получение заказов клиента.
    /// </summary>
    /// <param name="query">запрос на получение всех заказов клиента.</param>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns>ответ запроса.</returns>
    Task<GetClientOrdersQueryResponse> Handle(GetClientOrdersQuery query, CancellationToken cancellationToken);

    /// <summary>
    /// Обработчик запроса на получение заказа по его id.
    /// </summary>
    /// <param name="query">запрос на получение заказа.</param>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns>ответ запроса.</returns>
    Task<GetOrderByIdQueryResponse> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken);
}

using System;
using FoodDelivery.Core.Contracts.Application.Queries.Menu;

namespace FoodDelivery.Core.Contracts.Ports.Menu.In;

/// <summary>
/// Интерфейс для обработки запросов 
/// </summary>
public interface IMenuQueriesHandler
{
    /// <summary>
    /// Список всех меню.
    /// </summary>
    /// <param name="query">запрос на получение всех меню.</param>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns>Меню.</returns>
    Task<GetAllMenuItemsResponse> Handle(GetAllMenuItemsQuery query, CancellationToken cancellationToken);
}

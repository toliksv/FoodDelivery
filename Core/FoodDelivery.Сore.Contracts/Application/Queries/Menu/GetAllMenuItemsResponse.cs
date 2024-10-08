using System;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Menu;

namespace FoodDelivery.Core.Contracts.Application.Queries.Menu;

/// <summary>
/// Ответ на запрос списка блюд.
/// </summary>
public class GetAllMenuItemsResponse
{
    /// <summary>
    /// Конструктор.    
    /// </summary>
    /// <param name="menuItems"></param>
    public GetAllMenuItemsResponse(List<MenuItem> menuItems)
    {
        Menu = menuItems ?? new List<MenuItem>();
    }

    /// <summary>
    /// Список меню.
    /// </summary>
    public List<MenuItem> Menu { get; }
}
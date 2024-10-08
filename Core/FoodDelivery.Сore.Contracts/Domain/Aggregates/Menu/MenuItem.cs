using System;

namespace FoodDelivery.Core.Contracts.Domain.Aggregates.Menu;

/// <summary>
/// Пункт меню ресторана (Блюдо).
/// </summary>
public class MenuItem
{
    /// <summary>
    /// Идентификатор блюда.
    /// </summary>
    public int MenuItemId { get; set; }

    /// <summary>
    /// Наименование блюда.
    /// </summary>
    public string Name { get; set;}

    /// <summary>
    /// Описание блюда.
    /// </summary>
    public string Description { get; set;}

    /// <summary>
    /// Цена блюда.
    /// </summary>
    public decimal Price { get; set;}

}

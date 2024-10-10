using System;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Menu;
using FoodDelivery.PostgreSql.Application.Models.Menu;

namespace FoodDelivery.PostgreSql.Application.Mapping.Menu;

/// <summary>
/// Маппинг между <see cref="MenuItem"/> и <see cref="MenuItemEntity"/>
/// </summary>
internal interface IMenuItemMapper
{
    /// <summary>
    /// Маппинг <see cref="MenuItemEntity"/> на <see cref="MenuItem"/>.
    /// </summary>
    /// <param name="source"><see cref="MenuItemEntity"/></param>
    /// <returns><see cref="MenuItem"/></returns>
    MenuItem Map(MenuItemEntity source);
}

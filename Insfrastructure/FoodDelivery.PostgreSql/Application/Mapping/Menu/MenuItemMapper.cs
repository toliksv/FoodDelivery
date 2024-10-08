using System;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Menu;
using FoodDelivery.PostgreSql.Application.Models.Menu;

namespace FoodDelivery.PostgreSql.Application.Mapping.Menu;

/// <inheritdoc cref="IMenuItemMapper" />
internal class MenuItemMapper : IMenuItemMapper
{
    public MenuItem Map(MenuItemEntity source)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        return new MenuItem
        {
            MenuItemId = source.MenuItemId,
            Name = source.Name,
            Description = source.Description,
            Price = source.Price,
        };
    }
}

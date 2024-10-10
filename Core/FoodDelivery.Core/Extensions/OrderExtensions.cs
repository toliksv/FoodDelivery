using System;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Orders;

namespace FoodDelivery.Core.Extensions;

/// <summary>
/// Методы расширения для класса <see cref="Order"/>.
/// </summary>
public static class OrderExtensions
{
    /// <summary>
    /// Поиск блюда в заказе.
    /// </summary>
    /// <param name="order">заказ.</param>
    /// <param name="">идентификатор блюда.</param>
    /// <returns>позиция заказа.</returns>
    public static OrderItem FindOrderItem(this Order order, int menuItemId)
    {
        var index = order.Items.FindIndex(x => x.MenuItem?.MenuItemId == menuItemId);
        if (index < 0)
        {
            return null;
        }

        return order.Items[index];
    }
}

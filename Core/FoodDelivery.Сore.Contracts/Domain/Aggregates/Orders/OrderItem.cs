using FoodDelivery.Core.Contracts.Domain.Aggregates.Menu;

namespace FoodDelivery.Core.Contracts.Domain.Aggregates.Orders;

/// <summary>
/// Пункт заказа.
/// </summary>
public class OrderItem
{
    public OrderItem(MenuItem menuItem, int quantity)
    {     
        MenuItem = menuItem ?? throw new ArgumentException(nameof(menuItem));
        Quantity = quantity;
    }

    /// <summary>
    /// Пункт меню ресторана (блюдо).
    /// </summary>
    public MenuItem MenuItem { get; }
    /// <summary>
    /// Количество блюд.
    /// </summary>
    public int Quantity { get; set; } = 0;

    /// <summary>
    /// Цена за единицу товара.
    /// </summary>
    public decimal PricePerUnit => MenuItem?.Price ?? 0m;

    /// <summary>
    /// Цена за все блода.
    /// </summary>
    public decimal TotalPrice => (MenuItem?.Price ?? 0) * Quantity;
}

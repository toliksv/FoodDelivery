using System;

namespace FoodDelivery.Core.Contracts.Application.Dto.Orders;

/// <summary>
/// Web представление пункта заказа.
/// </summary>
public class OrderItemDto
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
    
    /// <summary>
    /// Количество блюд.
    /// </summary>
    public int Quantity { get; set; } = 0;
}

namespace FoodDelivery.Core.Contracts.Domain.Aggregates.Orders;

/// <summary>
/// Статус заказа.
/// </summary>
[Flags]
public enum OrderStatus : int
{
    /// <summary>
    /// Не установлен.
    /// </summary>
    Undefined = 0,

    /// <summary>
    /// Создан.
    /// </summary>
    Created = 1,

    /// <summary>
    /// В обработке.
    /// </summary>
    InProgress = 2,

    /// <summary>
    ///  В доставке.
    /// </summary>
    Delivering = 4,

    /// <summary>
    /// Доставлен.
    /// </summary>
    Delivered = 8,
}

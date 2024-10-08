namespace FoodDelivery.PostgreSql.Application.Models.Orders;

/// <summary>
/// Тип событий по заказу.
/// </summary>
public enum OrderEventType
{
    /// <summary>
    /// Не задан.
    /// </summary>
    Undefined = 0,

    /// <summary>
    /// Создание заказа.
    /// </summary>
    CreatingOrder = 1,

    /// <summary>
    /// Установка статуса заказу.
    /// </summary>
    SettingStatusToOrder = 2,

    /// <summary>
    /// Добавление блюда в заказа.
    /// </summary>
    AddingOrderItem = 3,

    /// <summary>
    /// Удаление позиции из заказа.
    /// </summary>
    RemovingOrderItem = 4,

    /// <summary>
    /// Изменение количества позиции в заказе.
    /// </summary>
    ChangingOrderItemQuantity = 5,
}

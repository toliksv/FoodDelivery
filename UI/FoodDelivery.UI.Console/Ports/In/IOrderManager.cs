using System;

namespace FoodDelivery.UI.Console.Ports.In;

/// <summary>
/// Забора с заказом.
/// </summary>
public interface IOrderManager
{
    /// <summary>
    /// Создать заказ.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AddOrder(CancellationToken cancellationToken);

    /// <summary>
    /// Редактировать заказ.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task EditOrder(CancellationToken cancellationToken);

    /// <summary>
    /// Заплатить за заказ.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task PayForOrder(CancellationToken cancellationToken)
}

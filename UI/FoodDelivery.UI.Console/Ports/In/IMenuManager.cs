using System;

namespace FoodDelivery.UI.Console.Ports.In;

/// <summary>
/// Работа с меню.
/// </summary>
public interface IMenuManager
{
    /// <summary>
    /// Показать меню.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ShowMenu(CancellationToken cancellationToken);
}

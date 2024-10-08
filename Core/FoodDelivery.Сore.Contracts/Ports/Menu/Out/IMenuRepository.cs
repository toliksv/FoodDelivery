using System;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Menu;

namespace FoodDelivery.Core.Contracts.Ports.Menu.Out;

/// <summary>
/// Репозиторий для хранения блюд.
/// </summary>
/// <remarks>
/// где-то лежит список блюд. ДОбавлением новых и ресторанами не
/// заморачиваемся.
/// </remarks>
public interface IMenuRepository
{
    /// <summary>
    /// Получить полный список меню.
    /// </summary>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns></returns>
    Task<List<MenuItem>> GetAllMenuItems(CancellationToken cancellationToken);
}

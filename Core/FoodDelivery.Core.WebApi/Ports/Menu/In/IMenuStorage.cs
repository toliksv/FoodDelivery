using System;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Menu;

namespace FoodDelivery.Core.WebApi.Ports.Menu.In;

/// <summary>
/// Клиент к хранилищу данных в части меню.
/// </summary>
public interface IMenuStorage
{
    /// <summary>
    /// Получить меню.
    /// </summary>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns>ожидание меню.</returns>
    public Task<List<MenuItem>> GetMenu(CancellationToken cancellationToken);
}

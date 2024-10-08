using System;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Menu;
using RestEase;

namespace FoodDelivery.Core.WebApi.Ports.Menu.Out;

/// <summary>
/// Клиент к хранилищу меню.
/// </summary>
[BasePath("api/postgre-sql-adapter/menu")]
public interface IMenuStorageController
{
    /// <summary>
    /// Получить меню.
    /// </summary>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns>меню.</returns>
    [Get("list-all")]
    Task<List<MenuItem>> GetAll(CancellationToken cancellationToken);
}

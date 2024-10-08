using System;
using FoodDelivery.Core.Contracts.Application.Commands.Orders;


namespace FoodDelivery.Core.Contracts.Ports.Orders.In;

/// <summary>
/// Обработчик команд по заказам. 
/// </summary>
public interface IOrdersCommandHandler
{
    /// <summary>
    /// Создание заказа.
    /// </summary>
    /// <param name="createOrderCommand">команда созданя заказаю</param>
    /// <param name="cancellationToken">токени отмены./param>
    Task Handle (CreateOrderCommand createOrderCommand, CancellationToken cancellationToken);

    /// <summary>
    /// Установка статуса заказа.
    /// </summary>
    /// <param name="setStatusCommand">команда установки статуса заказу.</param>
    /// <param name="cancellationToken">токен отмены</param>
    Task Handle (SetOrderStatusCommand setStatusCommand, CancellationToken cancellationToken);

    /// <summary>
    /// Добавление блюд в заказ.
    /// </summary>
    /// <param name="addItemsToOrderCommand">команда добавления блюд в заказ.</param>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns></returns>
    Task Handle (AddItemsToOrderCommand addItemsToOrderCommand, CancellationToken cancellationToken);

    /// <summary>
    /// Удаление блюд из заказа.
    /// </summary>
    /// <param name="removeItemsFromOrderCommand">команда удаления блюд из заказа.</param>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns></returns>
    Task Handle (RemoveItemsFromOrderCommand removeItemsFromOrderCommand, CancellationToken cancellationToken);

    /// <summary>
    /// Изменение количества блюда в заказе.
    /// </summary>
    /// <param name="changeOrderItemQuantityCommand">команда изменения количества блюда в заказе.</param>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns></returns>
    Task Handle (ChangeOrderItemQuantityCommand changeOrderItemQuantityCommand, CancellationToken cancellationToken);
}

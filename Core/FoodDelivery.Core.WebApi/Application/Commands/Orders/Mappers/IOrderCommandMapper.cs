using System;
using FoodDelivery.Core.Contracts.Application.Commands.Orders;
using FoodDelivery.Core.Contracts.Application.Commands.Orders.Web;

namespace FoodDelivery.Core.WebApi.Application.Commands.Orders.Mappers;

/// <summary>
/// Маппинг между командами ядра и их представлениями.ать зая
/// </summary>
public interface IOrderCommandMapper
{
    /// <summary>
    /// Маппинг <see cref="CreateOrderCommandRequest"/> на <see cref="CreateOrderCommand"/>.
    /// </summary>
    /// <param name="source"><see cref="CreateOrderCommandRequest"/>.</param>
    /// <returns><see cref="CreateOrderCommand"/></returns>
    CreateOrderCommand Map(CreateOrderCommandRequest source);

    /// <summary>
    /// Маппинг <see cref="SetOrderStatusCommandRequest"/> на <see cref="SetOrderStatusCommand"/>.
    /// </summary>
    /// <param name="source"><see cref="CreateOrderCommandRequest"/>.</param>
    /// <returns><see cref="SetOrderStatusCommand"/></returns>
    SetOrderStatusCommand Map(SetOrderStatusCommandRequest source);

    /// <summary>
    /// Маппинг <see cref="AddItemsToOrderCommandRequest"/> на <see cref="AddItemsToOrderCommand"/>.
    /// </summary>
    /// <param name="source"><see cref="AddItemsToOrderCommandRequest"/>.</param>
    /// <returns><see cref="AddItemsToOrderCommand"/></returns>
    AddItemsToOrderCommand Map(AddItemsToOrderCommandRequest source);

    /// <summary>
    /// Маппинг <see cref="RemoveItemsFromOrderCommandRequest"/> на <see cref="RemoveItemsFromOrderCommand"/>.
    /// </summary>
    /// <param name="source"><see cref="RemoveItemsFromOrderCommandRequest"/>.</param>
    /// <returns><see cref="RemoveItemsFromOrderCommand"/></returns>
    RemoveItemsFromOrderCommand Map(RemoveItemsFromOrderCommandRequest source);

    /// <summary>
    /// Маппинг <see cref="ChangeOrderItemQuantityCommandRequest"/> на <see cref="ChangeOrderItemQuantityCommand"/>.
    /// </summary>
    /// <param name="source"><see cref="ChangeOrderItemQuantityCommandRequest"/>.</param>
    /// <returns><see cref="ChangeOrderItemQuantityCommand"/></returns>
    ChangeOrderItemQuantityCommand Map(ChangeOrderItemQuantityCommandRequest source);
}

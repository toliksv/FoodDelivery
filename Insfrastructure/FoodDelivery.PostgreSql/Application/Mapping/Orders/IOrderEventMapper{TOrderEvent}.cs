using System;
using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.PostgreSql.Application.Mapping.Orders;
using FoodDelivery.PostgreSql.Application.Models.Orders;

namespace FoodDelivery.PostgreSql.Application.Mapping;

/// <summary>
/// Маппинг между <see cref="TOrderEvent" />  и <see cref="OrderEventEntity" />
/// </summary>
/// <typeparam name="TOrderEvent">тип события.</typeparam>
internal interface IOrderEventMapper<TOrderEvent> : IOrderEventMapper
where TOrderEvent : OrderEventBase
{
    /// <summary>
    /// Маппинг <see cref="TOrderEvent" />  на <see cref="OrderEventEntity" />.
    /// </summary>
    /// <param name="source"><see cref="OrderEventEntity"/></param>
    /// <returns><see cref="TOrderEvent" /></returns>
    OrderEventEntity Map(TOrderEvent source);

    /// <summary>
    /// Маппинг <see cref="OrderEventEntity" /> на <see cref="TOrderEvent" />.
    /// </summary>
    /// <param name="source"><see cref="OrderEventEntity" /></param>
    /// <returns><see cref="TOrderEvent" /></returns>
    TOrderEvent Map(OrderEventEntity source);
}

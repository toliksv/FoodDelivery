using System;
using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.PostgreSql.Application.Models.Orders;

namespace FoodDelivery.PostgreSql.Application.Mapping.Orders;

/// <summary>
/// Базоый интерфейс маппинга событий заказа
/// </summary>
internal interface IOrderEventMapper
{
    /// <summary>
    /// Маппинг события на cущность БД.
    /// </summary>
    /// <param name="source">событие</param>
    /// <returns>сущность БД.</returns>
    OrderEventEntity MapFromEvent(OrderEventBase source);

    /// <summary>
    /// Маппинг сущности БД на событие.
    /// </summary>
    /// <param name="source">сущность БД.</param>
    /// <returns>событие.</returns>
    OrderEventBase MapToEvent(OrderEventEntity source);
}

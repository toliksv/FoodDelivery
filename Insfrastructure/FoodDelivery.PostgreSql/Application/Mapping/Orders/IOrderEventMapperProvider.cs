using System;
using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.PostgreSql.Application.Models.Orders;

namespace FoodDelivery.PostgreSql.Application.Mapping.Orders;

/// <summary>
/// Провайдер для мапперов.
/// </summary>
internal interface IOrderEventMapperProvider
{
    /// <summary>
    /// Предоставляет маппер для указанного события.
    /// </summary>
    /// <param name="orderEvent">событие.</param>
    /// <returns>маппер.</returns>
    IOrderEventMapper ProvideMapper(OrderEventBase orderEvent);

    /// <summary>
    /// Предоставляет маппер для указанного типа события.
    /// </summary>
    /// <typeparam name="TOrderEvent"></typeparam>
    /// <returns>маппер.</returns>
    IOrderEventMapper<TOrderEvent> ProvideMapper<TOrderEvent>() where TOrderEvent : OrderEventBase;

    /// <summary>
    /// Предоставляет маппер согласно указангому <see cref="OrderEventType"/>
    /// </summary>
    /// <param name="orderEventType">тип события.</param>
    /// <returns>маппер.</returns>
    IOrderEventMapper ProvideMapper(OrderEventType orderEventType);
}

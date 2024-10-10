using System;
using System.Diagnostics.Contracts;

namespace FoodDelivery.Core.Contracts.Application.Events.Orders;

/// <summary>
/// Базовый класс события.
/// </summary>
public abstract class OrderEventBase
{
    /// <summary>
    /// Идентификатор заказа.
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Идентификатор клиента.
    /// </summary>
    /// <remarks>
    /// Проще протащить Id клиента во все события, чем собирать сначала id
    /// заказов, а потом все события по ним.
    /// </remarks>
    public int ClientId { get; set; }

    /// <summary>
    /// Дата события.
    /// </summary>
    public DateTime OccurredOn { get; set; }
}

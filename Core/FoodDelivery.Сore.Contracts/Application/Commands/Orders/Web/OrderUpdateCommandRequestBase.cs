using System;

namespace FoodDelivery.Core.Contracts.Application.Commands.Orders.Web;

/// <summary>
/// Базовый класс запроса для комнад изменения заказа.
/// </summary>
public abstract class OrderUpdateCommandRequestBase : OrderCommandRequestBase
{
    /// <summary>
    /// Идентификатор клиента.
    /// </summary>
    public int OrderId { get; set; }
}

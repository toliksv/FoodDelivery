using System;
using FoodDelivery.Core.Contracts.Application.Events.Orders;

namespace FoodDelivery.PostgreSql.WebApi.Application.Web.Orders;

/// <summary>
/// Ответ на запросы получения соыбтий.
/// </summary>
public class OrderQueryResponse
{   
     /// <summary>
    /// Событие создания заказа.
    /// </summary>
    public List<CreateOrderEvent> CreateEvents { get; set; } = new List<CreateOrderEvent>();

    /// <summary>
    /// События установки статуса заказа.
    /// </summary>
    public List<SetOrderStatusEvent> SetStatusEvents { get; set;} = new List<SetOrderStatusEvent>();

    /// <summary>
    /// События добавления блюд в заказ.
    /// </summary>
    public List<AddOrderItemEvent> AddOrderItemEvents { get; set; } = new List<AddOrderItemEvent>();

    /// <summary>
    /// События удаления блюд из заказа.
    /// </summary>
    public List<RemoveOrderItemEvent> RemoveOrderItemEvents { get; set; } = new List<RemoveOrderItemEvent>();

    /// <summary>
    /// События изменения количества блюд в заказе.
    /// </summary>
    public List<ChangeOrderItemQuantityEvent> ChangeOrderItemQuantityEvents { get; set; } = new List<ChangeOrderItemQuantityEvent>();
}

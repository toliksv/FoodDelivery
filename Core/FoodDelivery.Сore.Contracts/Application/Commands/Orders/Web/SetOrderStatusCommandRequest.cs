using System;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Orders;

namespace FoodDelivery.Core.Contracts.Application.Commands.Orders.Web;

/// <summary>
/// WEB представление команды 
/// </summary>
public class SetOrderStatusCommandRequest : OrderUpdateCommandRequestBase
{
    
    /// <summary>
    /// Статус заказа.
    /// </summary>
    public OrderStatus Status { get; set; }  
}

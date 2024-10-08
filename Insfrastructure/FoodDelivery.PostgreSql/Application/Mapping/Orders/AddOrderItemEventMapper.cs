using System;
using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.PostgreSql.Application.Models.Orders;

namespace FoodDelivery.PostgreSql.Application.Mapping.Orders;

/// <inheritdoc cref="IOrderEventMapper" />
internal class AddOrderItemEventMapper : OrderEventMapperBase<AddOrderItemEvent>
{
    public override AddOrderItemEvent Map(OrderEventEntity source)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (source.OrderEventData is null)
        {
            throw new ArgumentNullException(nameof(source.OrderEventData));
        }

        return new AddOrderItemEvent
        {
            OrderId = source.OrderId, 
            ClientId = source.ClientId, 
            OccurredOn = source.OccuredOn, 
            MenuItemId = source.OrderEventData.MenuItemId ?? throw new ArgumentNullException(nameof(source.OrderEventData.MenuItemId)), 
            Quantity = source.OrderEventData.Quantity ?? throw new ArgumentNullException(nameof(source.OrderEventData.Quantity)),
        };
    }

    protected override OrderEventData GetOrderEventData(AddOrderItemEvent source)
        => new OrderEventData
        {
            MenuItemId = source.MenuItemId,
            Quantity = source.Quantity,
        };
}

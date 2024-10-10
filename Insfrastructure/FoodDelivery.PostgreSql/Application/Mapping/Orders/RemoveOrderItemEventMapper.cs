using System;
using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.PostgreSql.Application.Models.Orders;

namespace FoodDelivery.PostgreSql.Application.Mapping.Orders;

/// <inheritdoc cref="IOrderEventMapper{TOrderEvent}" />
internal class RemoveOrderItemEventMapper : OrderEventMapperBase<RemoveOrderItemEvent>
{
    public override RemoveOrderItemEvent Map(OrderEventEntity source)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (source.OrderEventData == null)
        {
            throw new ArgumentNullException(nameof(source.OrderEventData));
        }

        return new RemoveOrderItemEvent
        {
            OrderId = source.OrderId,
            ClientId = source.ClientId,
            OccurredOn = source.OccuredOn,
            MenuItemId = source.OrderEventData?.MenuItemId ?? throw new ArgumentNullException(nameof(source.OrderEventData.MenuItemId)),
        };
    }

    protected override OrderEventData GetOrderEventData(RemoveOrderItemEvent source)
        => new OrderEventData { MenuItemId = source.MenuItemId};
}

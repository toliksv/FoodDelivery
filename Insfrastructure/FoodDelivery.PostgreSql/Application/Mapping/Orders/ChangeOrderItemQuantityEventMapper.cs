using System;
using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.PostgreSql.Application.Models.Orders;

namespace FoodDelivery.PostgreSql.Application.Mapping.Orders;

/// <inheritdoc cref="IOrderEventMapper{TOrderEvent}" />
internal class ChangeOrderItemQuantityEventMapper : OrderEventMapperBase<ChangeOrderItemQuantityEvent>
{
    public override ChangeOrderItemQuantityEvent Map(OrderEventEntity source)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));            
        }

        if (source.OrderEventData is null)
        {
            throw new ArgumentNullException(nameof(source.OrderEventData));
        }

        return new ChangeOrderItemQuantityEvent
        {
            OrderId = source.OrderId,
            ClientId = source.ClientId,
            OccurredOn = source.OccuredOn,
            MenuItemId = source.OrderEventData.MenuItemId ?? throw new ArgumentNullException(nameof(source.OrderEventData.MenuItemId)),
            NewQuantity = source.OrderEventData.NewQuantity ?? throw new ArgumentNullException(nameof(source.OrderEventData.NewQuantity))
        };
    }

    protected override OrderEventData GetOrderEventData(ChangeOrderItemQuantityEvent source)
        => new OrderEventData
        {
            MenuItemId = source.MenuItemId,
            NewQuantity = source.NewQuantity,
        };
}

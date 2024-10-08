using System;
using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.PostgreSql.Application.Models.Orders;

namespace FoodDelivery.PostgreSql.Application.Mapping.Orders;

internal class SetOrderStatusEventMapper : OrderEventMapperBase<SetOrderStatusEvent>
{
    public override SetOrderStatusEvent Map(OrderEventEntity source)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        return new SetOrderStatusEvent
        {
            OrderId = source.OrderId,
            ClientId = source.ClientId,
            OccurredOn = source.OccuredOn,
            NewStatus = source.OrderEventData?.NewOrderStatus ?? throw new ArgumentNullException(nameof(source.OrderEventData.NewOrderStatus)),
        };
    }

    protected override OrderEventData GetOrderEventData(SetOrderStatusEvent source)
        => new OrderEventData { NewOrderStatus = source.NewStatus};
}

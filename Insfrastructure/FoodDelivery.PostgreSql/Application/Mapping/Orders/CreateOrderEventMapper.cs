using System;
using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.PostgreSql.Application.Models.Orders;

namespace FoodDelivery.PostgreSql.Application.Mapping.Orders;

internal class CreateOrderEventMapper : OrderEventMapperBase<CreateOrderEvent>
{
    public override CreateOrderEvent Map(OrderEventEntity source)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        return new CreateOrderEvent        
        {
            OrderId = source.OrderId,
            ClientId = source.ClientId,
            OccurredOn = source.OccuredOn,
        };
    }    
        

    protected override OrderEventData GetOrderEventData(CreateOrderEvent source)
        => null; 
    
}

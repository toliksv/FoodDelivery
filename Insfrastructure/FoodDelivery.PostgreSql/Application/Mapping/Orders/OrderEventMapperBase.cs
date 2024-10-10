using System;
using System.Security.Cryptography;
using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.PostgreSql.Extensions;
using FoodDelivery.PostgreSql.Application.Models.Orders;

namespace FoodDelivery.PostgreSql.Application.Mapping.Orders;

/// <inheritdoc cref="IOrderEventMapper{TOrderEvent}" />
internal abstract class OrderEventMapperBase<TOrderEvent> : IOrderEventMapper<TOrderEvent>
where TOrderEvent : OrderEventBase 
{
     public OrderEventEntity Map(TOrderEvent source)
     {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        var entity = new OrderEventEntity();
        entity.OrderId = source.OrderId;
        entity.ClientId = source.ClientId;
        entity.OccuredOn = source.OccurredOn;
        entity.OrderEventType = source.GetOrderEventType();
        entity.OrderEventData = GetOrderEventData(source);
        return entity;
    }

    public OrderEventEntity MapFromEvent(OrderEventBase source)
      => Map((TOrderEvent)source);

    public abstract TOrderEvent Map(OrderEventEntity source);

    public OrderEventBase MapToEvent(OrderEventEntity source)
        => Map(source);

    protected abstract OrderEventData GetOrderEventData(TOrderEvent source);  
       
}

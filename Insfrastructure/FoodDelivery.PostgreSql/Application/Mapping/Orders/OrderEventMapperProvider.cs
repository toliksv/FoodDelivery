using System;
using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.PostgreSql.Extensions;
using FoodDelivery.PostgreSql.Application.Models.Orders;
using Microsoft.Extensions.DependencyInjection;

namespace FoodDelivery.PostgreSql.Application.Mapping.Orders;

internal class OrderEventMapperProvider : IOrderEventMapperProvider
{
    private readonly IServiceProvider _serviceProvider;

    public OrderEventMapperProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public IOrderEventMapper ProvideMapper(OrderEventBase orderEvent)
     => ProvideMapper(orderEvent.GetOrderEventType());


    public IOrderEventMapper<TOrderEvent> ProvideMapper<TOrderEvent>() where TOrderEvent : OrderEventBase
        => _serviceProvider.GetRequiredService<IOrderEventMapper<TOrderEvent>>();

    public IOrderEventMapper ProvideMapper(OrderEventType orderEventType)
         => orderEventType switch
        {
            OrderEventType.CreatingOrder => ProvideMapper<CreateOrderEvent>(),
            OrderEventType.SettingStatusToOrder => ProvideMapper<SetOrderStatusEvent>(),
            OrderEventType.AddingOrderItem => ProvideMapper<AddOrderItemEvent>(),           
            OrderEventType.RemovingOrderItem => ProvideMapper<RemoveOrderItemEvent>(),
            OrderEventType.ChangingOrderItemQuantity => ProvideMapper<ChangeOrderItemQuantityEvent>(),
            _ => throw new InvalidOperationException("Неизвестное событие."),
        };
    
}

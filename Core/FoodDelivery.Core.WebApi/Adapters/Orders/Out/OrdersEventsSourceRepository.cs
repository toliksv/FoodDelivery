using System;

using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.Core.Contracts.Ports.Orders.Out;
using FoodDelivery.Core.WebApi.Ports.Orders.In;

namespace FoodDelivery.Core.WebApi.Adapters.Orders.Out;

internal class OrdersEventsSourceRepository : IOrdersEventsSourceRepository
{
    private readonly IOrderStorage _orderStorage;

    public OrdersEventsSourceRepository(IOrderStorage orderStorage)
    {
        _orderStorage = orderStorage ?? throw new ArgumentNullException(nameof(orderStorage));
    }

    public Task<List<OrderEventBase>> GetClientEvents(int clientId, CancellationToken cancellationToken)
        => _orderStorage.GetOrderEventsByClientId(clientId, cancellationToken);

    public Task<List<OrderEventBase>> GetOrderEvents(int orderId, CancellationToken cancellationToken)
     => _orderStorage.GetOrderEventsByOrderId(orderId, cancellationToken);

    public Task SaveEvent<TOrderEvent>(TOrderEvent orderEventBase, CancellationToken cancellationToken) where TOrderEvent : OrderEventBase
    {
        if (orderEventBase is null)
        {
            throw new ArgumentNullException(nameof(orderEventBase));
        }

        switch (orderEventBase)
        {
            case CreateOrderEvent createOrderEvent:
                return _orderStorage.WriteCreateOrderEvent(createOrderEvent, cancellationToken);
            case SetOrderStatusEvent setOrderStatusEvent:
                return _orderStorage.WriteSetOrderStatusEvent(setOrderStatusEvent, cancellationToken);
            case AddOrderItemEvent addOrderItemEvent:
                return _orderStorage.WriteAddOrderItemEvent(addOrderItemEvent, cancellationToken);
            case RemoveOrderItemEvent removeOrderItemEvent:
                return _orderStorage.WriteRemoveOrderItemEvent(removeOrderItemEvent, cancellationToken);
            case ChangeOrderItemQuantityEvent changeOrderItemQuantityEvent:
                return _orderStorage.WriteChangeOrderItemQuantityEvent(changeOrderItemQuantityEvent, cancellationToken);
            default:
             throw new InvalidOperationException("Неизвестное событие");
        }
    }
}

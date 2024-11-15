using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.Core.WebApi.Application.Queries.Orders.Web;
using FoodDelivery.Core.WebApi.Ports.Orders.In;
using FoodDelivery.Kafka.Contracts.Ports.Orders.In;
using Polly;

namespace FoodDelivery.Core.WebApi.Adapters.Orders.In;

/// <inheritdoc cref="IOrderStorage" />
internal class OrderStorage : IOrderStorage
{
    private readonly IOrderStorageControllerWrapper _orderStorageControllerWrapper;
    private readonly ResiliencePipeline _reilienePipeline;
    private readonly IOrderEventsProducer _orderEventsProducer;
    
    public OrderStorage(IOrderStorageControllerWrapper orderStorageControllerWrapper,
        [FromKeyedServices("main")] ResiliencePipeline reilienePipeline, IOrderEventsProducer orderEventsProducer)
    {
        _orderStorageControllerWrapper = orderStorageControllerWrapper;
        _reilienePipeline = reilienePipeline;
        _orderEventsProducer = orderEventsProducer ?? throw new ArgumentNullException(nameof(orderEventsProducer));
    }

    public Task<List<OrderEventBase>> GetOrderEventsByClientId(int clientId, CancellationToken cancellationToken)
    {
        return WrapOrderQuery(_orderStorageControllerWrapper.GetOrderEventsByClientId, clientId, cancellationToken);
    }

    public Task<List<OrderEventBase>> GetOrderEventsByOrderId(int orderId, CancellationToken cancellationToken)
    {
        return WrapOrderQuery(_orderStorageControllerWrapper.GetOrderEventsByOrderId, orderId, cancellationToken);
    }


    public Task WriteAddOrderItemEvent(AddOrderItemEvent addOrderItemEvent, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(addOrderItemEvent);
        return _orderEventsProducer.Produce(addOrderItemEvent, cancellationToken);
    }

    public Task WriteChangeOrderItemQuantityEvent(ChangeOrderItemQuantityEvent changeOrderItemQuantityEvent,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(changeOrderItemQuantityEvent);
        return _orderEventsProducer.Produce(changeOrderItemQuantityEvent, cancellationToken);
    }

    public Task WriteCreateOrderEvent(CreateOrderEvent createOrderEvent, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(createOrderEvent);
        return _orderEventsProducer.Produce(createOrderEvent, cancellationToken);
    }

    public Task WriteRemoveOrderItemEvent(RemoveOrderItemEvent removeOrderItemEvent,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(removeOrderItemEvent);
        return _orderEventsProducer.Produce(removeOrderItemEvent, cancellationToken);
    }

    public Task WriteSetOrderStatusEvent(SetOrderStatusEvent setOrderStatusEvent,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(setOrderStatusEvent);
        return _orderEventsProducer.Produce(setOrderStatusEvent, cancellationToken);
    }

    private async Task<List<OrderEventBase>> WrapOrderQuery(
        Func<int, CancellationToken, ValueTask<OrderEventsQueryStorageResponse>> func, int idEntity,
        CancellationToken cancellationToken)
    {
        var response = await _reilienePipeline.ExecuteAsync(tkn => func(idEntity, tkn), cancellationToken);
        if (response is null) return null;

        return GetEvents(response);
    }

    private List<OrderEventBase> GetEvents(OrderEventsQueryStorageResponse response)
    {
        if (response is null) throw new ArgumentNullException(nameof(response));

        var events = new List<OrderEventBase>();
        events.AddRange(response.CreateEvents);
        events.AddRange(response.SetStatusEvents);
        events.AddRange(response.AddOrderItemEvents);
        events.AddRange(response.RemoveOrderItemEvents);
        events.AddRange(response.ChangeOrderItemQuantityEvents);
        return events;
    }
}
using System;
using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.Core.Contracts.Application.Queries.Orders.Web;
using FoodDelivery.Core.WebApi.Ports.Orders.In;
using FoodDelivery.Core.WebApi.Ports.Orders.Out;
using Polly;

namespace FoodDelivery.Core.WebApi.Adapters.Orders.In;

/// <inheritdoc cref="IOrderStorage" />
internal class OrderStorage : IOrderStorage
{
    private readonly IOrderStorageControllerWrapper _orderStorageControllerWrapper;
    private readonly ResiliencePipeline  _reilienePipeline;

    public OrderStorage(IOrderStorageControllerWrapper orderStorageControllerWrapper, [FromKeyedServices("main")]ResiliencePipeline reilienePipeline)
    {
        _orderStorageControllerWrapper = orderStorageControllerWrapper;
        _reilienePipeline = reilienePipeline;
    }

    public Task<List<OrderEventBase>> GetOrderEventsByClientId(int clientId, CancellationToken cancellationToken)
        => WrapOrderQuery(_orderStorageControllerWrapper.GetOrderEventsByClientId, clientId, cancellationToken);                   

    public Task<List<OrderEventBase>> GetOrderEventsByOrderId(int orderId, CancellationToken cancellationToken)
        => WrapOrderQuery(_orderStorageControllerWrapper.GetOrderEventsByOrderId, orderId, cancellationToken);
    

    public async Task WriteAddOrderItemEvent(AddOrderItemEvent addOrderItemEvent, CancellationToken cancellationToken)
    {
        if (addOrderItemEvent is null)
        {
            throw new ArgumentNullException(nameof(addOrderItemEvent));
        }

        await _reilienePipeline.ExecuteAsync(tkn => _orderStorageControllerWrapper.WriteAddOrderItemEvent(addOrderItemEvent, tkn), cancellationToken);
    }

    public async Task WriteChangeOrderItemQuantityEvent(ChangeOrderItemQuantityEvent changeOrderItemQuantityEvent, CancellationToken cancellationToken)
    {
        if (changeOrderItemQuantityEvent is null)
        {
            throw new ArgumentNullException(nameof(changeOrderItemQuantityEvent));
        }

        await _reilienePipeline.ExecuteAsync(tkn => _orderStorageControllerWrapper.WriteChangeOrderItemQuantityEvent(changeOrderItemQuantityEvent, tkn));  
    }

    public async Task WriteCreateOrderEvent(CreateOrderEvent createOrderEvent, CancellationToken cancellationToken)
    {
        if (createOrderEvent is null)
        {
            throw new ArgumentNullException(nameof(createOrderEvent));
        }

        await _reilienePipeline.ExecuteAsync(tkn => _orderStorageControllerWrapper.WriteCreateOrderEvent(createOrderEvent, tkn), cancellationToken);
    }

    public async Task WriteRemoveOrderItemEvent(RemoveOrderItemEvent removeOrderItemEvent, CancellationToken cancellationToken)
    {
        if (removeOrderItemEvent is null)
        {
            throw new ArgumentNullException(nameof(removeOrderItemEvent));
        }

        await _reilienePipeline.ExecuteAsync(tkn => _orderStorageControllerWrapper.WriteRemoveOrderItemEvent(removeOrderItemEvent, tkn), cancellationToken);
    }

    public async Task WriteSetOrderStatusEvent(SetOrderStatusEvent setOrderStatusEvent, CancellationToken cancellationToken)
    {
        if (setOrderStatusEvent is null)
        {
            throw new ArgumentNullException(nameof(setOrderStatusEvent));            
        }

        await _reilienePipeline.ExecuteAsync(tkn => _orderStorageControllerWrapper.WriteSetOrderStatusEvent(setOrderStatusEvent, tkn), cancellationToken);
    }

    private async Task<List<OrderEventBase>> WrapOrderQuery(Func<int, CancellationToken, ValueTask<OrderEventsQueryStorageResponse>> func, int idEntity, CancellationToken cancellationToken)
    {
        var response = await _reilienePipeline.ExecuteAsync(tkn => func(idEntity, tkn), cancellationToken);
        if (response is null)
        {
            return null;
        }

        return GetEvents(response);    
    }

    private List<OrderEventBase> GetEvents(OrderEventsQueryStorageResponse response)
    {
        if (response is null)
        {
            throw new ArgumentNullException(nameof(response));            
        }

        var events = new List<OrderEventBase>();
        events.AddRange(response.CreateEvents);
        events.AddRange(response.SetStatusEvents);
        events.AddRange(response.AddOrderItemEvents);
        events.AddRange(response.RemoveOrderItemEvents);
        events.AddRange(response.ChangeOrderItemQuantityEvents);
        return events;     
    }
}

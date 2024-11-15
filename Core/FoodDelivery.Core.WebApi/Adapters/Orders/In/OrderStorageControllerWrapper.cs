using System;
using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.Core.Contracts.Application.Queries.Orders.Web;
using FoodDelivery.Core.WebApi.Application.Queries.Orders.Web;
using FoodDelivery.Core.WebApi.Ports.Orders.In;
using FoodDelivery.Core.WebApi.Ports.Orders.Out;
using Polly;

namespace FoodDelivery.Core.WebApi.Adapters.Orders.In;

internal class OrderStorageControllerWrapper : IOrderStorageControllerWrapper
{
    private readonly IOrderStorageController _orderStorageController;    

    public OrderStorageControllerWrapper(IOrderStorageController orderStorageController)
    {
        _orderStorageController = orderStorageController ?? throw new ArgumentNullException(nameof(orderStorageController));        
    }

    public async ValueTask<OrderEventsQueryStorageResponse> GetOrderEventsByClientId(int clientId, CancellationToken cancellationToken)
         => await  _orderStorageController.GetOrderEventsByClientId(clientId, cancellationToken);

    public async ValueTask<OrderEventsQueryStorageResponse> GetOrderEventsByOrderId(int orderId, CancellationToken cancellationToken)
        => await _orderStorageController.GetOrderEventsByOrderId(orderId, cancellationToken);

    public async ValueTask WriteAddOrderItemEvent(AddOrderItemEvent addOrderItemEvent, CancellationToken cancellationToken)
    {
        if (addOrderItemEvent is null)
        {
            throw new ArgumentNullException(nameof(addOrderItemEvent));
        }

        await _orderStorageController.WriteAddOrderItemEvent(addOrderItemEvent,  cancellationToken);
    }

    public async ValueTask WriteChangeOrderItemQuantityEvent(ChangeOrderItemQuantityEvent changeOrderItemQuantityEvent, CancellationToken cancellationToken)
    {
        if (changeOrderItemQuantityEvent is null)
        {
            throw new ArgumentNullException(nameof(changeOrderItemQuantityEvent));
        }

        await _orderStorageController.WriteChangeOrderItemQuantityEvent(changeOrderItemQuantityEvent, cancellationToken);  
    }

    public async ValueTask WriteCreateOrderEvent(CreateOrderEvent createOrderEvent, CancellationToken cancellationToken)
    {
        if (createOrderEvent is null)
        {
            throw new ArgumentNullException(nameof(createOrderEvent));
        }

        await _orderStorageController.WriteCreateOrderEvent(createOrderEvent,  cancellationToken);
    }

    public async ValueTask WriteRemoveOrderItemEvent(RemoveOrderItemEvent removeOrderItemEvent, CancellationToken cancellationToken)
    {
        if (removeOrderItemEvent is null)
        {
            throw new ArgumentNullException(nameof(removeOrderItemEvent));
        }

        await _orderStorageController.WriteRemoveOrderItemEvent(removeOrderItemEvent,  cancellationToken);
    }

    public async ValueTask WriteSetOrderStatusEvent(SetOrderStatusEvent setOrderStatusEvent, CancellationToken cancellationToken)
    {
        if (setOrderStatusEvent is null)
        {
            throw new ArgumentNullException(nameof(setOrderStatusEvent));            
        }

        await _orderStorageController.WriteSetOrderStatusEvent(setOrderStatusEvent,  cancellationToken);
    }
}

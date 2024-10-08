using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.Core.Contracts.Application.Queries.Menu;
using FoodDelivery.Core.Contracts.Application.Queries.Orders;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Menu;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Orders;
using FoodDelivery.Core.Contracts.Ports.Menu.In;
using FoodDelivery.Core.Contracts.Ports.Orders.In;
using FoodDelivery.Core.Contracts.Ports.Orders.Out;
using FoodDelivery.Core.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;


namespace FoodDelivery.Core.Adapters.Orders.In;

internal class OrdersQueriesHandler : IOrdersQueriesHandler
{
    private readonly Dictionary<int,  Order> _ordersDictionary;
    private readonly Dictionary<int, MenuItem> _menuDictionary;

    private readonly IOrdersEventsSourceRepository _ordersEventsRepository;
    private readonly IMenuQueriesHandler _menuQueriesHandler;
    private readonly ILogger<OrdersQueriesHandler> _logger;

    public OrdersQueriesHandler(IOrdersEventsSourceRepository ordersEventsSourceRepository, ILogger<OrdersQueriesHandler> logger, IMenuQueriesHandler menuQueriesHandler)
    {
        _ordersEventsRepository = ordersEventsSourceRepository ?? throw new ArgumentNullException(nameof(ordersEventsSourceRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ordersDictionary = new Dictionary<int, Order>();
        _menuDictionary = new Dictionary<int, MenuItem>();
        _menuQueriesHandler = menuQueriesHandler ?? throw new ArgumentNullException(nameof(menuQueriesHandler));
    }


    public async Task<GetClientOrdersQueryResponse> Handle(GetClientOrdersQuery query, CancellationToken cancellationToken)
    {
        if (query is null)
        {
            throw new ArgumentNullException(nameof(query));
        }

        var ordersEvents = await _ordersEventsRepository.GetClientEvents(query.ClientId, cancellationToken);
        await ProcessEvents(ordersEvents, cancellationToken);

        if (_ordersDictionary.Count > 0)
        {
            return new GetClientOrdersQueryResponse(_ordersDictionary.Values.ToList());
        }

        _logger.LogWarning("У клиента {ClientId} не найдены заказы.", query.ClientId);
        return null;        
    }

    public async Task<GetOrderByIdQueryResponse> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
    {
        if (query is null)
        {
            throw new ArgumentNullException(nameof(query));
        }

        var ordersEvents = await _ordersEventsRepository.GetOrderEvents(query.OrderId, cancellationToken);
        await ProcessEvents(ordersEvents, cancellationToken);
        if (_ordersDictionary.TryGetValue(query.OrderId, out var order))
        {
            return new GetOrderByIdQueryResponse(order);
        }

        _logger.LogWarning("Не найден заказ по идентификатору {OrderId}", query.OrderId);
        return null;
    }

    private async ValueTask ProcessEvents(List<OrderEventBase> ordersEvents, CancellationToken cancellationToken)
    {
        if (ordersEvents is null)
        {
            throw new ArgumentNullException(nameof(ordersEvents));
        }

        var orderedEvents = ordersEvents.OrderBy(ord => ord.OccurredOn);
        foreach (var orderEvent in orderedEvents)
        {
            await ProcessEvent(orderEvent, cancellationToken);
        }
    }

    private async ValueTask ProcessEvent(OrderEventBase orderEvent, CancellationToken cancellationToken)
     {
        if (orderEvent is null)
        {
            throw new ArgumentNullException(nameof(orderEvent));
        }

        switch (orderEvent)
        {
            case CreateOrderEvent createEvent:
                CreateEvent(createEvent);
                break;
            case SetOrderStatusEvent setOrderStatusEvent:
                await WrapChangeOrderEvent(HandleSetOrderStatusEvent, setOrderStatusEvent, cancellationToken);
                break;
            case AddOrderItemEvent addOrderItemEvent:
                await WrapChangeOrderEvent(HandleAddOrderItemEvent, addOrderItemEvent, cancellationToken);
                break;        
            case RemoveOrderItemEvent removeOrderItemEvent:
                await WrapChangeOrderEvent(HandleRemoveOrderItemEvent,removeOrderItemEvent, cancellationToken);
                break;
            case ChangeOrderItemQuantityEvent changeOrderItemQuantityEvent:
                await WrapChangeOrderEvent(HandleChangeOrderItemQuantityEvent, changeOrderItemQuantityEvent, cancellationToken);
                break;
            default:
                throw new InvalidOperationException($"Неизвестный тип события {orderEvent.GetType().FullName}");            
        }   
     }

     private ValueTask WrapChangeOrderEvent<TChangeOrderEvent>(Func<TChangeOrderEvent, Order, CancellationToken, ValueTask> changeFunc, TChangeOrderEvent changeOrderEvent, CancellationToken cancellationToken)
     where TChangeOrderEvent : OrderEventBase
     {
        if (changeOrderEvent is null)
        {
            throw new ArgumentNullException(nameof(changeOrderEvent));
        }

        if (_ordersDictionary.TryGetValue(changeOrderEvent.OrderId, out var order))
        {
           return changeFunc(changeOrderEvent, order,  cancellationToken);                        
        }

        return ValueTask.FromException(new InvalidAsynchronousStateException($"Не было события создарния заказа {changeOrderEvent.OrderId}"));
    }      


    private ValueTask HandleChangeOrderItemQuantityEvent(ChangeOrderItemQuantityEvent changeOrderItemQuantityEvent, Order order, CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
        {
            var orderItem = order.FindOrderItem(changeOrderItemQuantityEvent.MenuItemId);
            if (orderItem != null)
            {
                // поле количество read-only. придется заменить позицию новой.                        
                order.Items.Remove(orderItem);
                var newOrderItem = new OrderItem(orderItem.MenuItem, changeOrderItemQuantityEvent.NewQuantity);
                order.Items.Add(newOrderItem);
            }
            else
            {
            _logger.LogWarning("В заказе {OrderId} не найдено блюдо {MenuItemID}", changeOrderItemQuantityEvent.OrderId, changeOrderItemQuantityEvent.MenuItemId);
            }
        }

        return ValueTask.CompletedTask;   
    }   

    private ValueTask HandleRemoveOrderItemEvent(RemoveOrderItemEvent removeOrderItemEvent, Order order, CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
        {       
            var index = order.Items.FindIndex(x=> x.MenuItem.MenuItemId == removeOrderItemEvent.MenuItemId);
            if (index >= 0)
            {
                order.Items.RemoveAt(index);
            }
            else
            {
                _logger.LogWarning("В заказе {OrderId} не найдено блюдо {MenuItemID}", removeOrderItemEvent.OrderId, removeOrderItemEvent.MenuItemId);
            }
        }

        return ValueTask.CompletedTask;       
    }

    private async ValueTask HandleAddOrderItemEvent(AddOrderItemEvent addOrderItemEvent, Order order, CancellationToken cancellationToken)
    {                 
        var menuItem = await GetMenuItem(addOrderItemEvent.MenuItemId, cancellationToken);
        order.Items.Add(new OrderItem(menuItem, addOrderItemEvent.Quantity));         
    }

    private ValueTask HandleSetOrderStatusEvent(SetOrderStatusEvent setOrderStatusEvent, Order order, CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
        {
            order.Status = setOrderStatusEvent.NewStatus;            
        }

        return ValueTask.CompletedTask;
    }

    private void CreateEvent(CreateOrderEvent createOrderEvent)
     {
        if (createOrderEvent is null)
        {
            throw new ArgumentNullException(nameof(createOrderEvent));
        }

        if (!_ordersDictionary.ContainsKey(createOrderEvent.OrderId))
        {
            _ordersDictionary.Add(createOrderEvent.OrderId, new Order {
                OrderId = createOrderEvent.OrderId,
                ClientId = createOrderEvent.ClientId,                
            });
        }
    }    

    private async ValueTask<MenuItem> GetMenuItem(int menuItemId, CancellationToken cancellationToken)
    {
        if (_menuDictionary.Count == 0)
        {
            var response = await _menuQueriesHandler.Handle(new GetAllMenuItemsQuery(), cancellationToken);

            if (response?.Menu == null)
            {
                throw  new InvalidOperationException("Не получено меню");
            }                

            foreach(var menuItem in response.Menu)
            {
                _menuDictionary.Add(menuItem.MenuItemId, menuItem);
            }
        }

        if (_menuDictionary.TryGetValue(menuItemId, out var findMenu))    
        {
            return findMenu;
        }

        throw new InvalidOperationException($"Не найдено меню с идентификатором {menuItemId}");
    }    
}

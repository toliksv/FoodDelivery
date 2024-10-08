using System;
using System.Windows.Markup;
using FoodDelivery.Core.Contracts.Application.Commands.Orders;
using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.Core.Contracts.Ports.Common.In.Calendar;
using FoodDelivery.Core.Contracts.Ports.Common.In.OrderIdentificators;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Orders;
using FoodDelivery.Core.Contracts.Ports.Orders.In;
using FoodDelivery.Core.Contracts.Ports.Orders.Out;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace FoodDelivery.Core.Adapters.Orders.In;

internal class OrdersCommandHandler : IOrdersCommandHandler
{
    private readonly IOrdersEventsSourceRepository _ordersEventsSourceRepository;
    private readonly ILogger<OrdersCommandHandler> _logger;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IOrderIdentificatorGenerator _orderIdentificatorGenerator;

    public OrdersCommandHandler(IOrdersEventsSourceRepository ordersEventsSourceRepository, ILogger<OrdersCommandHandler> logger, IDateTimeProvider dateTimeProvider, IOrderIdentificatorGenerator orderIdentificatorGenerator)
    {
        _ordersEventsSourceRepository = ordersEventsSourceRepository ?? throw new ArgumentNullException(nameof(ordersEventsSourceRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        _orderIdentificatorGenerator = orderIdentificatorGenerator ?? throw new ArgumentNullException(nameof(orderIdentificatorGenerator));
    }

    public async Task Handle(CreateOrderCommand createOrderCommand, CancellationToken cancellationToken)
    {
        if (createOrderCommand is null)
        {
            throw new ArgumentNullException(nameof(createOrderCommand));
        }

        var orderId = _orderIdentificatorGenerator.NewOrderId();
        var clientId = createOrderCommand.ClientId;
        var createEvent = new CreateOrderEvent
        {
            OrderId = orderId,
            ClientId = clientId,
            OccurredOn = _dateTimeProvider.Now,
        };
        await _ordersEventsSourceRepository.SaveEvent(createEvent, cancellationToken); 

        // добавляем болюда в заказ
        if (createOrderCommand.Items != null && createOrderCommand.Items.Any())
        {
            var addItemCommand = new AddItemsToOrderCommand(orderId, clientId, createOrderCommand.Items);
            await Handle(addItemCommand, cancellationToken);
        }
        
        // финальным аккордом ставим статус
        var setOrderStatusCommand = new SetOrderStatusCommand(orderId, clientId, OrderStatus.Created);
        await Handle(setOrderStatusCommand, cancellationToken);
    }
    public Task Handle(SetOrderStatusCommand setStatusCommand, CancellationToken cancellationToken)    
    {
        if (setStatusCommand is null)
        {
            throw new ArgumentNullException(nameof(setStatusCommand));
        }

        var setOrderStatusEvent = new SetOrderStatusEvent
        {
            OrderId = setStatusCommand.OrderId,
            ClientId = setStatusCommand.ClientId,
            OccurredOn = _dateTimeProvider.Now,
            NewStatus = setStatusCommand.Status,
        };
        return _ordersEventsSourceRepository.SaveEvent(setOrderStatusEvent, cancellationToken);
    }

    public async Task Handle(AddItemsToOrderCommand addItemsToOrderCommand, CancellationToken cancellationToken)
    {
        if (addItemsToOrderCommand is null)
        {
            throw new ArgumentNullException(nameof(addItemsToOrderCommand));
        }

        foreach (var item in addItemsToOrderCommand.Items)
        {
            if (item.MenuItem?.MenuItemId == null || item.Quantity <= 0)
            {
                _logger.LogWarning("Для блюда {MenuItemId} указано количество указано {Quantity}", item.MenuItem?.MenuItemId.ToString() ?? "не указано", item.Quantity);
                continue;
            }
            var addItemEvent = new AddOrderItemEvent
            {
                OrderId = addItemsToOrderCommand.OrderId,
                ClientId = addItemsToOrderCommand.ClientId,
                OccurredOn = _dateTimeProvider.Now,
                MenuItemId = item.MenuItem?.MenuItemId ?? throw new ArgumentNullException(nameof(item.MenuItem.MenuItemId)),
                Quantity = item.Quantity,
            };

            await _ordersEventsSourceRepository.SaveEvent(addItemEvent, cancellationToken);
        }
    }

    public async Task Handle(RemoveItemsFromOrderCommand removeItemsFromOrderCommand, CancellationToken cancellationToken)
    {
        if (removeItemsFromOrderCommand is null)
        {
            throw new ArgumentNullException(nameof(removeItemsFromOrderCommand));
        }

        foreach(var menuItemId in removeItemsFromOrderCommand.MenuItemsIds)
        {
            var removeItemEvent = new RemoveOrderItemEvent
            {
                OrderId = removeItemsFromOrderCommand.OrderId,
                ClientId =  removeItemsFromOrderCommand.ClientId,
                OccurredOn = _dateTimeProvider.Now,
                MenuItemId = menuItemId,
            };
            await _ordersEventsSourceRepository.SaveEvent(removeItemEvent, cancellationToken);
        }
    }

    public async Task Handle(ChangeOrderItemQuantityCommand changeOrderItemQuantityCommand, CancellationToken cancellationToken)
    {
        if (changeOrderItemQuantityCommand is null)
        {
            throw new ArgumentNullException(nameof(changeOrderItemQuantityCommand));
        }

        foreach (var item in changeOrderItemQuantityCommand.ItemsWithNewQuantity)
        {
            if (item.MenuItem?.MenuItemId == null || item.Quantity <= 0)
            {
                _logger.LogWarning("Для блюда {MenuItemId} указано количество указано {Quantity}", item.MenuItem?.MenuItemId.ToString() ?? "не указано", item.Quantity);
                continue;
            }
            var changeOrderItemQuantityEvent = new ChangeOrderItemQuantityEvent
            {
                OrderId =changeOrderItemQuantityCommand.OrderId, 
                ClientId = changeOrderItemQuantityCommand.ClientId, 
                OccurredOn = _dateTimeProvider.Now, 
                MenuItemId = item.MenuItem?.MenuItemId ?? throw new ArgumentNullException(nameof(item.MenuItem.MenuItemId)),
                NewQuantity = item.Quantity
            };
            await _ordersEventsSourceRepository.SaveEvent(changeOrderItemQuantityEvent, cancellationToken);
        }
    }    
}

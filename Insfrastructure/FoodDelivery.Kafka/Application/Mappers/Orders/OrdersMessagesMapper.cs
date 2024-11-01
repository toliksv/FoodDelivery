using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Orders;
using FoodDelivery.Kafka.Application.Messages.Orders;
using FoodDelivery.Kafka.Contracts.Application.Mappers.Orders;

namespace FoodDelivery.Kafka.Application.Mappers.Orders;

/// <inheritdoc cref="IOrdersMessagesMapper"/>
internal class OrdersMessagesMapper : IOrdersMessagesMapper
{
    public AddOrderItemMessage Map(AddOrderItemEvent source)
    {
        ArgumentNullException.ThrowIfNull(source);

        return new AddOrderItemMessage
        {
            ClientId = source.ClientId,
            MenuItemId = source.MenuItemId,
            OccurredOn = source.OccurredOn.Ticks,
            OrderId = source.OrderId,
            Quantity = source.Quantity,
        };
    }

    public AddOrderItemEvent Map(AddOrderItemMessage source)
    {
        ArgumentNullException.ThrowIfNull(source);

        return new AddOrderItemEvent
        {
            ClientId = source.ClientId,
            MenuItemId = source.MenuItemId,
            OccurredOn = new DateTime(source.OccurredOn),
            OrderId = source.OrderId,
            Quantity = source.Quantity,      
        };
    }

    public ChangeOrderItemQuantityMessage Map(ChangeOrderItemQuantityEvent source)
    {
        ArgumentNullException.ThrowIfNull(source);

        return new ChangeOrderItemQuantityMessage
        {
            ClientId = source.ClientId,
            MenuItemId = source.MenuItemId,
            NewQuantity = source.NewQuantity,
            OccurredOn = source.OccurredOn.Ticks,
            OrderId = source.OrderId,            
        };
    }

    public ChangeOrderItemQuantityEvent Map(ChangeOrderItemQuantityMessage source)
    {
        ArgumentNullException.ThrowIfNull(source);

        return new ChangeOrderItemQuantityEvent
        {
            ClientId = source.ClientId,
            MenuItemId = source.MenuItemId,
            OccurredOn = new DateTime(source.OccurredOn),
            OrderId = source.OrderId
            
        };
    }

    public CreateOrderMessage Map(CreateOrderEvent source)
    {
        ArgumentNullException.ThrowIfNull(source);
        return new CreateOrderMessage
        {
            OrderId = source.OrderId,
            ClientId = source.ClientId,
            OccurredOn = source.OccurredOn.Ticks,
        };
    }

    public CreateOrderEvent Map(CreateOrderMessage source)
    {
        ArgumentNullException.ThrowIfNull(source);
        return new CreateOrderEvent
        {
            OrderId = source.OrderId,
            ClientId = source.ClientId,
            OccurredOn = new DateTime(source.OccurredOn),
        };
    }

    public RemoveOrderItemMessage Map(RemoveOrderItemEvent source)
    {
        ArgumentNullException.ThrowIfNull(source);
        return new RemoveOrderItemMessage
        {
            OrderId = source.OrderId,
            ClientId = source.ClientId,
            OccurredOn = source.OccurredOn.Ticks,
            MenuItemId = source.MenuItemId,
        };
    }

    public RemoveOrderItemEvent Map(RemoveOrderItemMessage source)
    {
        ArgumentNullException.ThrowIfNull(source);
        return new RemoveOrderItemEvent
        {
            OrderId = source.OrderId,
            ClientId = source.ClientId,
            OccurredOn = new DateTime(source.OccurredOn),
            MenuItemId = source.MenuItemId,
        };
    }

    public SetOrderStatusMessage Map(SetOrderStatusEvent source)
    {
        ArgumentNullException.ThrowIfNull(source);
        return new SetOrderStatusMessage
        {
            OrderId = source.OrderId,
            ClientId = source.ClientId,
            OccurredOn = source.OccurredOn.Ticks,
            NewStatus = (int)source.NewStatus,
        };
    }

    public SetOrderStatusEvent Map(SetOrderStatusMessage source)
    {
        ArgumentNullException.ThrowIfNull(source);
        return new SetOrderStatusEvent
        {
            OrderId = source.OrderId,
            ClientId = source.ClientId,
            OccurredOn = new DateTime(source.OccurredOn),
            NewStatus = (OrderStatus)source.NewStatus,
        };
    }   
}

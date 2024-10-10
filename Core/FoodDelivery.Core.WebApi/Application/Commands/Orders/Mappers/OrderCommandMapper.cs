using System;
using FoodDelivery.Core.Contracts.Application.Commands.Orders;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Menu;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Orders;
using FoodDelivery.Core.Contracts.Application.Commands.Orders.Web;
using FoodDelivery.Core.Contracts.Application.Dto.Orders;

namespace FoodDelivery.Core.WebApi.Application.Commands.Orders.Mappers;

internal class OrderCommandMapper : IOrderCommandMapper
{
    public CreateOrderCommand Map(CreateOrderCommandRequest source)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }


        var orderItems = source?.Items.Select(Map).ToList();

        return new CreateOrderCommand(source.ClientId, orderItems);
    }

    public SetOrderStatusCommand Map(SetOrderStatusCommandRequest source)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        return new SetOrderStatusCommand(source.OrderId, source.ClientId, source.Status);
    }

    public AddItemsToOrderCommand Map(AddItemsToOrderCommandRequest source)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (source.OrderItems is null)
        {
            throw new ArgumentNullException(nameof(source.OrderItems));
        }

        var items = source.OrderItems.Select(Map).ToList();
        return new AddItemsToOrderCommand(source.OrderId, source.ClientId, items);
    }

    public RemoveItemsFromOrderCommand Map(RemoveItemsFromOrderCommandRequest source)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (source.MenuItemsIds is null)
        {
            throw new ArgumentNullException(nameof(source.MenuItemsIds));
        }

        return new RemoveItemsFromOrderCommand(source.OrderId, source.ClientId, source.MenuItemsIds);
    }

    public ChangeOrderItemQuantityCommand Map(ChangeOrderItemQuantityCommandRequest source)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (source.ItemsWithNewQuantity is null)
        {
            throw new ArgumentNullException(nameof(source.ItemsWithNewQuantity));            
        }

        var orderItems = source.ItemsWithNewQuantity.Select(Map).ToList();
        return new ChangeOrderItemQuantityCommand(source.OrderId, source.ClientId, orderItems);
    }

    private OrderItem Map(OrderItemDto orderItemDto)
    {
        if (orderItemDto is null)
        {
            throw new ArgumentNullException(nameof(orderItemDto));
        }

        var menuItem = new MenuItem
        {
            MenuItemId = orderItemDto.MenuItemId, 
            Name =orderItemDto.Name, 
            Description = orderItemDto.Description,
            Price = orderItemDto.Price,
        };

        return new OrderItem(menuItem, orderItemDto.Quantity);
    } 
}

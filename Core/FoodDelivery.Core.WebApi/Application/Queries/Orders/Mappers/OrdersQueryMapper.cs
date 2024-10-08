using System;
using FoodDelivery.Core.Contracts.Application.Queries.Orders;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Orders;
using FoodDelivery.Core.Contracts.Application.Dto.Orders;
using FoodDelivery.Core.Contracts.Application.Queries.Orders.Web;

namespace FoodDelivery.Core.WebApi.Application.Queries.Orders.Mappers;

internal class OrdersQueryMapper : IOrdersQueryMapper
{
    public GetOrderByIdResponse Map(GetOrderByIdQueryResponse source)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        return new GetOrderByIdResponse
        {
            Order = Map(source.Order),
        };
    }

    public GetClientOrdersResponse Map(GetClientOrdersQueryResponse source)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        return new GetClientOrdersResponse
        {
            ClientOrders = source.Orders?.Select(Map).ToList() ?? throw new ArgumentNullException(nameof(source.Orders)),
        };  
    }

    private OrderDto Map(Order order)
    {
        if (order is null)
        {
            throw new ArgumentNullException(nameof(order));
        }

        var orderDto = new OrderDto
        {
            OrderId = order.OrderId,
            ClientId = order.ClientId,
            OrderStatus = order.Status,            
        };

        if (order.Items is not null)
        {
            orderDto.OrderItems.AddRange(order.Items.Select(Map));
        }

        return orderDto; 
    }

    private OrderItemDto Map(OrderItem source)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        return new OrderItemDto
        {
            MenuItemId = source.MenuItem?.MenuItemId ?? throw new ArgumentNullException(nameof(source.MenuItem)),
            Name = source.MenuItem.Name,
            Description = source.MenuItem.Description,
            Price = source.MenuItem.Price,
            Quantity = source.Quantity,
        };
    }


}

using System;

namespace FoodDelivery.UI..Console.Ports.Out;

/// <summary>
/// Контроллер для RestEase с запросами к Core.WebApi
/// </summary>
/// <remarks>
/// 
///     [Post("command/create")]
///     public  Task CreateOrder([Body]CreateOrderCommandRequest createOrderCommandRequest, CancellationToken cancellationToken)

        // [Put("command/set-status")]
        // public  Task SetOrderStatus([Body]SetOrderStatusCommandRequest setOrderStatusCommandRequest, CancellationToken cancellationToken)
        
        // [Post("command/add-items")]
        // public  Task AddItemsToOrder([Body]AddItemsToOrderCommandRequest addItemsToOrderCommandRequest, CancellationToken cancellationToken)

        // [Delete("command/remove-items")]
        // public  Task RemoveItemOrder([Body]RemoveItemsOrderCommandRequest removeItemsOrderCommandRequest, CancellationToken cancellationToken)

        // [Put("command/change-item-quantity")]
        // public  Task ChangeOrderItemQuantity([Body]ChangeOrderItemQuantityCommandRequest changeOrderItemQuantityCommandRequest, CancellationToken cancellationToken)

        // [Get("query/by-order-id/{orderId}")]        
        // public  Task GetOrderByOrderId([Route()]int orderId, CancellationToken cancellationToken)

        // [Get("query/by-client-id/{clientId}")]
        // public  Task GetOrdersByClientId([Route]int clientId, CancellationToken cancellationToken)
 
/// </remarks>
public interface IOrderController
{

}

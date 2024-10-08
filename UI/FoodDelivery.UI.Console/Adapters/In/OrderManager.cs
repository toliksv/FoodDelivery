using System;
using FoodDelivery.UI.Console.Ports.In;

namespace FoodDelivery.UI.Console.Adapters.In;

internal class OrderManager : IOrderManager
{
    public Task AddOrder(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task EditOrder(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task PayForOrder(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

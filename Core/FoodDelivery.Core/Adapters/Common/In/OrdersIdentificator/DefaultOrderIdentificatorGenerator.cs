using System;
using FoodDelivery.Core.Contracts.Ports.Common.In.OrderIdentificators;

namespace FoodDelivery.Core.Port.Common.In.OrderIdentificators;

internal class DefaultOrderIdentificatorGenerator : IOrderIdentificatorGenerator
{
    private readonly Random _random;

    public DefaultOrderIdentificatorGenerator()
    {
        _random = new Random();
    }

    public int NewOrderId()
        =>_random.Next(0,10000000);    
}

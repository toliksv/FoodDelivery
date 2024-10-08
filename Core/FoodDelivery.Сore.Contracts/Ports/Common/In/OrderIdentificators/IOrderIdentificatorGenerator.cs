using System;

namespace FoodDelivery.Core.Contracts.Ports.Common.In.OrderIdentificators;

/// <summary>
/// Генератор идентификаторов для заказа.
/// </summary>
public interface IOrderIdentificatorGenerator
{
    /// <summary>
    /// Новый идентификатор.
    /// </summary>
    /// <returns></returns>
    int NewOrderId();
}

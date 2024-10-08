using System;

namespace FoodDelivery.Core.Contracts.Application.Queries.Orders;

/// <summary>
/// Получить все заказы клиента.
/// </summary>
public class GetClientOrdersQuery
{
    public GetClientOrdersQuery(int clientId)
    {
        ClientId = clientId;
    }

    public int ClientId { get; }
}

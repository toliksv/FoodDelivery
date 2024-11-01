namespace FoodDelivery.Core.Contracts.Application.Commands.Orders.Web;

/// <summary>
///     Базовый класс для REST представления команд.
/// </summary>
public abstract class OrderCommandRequestBase
{
    /// <summary>
    ///     Идентификатор клиента.
    /// </summary>
    public int ClientId { get; set; }
}
namespace FoodDelivery.Kafka.Application.Headers.Orders;

/// <summary>
/// Ключи заголовков для сообщений Kafka.
/// </summary>
internal static class OrderEventHeadersKeys
{
    /// <summary>
    /// Тип пересылаемого сообщения.
    /// </summary>
    public const string OrderEventTypeKey = "order-event-type";
}
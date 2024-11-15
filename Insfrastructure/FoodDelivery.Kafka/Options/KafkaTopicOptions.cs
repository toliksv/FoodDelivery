namespace FoodDelivery.Kafka.Options;

/// <summary>
/// Опции для топиков кафки.
/// </summary>
public class KafkaTopicOptions
{
    /// <summary>
    /// Имя топика событий заказа.
    /// </summary>
    public string OrderEventsTopic { get; set; }
}
using Avro.Specific;

namespace FoodDelivery.Kafka.Common;

/// <summary>
/// Реализация серисализации контрактов посредством Avro
/// </summary>
public interface IMessageSerializer
{
    /// <summary>
    /// Сериализация сообщения.
    /// </summary>
    /// <param name="serializingObject">объект сериализации.</param>
    /// <typeparam name="TSerialize">сериализируемый тип.</typeparam>
    /// <returns>массив байтов сериализированного объекта.</returns>
    byte[] Serialize<TSerialize>(TSerialize serializingObject)
        where TSerialize : class, ISpecificRecord;

    /// <summary>
    /// Десериализация сообщения.
    /// </summary>
    /// <param name="serializedObject">массив байтов сериализированного объекта.</param>
    /// <typeparam name="TDeserialized">тип для десериализации.</typeparam>
    /// <returns>десериализируемый объект.</returns>
    TDeserialized Deserialize<TDeserialized>(byte[] serializedObject)
        where TDeserialized : class, ISpecificRecord, new(); 
}
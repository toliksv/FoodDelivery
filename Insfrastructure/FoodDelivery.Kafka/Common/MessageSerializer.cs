using Avro.Generic;
using Avro.IO;
using Avro.Specific;

namespace FoodDelivery.Kafka.Common;

internal class MessageSerializer : IMessageSerializer
{
    public byte[] Serialize<TSerialize>(TSerialize serializingObject) where TSerialize : class, ISpecificRecord
    {
        ArgumentNullException.ThrowIfNull(serializingObject);
        using (var str = new MemoryStream())
        {
            var writer = new BinaryEncoder(str);
            var datumWriter = new SpecificDatumWriter<TSerialize>(serializingObject.Schema);
            datumWriter.Write(serializingObject, writer);
            return str.ToArray();
        }
    }

    public TDeserialized Deserialize<TDeserialized>(byte[] serializedObject)
        where TDeserialized : class, ISpecificRecord, new()
    {
        ArgumentNullException.ThrowIfNull(serializedObject);
        using (var stream = new MemoryStream(serializedObject))
        {
            var schemaDef = new TDeserialized().Schema;
            var reader = new BinaryDecoder(stream);
            var datumReader = new SpecificDatumReader<TDeserialized>(schemaDef, schemaDef);

            // Чтение и десериализация данных
            return datumReader.Read(null, reader);
        }
    }
}
using System.Threading.Tasks;
using Avro.Specific;
using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;

namespace SchemaRegistry.Producers
{
    public class KafkaEventProducer: IEventProducer
    {
        private readonly IProducer<string,ISpecificRecord> _producer;

        public KafkaEventProducer(ProducerConfig config, SchemaRegistryConfig schemaRegistryConfig)
        {
            var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);
            _producer = new ProducerBuilder<string, ISpecificRecord>(config)
                .SetKeySerializer(new AvroSerializer<string>(schemaRegistry))
                .SetValueSerializer(new AvroSerializer<ISpecificRecord>(schemaRegistry))
                .Build();
        }
        
        public Task AddEventAsync(string topic, string key, ISpecificRecord eventRecord)
        {
            return _producer.ProduceAsync(topic,new Message<string, ISpecificRecord>()
            {
                Key = key,
                Value = eventRecord
            });
        }
    }
}
using System.Threading.Tasks;
using Avro.Specific;

namespace SchemaRegistry.Producers
{
    public interface IEventProducer
    {
        Task AddEventAsync(string topic, string key, ISpecificRecord eventRecord);
    }
}
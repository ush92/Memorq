using Memorq.Models;

namespace Memorq.Infrastructure
{
    public interface IJsonConverter
    {
        public string Serialize(object item);
        public object Deserialize(string json);
    }
}
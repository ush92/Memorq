using Memorq.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorq.Infrastructure
{
    public class JsonConverter : IJsonConverter
    {
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public object Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<object>(json);
        }
    }
}

using System.IO;
using SQL2JSON.Core;
using Newtonsoft.Json;

namespace SQL2JSON.Infrastructure
{
    public class JsonDotNetSerializer : IJSONSerializer 
    {
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        public void Serialize(object obj, StreamWriter writer)
        {
            using (JsonWriter jw = new JsonTextWriter(writer))
            {
                jw.Formatting = Formatting.Indented;
                new JsonSerializer().Serialize(jw, obj);
            }
        }
    }
}
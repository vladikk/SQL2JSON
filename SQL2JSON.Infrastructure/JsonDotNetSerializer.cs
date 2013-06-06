using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQL2JSON.Core;
using Newtonsoft.Json;

namespace SQL2JSON.Infrastructure
{
    public class JsonDotNetSerializer : IJSONSerializer 
    {
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQL2JSON.Core
{
    public class ObjectTreeBuilder
    {
        public Dictionary<string, object> Split(Dictionary<string, object> source, string delimeter)
        {
            var result = new Dictionary<string, object>();
            foreach (var key in source.Keys)
            {
                var dict = result;
                List<string> keys = key.Split(new [] { delimeter }, StringSplitOptions.RemoveEmptyEntries).ToList();
                do
                {
                    if (keys.Count == 1)
                    {
                        dict[keys[0]] = source[key];
                    }
                    else
                    {
                        var newDict = dict.ContainsKey(keys[0]) ? (Dictionary<string, object>)dict[keys[0]] : new Dictionary<string, object>();
                        dict[keys[0]] = newDict;
                        dict = newDict;
                    }
                    keys.RemoveAt(0);
                } while (keys.Count > 0);
            }

            return result;
        }
    }
}

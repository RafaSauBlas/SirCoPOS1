using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SirCoPOS.BusinessLogic.Helpers
{
    public class Serializer
    {                
        public static T Deserialize<T>(string data)
        {
            var jser = new DataContractSerializer(typeof(T));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(data));
            var res = (T)jser.ReadObject(ms);
            return res;
        }
        public static string Serialize<T>(T data)
        {
            var jser = new DataContractSerializer(typeof(T));
            var ms = new MemoryStream();
            jser.WriteObject(ms, data);
            var res = Encoding.UTF8.GetString(ms.ToArray());
            return res;
        }
        public static T JsonDeserialize<T>(string data)
        {
            var jser = new DataContractJsonSerializer(typeof(T));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(data));
            var res = (T)jser.ReadObject(ms);
            return res;
        }
        public static string JsonSerialize<T>(T data)
        {
            var jser = new DataContractJsonSerializer(typeof(T));
            var ms = new MemoryStream();
            jser.WriteObject(ms, data);
            var res = Encoding.UTF8.GetString(ms.ToArray());
            return res;
        }
    }
}

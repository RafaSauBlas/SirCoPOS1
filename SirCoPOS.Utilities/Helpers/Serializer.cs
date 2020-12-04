using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using io = System.IO;
using s = System.Xml.Serialization;


namespace SirCoPOS.Utilities.Helpers
{
    public class Serializer
    {
        public static T XmlDeserialize<T>(string data)
        {
            if (data == null)
                return default(T);
            var xser = new s.XmlSerializer(typeof(T));
            var sr = new io.StringReader(data);
            var res = (T)xser.Deserialize(sr);
            return res;
        }
        public static string XmlSerialize<T>(T data)
        {
            var xser = new s.XmlSerializer(typeof(T));
            var sr = new io.StringWriter();
            xser.Serialize(sr, data);
            var res = sr.ToString();
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
        public static T Deserialize<T>(string data)
        {
            if (data == null)
                return default(T);
            var dser = new DataContractSerializer(typeof(T));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(data));
            var res = (T)dser.ReadObject(ms);
            return res;
        }
        public static string Serialize<T>(T data)
        {
            var dser = new DataContractSerializer(typeof(T));
            var ms = new MemoryStream();
            dser.WriteObject(ms, data);
            var res = Encoding.UTF8.GetString(ms.ToArray());
            return res;
        }
    }
}

using System;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.IO;

namespace Task_1
{
    class Convert
    {
        public static void ToXml(object obj, string path)
        {
            var serializer = new XmlSerializer(obj.GetType());

            using (var stream = new StreamWriter(path))
            {
                serializer.Serialize(stream, obj);
            }
        }

        public static void FromXml<T>(string path, out T obj)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var stream = new StreamReader(path))
            {
                obj = (T)serializer.Deserialize(stream);
            }
        }

        public static void ToJson(object obj, string path)
        {
            using (var stream = new StreamWriter(path))
            {
                JsonConvert.SerializeObject(obj);
            }
        }

        public static void FromJson<T>(string path, out T obj)
        {
            using (var stream = new StreamReader(path))
            {
                obj = JsonConvert.DeserializeObject<T>(stream.ReadToEnd());
            }
        }
    }
}

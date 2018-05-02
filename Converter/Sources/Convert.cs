using System.Xml.Serialization;
using Newtonsoft.Json;
using System.IO;

namespace Lesson_2
{
    public class Convert
    {
        public static void ToXml(object obj, string path)
        {
            var serializer = new XmlSerializer(obj.GetType());

            using (var stream = new StreamWriter(path))
            {
                serializer.Serialize(stream, obj);
            }
        }

        public static T FromXml<T>(string path)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var stream = new StreamReader(path))
            {
                return (T)serializer.Deserialize(stream);
            }
        }

        public static void ToJson(object obj, string path)
        {
            using (var stream = new StreamWriter(path))
            {
                stream.Write(JsonConvert.SerializeObject(obj, Formatting.Indented));
            }
        }

        public static T FromJson<T>(string path)
        {
            using (var stream = new StreamReader(path))
            {
                return JsonConvert.DeserializeObject<T>(stream.ReadToEnd());
            }
        }
    }
}

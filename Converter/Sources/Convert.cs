// MIT License
// 
// Copyright(c) 2018 Alexander Popelyuk
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.


using System.Xml.Serialization;
using Newtonsoft.Json;
using System.IO;


namespace Lesson_2
{
    //
    // Summary:
    //   Serialize and deserialize objects to/from XML and JSON formats.
    //   Only file system serialization/deserialization is supported.
    public class Convert
    {
        //
        // Summary:
        //   Serialize object to XML format.
        //
        // Parameters:
        //   obj:
        //     Object to serialize.
        // 
        //   path:
        //     File path to which serialized object will be written.
        public static void ToXml(object obj, string path)
        {
            var serializer = new XmlSerializer(obj.GetType());

            using (var stream = new StreamWriter(path))
            {
                serializer.Serialize(stream, obj);
            }
        }
        //
        // Summary:
        //   Deserialize object in XML format.
        //
        // Parameters:
        //   path:
        //     Path to the object to deserialize.
        //
        // Return:
        //   Deserialized object.
        public static T FromXml<T>(string path)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var stream = new StreamReader(path))
            {
                return (T)serializer.Deserialize(stream);
            }
        }
        //
        // Summary:
        //   Serialize object to JSON format.
        //
        // Parameters:
        //   obj:
        //     Object to serialize.
        // 
        //   path:
        //     File path to which serialized object will be written.
        public static void ToJson(object obj, string path)
        {
            using (var stream = new StreamWriter(path))
            {
                stream.Write(JsonConvert.SerializeObject(obj, Formatting.Indented));
            }
        }
        //
        // Summary:
        //   Deserialize object in JSON format.
        //
        // Parameters:
        //   path:
        //     Path to the object to deserialize.
        //
        // Return:
        //   Deserialized object.
        public static T FromJson<T>(string path)
        {
            using (var stream = new StreamReader(path))
            {
                return JsonConvert.DeserializeObject<T>(stream.ReadToEnd());
            }
        }
    }
}

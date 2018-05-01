using System;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace Task_2
{
    class Operation
    {
        public enum Type
        {
            [XmlEnum(Name = "income")]
            Debit,
            [XmlEnum(Name = "expense")]
            Credit,
        }

        [XmlAttribute("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Type OperationType;
        public Decimal Amount;
        public DateTime Date;
    }
}

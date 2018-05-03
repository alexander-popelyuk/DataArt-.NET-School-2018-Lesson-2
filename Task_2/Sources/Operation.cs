using System;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace Task_2
{
    [XmlRoot("operation")]
    public class Operation
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

        public override string ToString()
        {
            return string.Format("Date: {0}, Type: {1}, Amount: {2}",
                Date, Enum.GetName(typeof(Operation.Type), OperationType), Amount);
        }
    }
}

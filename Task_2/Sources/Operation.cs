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

        public override string ToString()
        {
            return string.Format("Type: {0}, Amount: {1}, Date: {2}",
                Enum.GetName(typeof(Operation.Type), OperationType), Amount, Date);
        }
    }
}

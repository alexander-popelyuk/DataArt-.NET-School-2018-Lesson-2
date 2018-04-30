using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1.NewClient
{
    public class Address
    {
        public string StreetName;
        public string HouseNumber;
        public string 
    }

    public class Client
    {
        [XmlElement("fn")]
        public string FirstName;
        [XmlElement("ln")]
        public string LastName;
        [XmlElement("mn")]
        public string MiddleName;
        [XmlElement("p")]
        public string PhoneNumber;
        [XmlElement("e")]
        public string Email;
        [XmlElement("bd")]
        public byte BirthDay;
        [XmlElement("bm")]
        public byte BirthMonth;
        [XmlElement("by")]
        public uint BirthYear;
        [XmlElement("hl1")]
        public string HomeAddressLine1;
        [XmlElement("hc")]
        public string HomeAddressCity;
        [XmlElement("hs")]
        public string HomeAddressState;
        [XmlElement("hz")]
        public string HomeAddressZip;
        [XmlElement("wl1")]
        public string WorkAddressLine1;
        [XmlElement("wc")]
        public string WorkAddressCity;
        [XmlElement("ws")]
        public string WorkAddressState;
        [XmlElement("wz")]
        public string WorkAddressZip;
    }
}

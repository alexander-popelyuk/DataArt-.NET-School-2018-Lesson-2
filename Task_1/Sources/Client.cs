using System;
using System.Xml.Serialization;

namespace Task_1.OldFormat
{
    [XmlRoot("cl")]
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

namespace Task_1.NewFormat
{
    public class Address
    {
        public string HouseNumber;
        public string StreetName;
        public string CityName;
        public string StateName;
        public string ZipCode;

        public Address()
        {
        }

        public Address(string line1, string city, string state, string zip)
        {
            var line1_fields = line1.Split(',');
            if (line1_fields.Length != 2) throw new InvalidCastException("Wrong address format.");
            this.HouseNumber = line1_fields[1].Trim();
            this.StreetName = line1_fields[0].Trim();
            this.CityName = city;
            this.StateName = state;
            this.ZipCode = zip;
        }
    }

    public class Client
    {
        public string FirstName;
        public string LastName;
        public string MiddleName;
        public DateTime BirthDate;
        public string PhoneNumber;
        public string Email;
        public Address HomeAddress;
        public Address WorkAddress;

        public Client()
        {
        }

        public Client(OldFormat.Client client)
        {
            this.FirstName = client.FirstName;
            this.LastName = client.LastName;
            this.MiddleName = client.MiddleName;
            this.BirthDate = new DateTime((int)client.BirthYear, client.BirthMonth, client.BirthDay);
            this.PhoneNumber = client.PhoneNumber;
            this.Email = client.Email;
            this.HomeAddress = new Address(client.HomeAddressLine1, client.HomeAddressCity,
                client.HomeAddressState, client.HomeAddressZip);
            this.WorkAddress = new Address(client.WorkAddressLine1, client.WorkAddressCity,
                client.WorkAddressState, client.WorkAddressZip);
        }

        public static implicit operator Client(OldFormat.Client client)
        {
            return new Client(client);
        }
    }
}

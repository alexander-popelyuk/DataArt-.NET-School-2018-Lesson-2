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


using System;
using System.Xml.Serialization;


namespace Task_1.OldFormat
{
    //
    // Summary:
    //   Old format client representation.
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
        public ushort BirthYear;
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
    //
    // Summary:
    //   New format client address.
    public class Address
    {
        public string HouseNumber;
        public string StreetName;
        public string CityName;
        public string StateName;
        public string ZipCode;
        //
        // Summary:
        //   Default constructor, required by serialization/deserialization routines.
        public Address()
        {
        }
        //
        // Summary:
        //   Non default constructor, used to convert address from old client.
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
    //
    // Summary:
    //   New format client birth date.
    public class Date
    {
        public byte Day;
        public byte Month;
        public ushort Year;
        //
        // Summary:
        //   Default constructor, required by serialization/deserialization routines.
        public Date()
        {
        }
        //
        // Summary:
        //   Non default constructor, used to convert date from old client.
        public Date(byte day, byte month, ushort year)
        {
            this.Day = day;
            this.Month = month;
            this.Year = year;
        }
    }
    //
    // Summary:
    //   New format client representation.
    public class Client
    {
        public string FirstName;
        public string LastName;
        public string MiddleName;
        public Date BirthDate;
        public string PhoneNumber;
        public string Email;
        public Address HomeAddress;
        public Address WorkAddress;
        //
        // Summary:
        //   Default constructor, required by serialization/deserialization routines.
        public Client()
        {
        }
        //
        // Summary:
        //   Non default constructor, used to convert from old client format.
        public Client(OldFormat.Client client)
        {
            this.FirstName = client.FirstName;
            this.LastName = client.LastName;
            this.MiddleName = client.MiddleName;
            this.BirthDate = new Date(client.BirthDay, client.BirthMonth, client.BirthYear);
            this.PhoneNumber = client.PhoneNumber;
            this.Email = client.Email;
            this.HomeAddress = new Address(client.HomeAddressLine1, client.HomeAddressCity,
                client.HomeAddressState, client.HomeAddressZip);
            this.WorkAddress = new Address(client.WorkAddressLine1, client.WorkAddressCity,
                client.WorkAddressState, client.WorkAddressZip);
        }
        //
        // Summary:
        //   Implicit assignment, used to convert from old client format.
        public static implicit operator Client(OldFormat.Client client)
        {
            return new Client(client);
        }
    }
}

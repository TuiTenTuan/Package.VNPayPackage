using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VNPayPackage.Enums;
using VNPayPackage.Ulits;

namespace VNPayPackage.Models
{
    public class VnpBill
    {
        public string Mobile { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public VnpBill(string name, string mobile, string email, string address, string city, string country, string state)
        {
            Mobile = mobile;
            Email = email;
            Name = name;
            Address = address;
            City = city;
            Country = country;
            State = state;
        }

        public VnpBill(string name, string mobile, string email, string address, string city, string country) : this(name, mobile, email, address, city, country, "") { }

        public SortedList<string, string> ConvertToSortedList()
        {
            SortedList<string, string> result = new SortedList<string, string>(new VnPayCompare());

            result.Add("vnp_Bill_Mobile", WebUtility.UrlEncode(Mobile));
            result.Add("vnp_Bill_Email", WebUtility.UrlEncode(Email));
            result.Add("vnp_Bill_Address", WebUtility.UrlEncode(Address));
            result.Add("vnp_Bill_City", WebUtility.UrlEncode(City));
            result.Add("vnp_Bill_Country", WebUtility.UrlEncode(Country));
            result.Add("vnp_Bill_State", WebUtility.UrlEncode(State));

            if (!string.IsNullOrEmpty(Name))
            {
                string[] subName = Functions.SplitVietNamName(Name);

                result.Add("vnp_Bill_FirstName", WebUtility.UrlEncode(subName[1]));
                result.Add("vnp_Bill_LastName", WebUtility.UrlEncode(subName[0]));
            }

            return result;
        }

        public SortedList<string, string> ConvertToSortedList(SortedList<string, string> arrayInput)
        {
            arrayInput.Add("vnp_Bill_Mobile", WebUtility.UrlEncode(Mobile));
            arrayInput.Add("vnp_Bill_Email", WebUtility.UrlEncode(Email));
            arrayInput.Add("vnp_Bill_Address", WebUtility.UrlEncode(Address));
            arrayInput.Add("vnp_Bill_City", WebUtility.UrlEncode(City));
            arrayInput.Add("vnp_Bill_Country", WebUtility.UrlEncode(Country));
            arrayInput.Add("vnp_Bill_State", WebUtility.UrlEncode(State));

            if (!string.IsNullOrEmpty(Name))
            {
                string[] subName = Functions.SplitVietNamName(Name);

                arrayInput.Add("vnp_Bill_FirstName", WebUtility.UrlEncode(subName[1]));
                arrayInput.Add("vnp_Bill_LastName", WebUtility.UrlEncode(subName[0]));
            }

            return arrayInput;
        }

    }
}

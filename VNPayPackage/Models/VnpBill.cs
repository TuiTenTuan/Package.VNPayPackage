using System.Net;
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

            result.Add("vnp_Bill_Mobile", Mobile);
            result.Add("vnp_Bill_Email", Email);
            result.Add("vnp_Bill_Address", Address);
            result.Add("vnp_Bill_City", City);
            result.Add("vnp_Bill_Country", Country);
            result.Add("vnp_Bill_State", State);

            if (!string.IsNullOrEmpty(Name))
            {
                string[] subName = Functions.SplitVietNamName(Name);

                result.Add("vnp_Bill_FirstName", subName[1]);
                result.Add("vnp_Bill_LastName", subName[0]);
            }

            return result;
        }

        public SortedList<string, string> ConvertToSortedList(SortedList<string, string> arrayInput)
        {
            arrayInput.Add("vnp_Bill_Mobile", Mobile);
            arrayInput.Add("vnp_Bill_Email", Email);
            arrayInput.Add("vnp_Bill_Address", Address);
            arrayInput.Add("vnp_Bill_City", City);
            arrayInput.Add("vnp_Bill_Country", Country);
            arrayInput.Add("vnp_Bill_State", State);

            if (!string.IsNullOrEmpty(Name))
            {
                string[] subName = Functions.SplitVietNamName(Name);

                arrayInput.Add("vnp_Bill_FirstName", subName[1]);
                arrayInput.Add("vnp_Bill_LastName", subName[0]);
            }

            return arrayInput;
        }

        public string ConvertToUrlParameter()
        {
            return Functions.CovertToUrlParameter(ConvertToSortedList());
        }

    }
}

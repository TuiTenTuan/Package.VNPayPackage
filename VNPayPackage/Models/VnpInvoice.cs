using System.Net;
using VNPayPackage.Enums;
using VNPayPackage.Extensions;
using VNPayPackage.Ulits;

namespace VNPayPackage.Models
{
    public class VnpInvoice
    {
        public string Phone { get; set; }

        public string Email { get; set; }

        public string CustomerName { get; set; }

        public string Address { get; set; }

        public string CompanyName { get; set; }

        public string TaxCode { get; set; }

        public TaxType TaxType { get; set; }

        public VnpInvoice(string customerName, string phone, string email, string address, string companyName, string taxCode, TaxType taxType)
        {
            Phone = phone;
            Email = email;
            CustomerName = customerName;
            Address = address;
            CompanyName = companyName;
            TaxCode = taxCode;
            TaxType = taxType;
        }

        public VnpInvoice(string customerName, string phone, string email, string address, string company, string taxCode) : this(customerName, phone, email, address, company, taxCode, TaxType.Persion) { }

        public SortedList<string, string> ConvertToSortedList()
        {
            SortedList<string, string> result = new SortedList<string, string>(new VnPayCompare());

            result.Add("vnp_Inv_Customer", CustomerName);
            result.Add("vnp_Inv_Phone", Phone);
            result.Add("vnp_Inv_Email", Email);
            result.Add("vnp_Inv_Address", Address);
            result.Add("vnp_Inv_Company", CompanyName);
            result.Add("vnp_Inv_Taxcode", TaxCode);
            result.Add("vnp_Inv_Type", TaxType.GetValue());

            return result;
        }

        public SortedList<string, string> ConvertToSortedList(SortedList<string, string> arrayInput)
        {
            arrayInput.Add("vnp_Inv_Customer", CustomerName);
            arrayInput.Add("vnp_Inv_Phone", Phone);
            arrayInput.Add("vnp_Inv_Email", Email);
            arrayInput.Add("vnp_Inv_Address", Address);
            arrayInput.Add("vnp_Inv_Company", CompanyName);
            arrayInput.Add("vnp_Inv_Taxcode", TaxCode);
            arrayInput.Add("vnp_Inv_Type", TaxType.GetValue());

            return arrayInput;
        }

        public string ConvertToUrlParameter()
        {
            return Functions.CovertToUrlParameter(ConvertToSortedList());
        }
    }
}

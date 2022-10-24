using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using VNPayPackage.Enums;
using VNPayPackage.Extensions;
using VNPayPackage.Ulits;

namespace VNPayPackage.Models
{
    public class VnpPay
    {
        public string Version { get; set; }

        public VNPCommand Command { get; set; }

        public string TmnCode { get; set; }

        public decimal Amount { get; set; }

        public string BankCode { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ExpireDate { get; set; }

        public Currency Currency { get; set; }

        public IPAddress IpGuest { get; set; }

        public Language Language { get; set; }

        public string OrderInfo { get; set; }

        public OrderType OrderType { get; set; }

        public string ReturnUrl { get; set; }

        public string TxnRef { get; set; }

        public VnpInvoice Invoice { get; set; }

        public VnpBill Bill { get; set; }


        public VnpPay(string version, VNPCommand command, string tmnCode, decimal amount, string bankCode, DateTime createDate, DateTime expireDate, Currency currency, IPAddress ipGuest, Language language, string orderInfo, OrderType orderType, string returnUrl, string txnRef, VnpInvoice invoice, VnpBill bill)
        {
            Version = version;
            Command = command;
            TmnCode = tmnCode;
            Amount = amount;
            BankCode = bankCode;
            CreateDate = createDate;
            ExpireDate = expireDate;
            Currency = currency;
            IpGuest = ipGuest;
            Language = language;
            OrderInfo = orderInfo;
            OrderType = orderType;
            ReturnUrl = returnUrl;
            TxnRef = txnRef;
            Invoice = invoice;
            Bill = bill;
        }

        public VnpPay(VNPCommand command, string tmnCode, decimal amount, string bankCode, DateTime createDate, DateTime expireDate, Currency currency, IPAddress ipGuest, Language language, string orderInfo, OrderType orderType, string returnUrl, string txnRef) : this("2.1.0", command, tmnCode, amount, bankCode, createDate, expireDate, currency, ipGuest, language, orderInfo, orderType, returnUrl, txnRef, null, null) { }

        public VnpPay(VNPCommand command, string tmnCode, decimal amount, DateTime createDate, DateTime expireDate, Currency currency, IPAddress ipGuest, Language language, string orderInfo, OrderType orderType, string returnUrl, string txnRef) : this(command, tmnCode, amount, "", createDate, expireDate, currency, ipGuest, language, orderInfo, orderType, returnUrl, txnRef) { }

        public VnpPay(VNPCommand command, string tmnCode, decimal amount, DateTime createDate, Currency currency, IPAddress ipGuest, Language language, string orderInfo, OrderType orderType, string returnUrl, string txnRef) : this(command, tmnCode, amount, "", createDate, createDate.AddMinutes(15), currency, ipGuest, language, orderInfo, orderType, returnUrl, txnRef) { }

        public VnpPay(VNPCommand command, string tmnCode, decimal amount, Currency currency, IPAddress ipGuest, Language language, string orderInfo, OrderType orderType, string returnUrl, string txnRef) : this(command, tmnCode, amount, DateTime.Now, currency, ipGuest, language, orderInfo, orderType, returnUrl, txnRef) { }

        public VnpPay(VNPCommand command, string tmnCode, decimal amount, Currency currency, IPAddress ipGuest, Language language, string orderInfo, string returnUrl, string txnRef) : this(command, tmnCode, amount, DateTime.Now, currency, ipGuest, language, orderInfo, OrderType.Other, returnUrl, txnRef) { }

        public VnpPay(VNPCommand command, string tmnCode, decimal amount, IPAddress ipGuest, string orderInfo, string returnUrl, string txnRef) : this(command, tmnCode, amount, DateTime.Now, Currency.VND, ipGuest, Language.Vietnamese, orderInfo, OrderType.Other, returnUrl, txnRef) { }

        public string GetCheckSum(string key, string dataInput)
        {
            StringBuilder result = new StringBuilder();

            result.Append(Functions.HmacSHA512(key, dataInput));

            return result.ToString();
        }

        public SortedList<string, string> CovertToSortedList()
        {
            SortedList<string, string> result = new SortedList<string, string>(new VnPayCompare());

            result.Add("vnp_Version", WebUtility.UrlEncode(Version));
            result.Add("vnp_Command", WebUtility.UrlEncode(Command.GetValue()));
            result.Add("vnp_TmnCode", WebUtility.UrlEncode(TmnCode));
            result.Add("vnp_Amount", WebUtility.UrlEncode(Math.Round(Amount * 100, 0, MidpointRounding.ToEven).ToString()));
            result.Add("vnp_BankCode", WebUtility.UrlEncode(BankCode));
            result.Add("vnp_CreateDate", WebUtility.UrlEncode(CreateDate.ToString("yyyyMMddHHmmss")));
            result.Add("vnp_CurrCode", WebUtility.UrlEncode(Currency.GetValue()));
            result.Add("vnp_IpAddr", WebUtility.UrlEncode(IpGuest.MapToIPv4().ToString()));
            result.Add("vnp_Locale", WebUtility.UrlEncode(Language.GetValue()));
            result.Add("vnp_OrderInfo", WebUtility.UrlEncode(OrderInfo));
            result.Add("vnp_OrderType", WebUtility.UrlEncode(OrderType.ToString()));
            result.Add("vnp_ReturnUrl", WebUtility.UrlEncode(ReturnUrl));
            result.Add("vnp_TxnRef", WebUtility.UrlEncode(TxnRef));
            result.Add("vnp_ExpireDate", WebUtility.UrlEncode(ExpireDate.ToString("yyyyMMddHHmmss")));

            return result;
        }

        public string ConvertToUrlParameter()
        {
            SortedList<string, string> dataConvert = CovertToSortedList();

            if (Invoice != null)
            {
                dataConvert = Invoice.ConvertToSortedList(dataConvert);
            }

            if (Bill != null)
            {
                dataConvert = Bill.ConvertToSortedList(dataConvert);
            }

            StringBuilder result = new StringBuilder();

            foreach (KeyValuePair<string, string> item in dataConvert)
            {
                if (!String.IsNullOrEmpty(item.Value))
                {
                    result.Append(item.Key).Append("=").Append(item.Value).Append("&");
                }
            }

            if (result.Length > 0)
            {
                result = result.Remove(result.Length - 1, 1);
            }

            return result.ToString();
        }

        public string CreatePayUrl(string baseUrl, string key)
        {
            StringBuilder result = new StringBuilder();

            string parameter = ConvertToUrlParameter();

            result.Append(baseUrl).Append("?");

            result.Append(parameter).Append("&vnp_SecureHash=").Append(GetCheckSum(key, parameter));

            return result.ToString();
        }

    }
}

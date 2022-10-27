using System.Net;
using VNPayPackage.Enums;
using VNPayPackage.Ulits;

namespace VNPayPackage.Models
{
    public class VnpResultPay
    {
        public string TmnCode { get; set; }

        public decimal Amount { get; set; }

        public string BankCode { get; set; }

        public string BankTranNo { get; set; }

        public CardType CardType { get; set; }

        public DateTime PayDate { get; set; }

        public string OrderInfo { get; set; }

        public long TransactionNo { get; set; }

        public ResponseCode ResponseCode { get; set; }

        public TransactionStatusCode TransactionStatus { get; set; }

        public string TxnRef { get; set; }

        private HashType secureHashType;

        public HashType SecureHashType
        {
            get { return secureHashType; }
            set { secureHashType = value; }
        }


        public VnpResultPay(string tmnCode, decimal amount, string bankCode, string bankTranNo, CardType cardType, DateTime payDate, string orderInfo, long transactionNo, ResponseCode responseCode, TransactionStatusCode transactionStatus, string txnRef, HashType secureHashType)
        {
            TmnCode = tmnCode;
            Amount = amount;
            BankCode = bankCode;
            BankTranNo = bankTranNo;
            CardType = cardType;
            PayDate = payDate;
            OrderInfo = orderInfo;
            TransactionNo = transactionNo;
            ResponseCode = responseCode;
            TransactionStatus = transactionStatus;
            TxnRef = txnRef;
            SecureHashType = secureHashType;
        }

        public VnpResultPay(string query, string keyCheckSum)
        {
            SecureHashType = HashType.HmacSHA512;

            if (query.Contains('?'))
            {
                query = query.Substring(query.IndexOf('?') + 1);
            }

            string[] parameter = query.Split('&');

            SortedList<string, string> dataInput = new SortedList<string, string>(new VnPayCompare());

            string hashChecksum = String.Empty;

            foreach (string param in parameter)
            {
                if (param.StartsWith("vnp_"))
                {
                    if (param.StartsWith("vnp_SecureHash"))
                    {
                        hashChecksum = param.Split('=')[1];
                    }
                    else if (!param.StartsWith("vnp_SecureHashType"))
                    {
                        Enum.TryParse(param.Split('=')[1], true, out secureHashType);
                    }
                    else
                    {
                        string[] keyValue = param.Split('=');

                        dataInput.Add(WebUtility.UrlDecode(keyValue[0]), WebUtility.UrlDecode(keyValue[1]));
                    }
                }
            }

            string checkSum = Functions.HmacSHA512(keyCheckSum, Functions.CovertToUrlParameter(dataInput));

            if (checkSum == hashChecksum)
            {
                try
                {
                    TmnCode = dataInput["vnp_TmnCode"];
                    Amount = decimal.Parse(dataInput["vnp_Amount"]) / 100;
                    BankCode = dataInput["vnp_BankCode"];
                    BankTranNo = dataInput["vnp_BankTranNo"];
                    CardType = (CardType)Enum.Parse(typeof(CardType), dataInput["vnp_CardType"], true);
                    PayDate = DateTime.ParseExact(dataInput["vnp_PayDate"], "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                    OrderInfo = dataInput["vnp_OrderInfo"];
                    TransactionNo = long.Parse(dataInput["vnp_TransactionNo"]);
                    ResponseCode = (ResponseCode)int.Parse(dataInput["vnp_ResponseCode"]);
                    TransactionStatus = (TransactionStatusCode)int.Parse(dataInput["vnp_ResponseCode"]);
                    TxnRef = dataInput["vnp_TxnRef"];
                }
                catch { }
            }
            else
            {
                throw new Exception("Invalid Signal");
            }
        }

        public VnpResultPay() { }
    }
}

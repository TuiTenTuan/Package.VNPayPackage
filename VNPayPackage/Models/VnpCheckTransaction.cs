using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using VNPayPackage.Enums;
using VNPayPackage.Extensions;
using VNPayPackage.Ulits;

namespace VNPayPackage.Models
{
    public class VnpCheckTransaction
    {
        public string ID { get; set; }

        public string Version { get; set; }

        public VNPCommand Command { get; set; }

        public string TmnCode { get; set; }

        public string TxnRef { get; set; }

        public string OrderInfo { get; set; }

        public long TransactionNo { get; set; }

        public DateTime TransactionDate { get; set; }

        public DateTime CreateDate { get; set; }

        public IPAddress IpServer { get; set; }

        public string SecureHash { get; set; }

        public VnpCheckTransaction(string iD, string version, VNPCommand command, string tmnCode, string txnRef, string orderInfo, long transactionNo, DateTime transactionDate, DateTime createDate, IPAddress ipServer)
        {
            ID = iD;
            Version = version;
            Command = command;
            TmnCode = tmnCode;
            TxnRef = txnRef;
            OrderInfo = orderInfo;
            TransactionNo = transactionNo;
            TransactionDate = transactionDate;
            CreateDate = createDate;
            IpServer = ipServer;
        }

        public VnpCheckTransaction(string tmnCode, string txnRef, string orderInfo, long transactionNo, DateTime transactionDate, IPAddress ipServer) : this(DateTime.Now.Ticks.ToString(), "2.1.0", VNPCommand.CheckPayment, tmnCode, txnRef, orderInfo, transactionNo, transactionDate, DateTime.Now, ipServer) { }

        public SortedList<string, string> ConvertToSortedList()
        {
            SortedList<string, string> resut = new SortedList<string, string>();

            resut.Add("vnp_RequestId", ID);
            resut.Add("vnp_Version", Version);
            resut.Add("vnp_Command", Command.GetValue());
            resut.Add("vnp_TmnCode", TmnCode);
            resut.Add("vnp_TxnRef", TxnRef);
            resut.Add("vnp_TransactionNo", TransactionNo.ToString());
            resut.Add("vnp_TransactionDate", TransactionDate.ToString("yyyyMMddHHmmss"));
            resut.Add("vnp_IpAddr", IpServer.MapToIPv4().ToString());

            return resut;
        }

        public string CheckSum(string key)
        {
            string dataCheckSum = $@"{WebUtility.UrlEncode(ID)}|{WebUtility.UrlEncode(Version)}|{WebUtility.UrlEncode(Command.GetValue())}|{WebUtility.UrlEncode(TmnCode)}|{WebUtility.UrlEncode(TxnRef)}|{WebUtility.UrlEncode(TransactionDate.ToString("yyyyMMddHHmmss"))}|{WebUtility.UrlEncode(CreateDate.ToString("yyyyMMddHHmmss"))}|{WebUtility.UrlEncode(IpServer.MapToIPv4().ToString())}|{WebUtility.UrlEncode(OrderInfo)}";

            return Functions.HmacSHA512(key, dataCheckSum);
        }

        public string ConvertToUrlParameter()
        {
            return Functions.CovertToUrlParameter(ConvertToSortedList());
        }

        public string CreateUrl(string baseUrl, string keyCheckSum)
        {
            StringBuilder result = new StringBuilder();

            string parameter = ConvertToUrlParameter();

            SecureHash = CheckSum(keyCheckSum);

            result.Append("?").Append(parameter).Append("&vnp_SecureHash=").Append(SecureHash);

            return result.ToString();
        }
    }
}

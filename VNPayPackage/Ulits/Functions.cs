using Microsoft.AspNetCore.Http;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace VNPayPackage.Ulits
{
    public static class Functions
    {
        public static IPAddress GetIP(this HttpRequest request)
        {
            IPAddress result = new IPAddress(new byte[] { 127, 0, 0, 1 });

            try
            {
                var tempIp = request.HttpContext.Connection.RemoteIpAddress;

                if (tempIp.IsIPv4MappedToIPv6)
                {
                    tempIp = tempIp.MapToIPv4();
                }

                result = tempIp;
            }
            catch { }

            return result;
        }

        public static string HmacSHA512(string key, string inputData)
        {
            var hash = new StringBuilder();

            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);

            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }

        public static string SHA256(string inputData)
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(inputData));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }

        public static string[] SplitVietNamName(string name)
        {
            string[] result =  new string[2];

            int indexLastSpace = name.LastIndexOf(' ');

            if (indexLastSpace == -1)
            {
                result[0] = "";
                result[1] = name;
            }
            else
            {
                result[0] = name.Substring(0, indexLastSpace).Trim();
                result[0] = name.Substring(indexLastSpace).Trim();
            }

            return result;
        }
    }
}

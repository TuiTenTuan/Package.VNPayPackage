using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNPayPackage.Enums;

namespace VNPayPackage.Extensions
{
    public static class HashTypeExtension
    {
        public static string GetValue(this HashType hash)
        {
            switch (hash)
            {
                case HashType.SHA256:
                    return "SHA256";
                case HashType.HmacSHA512:
                    return "HmacSHA256";

                default: 
                    return "HmacSHA512";
            }
        }
    }
}

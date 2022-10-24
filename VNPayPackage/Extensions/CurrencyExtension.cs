using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNPayPackage.Enums;

namespace VNPayPackage.Extensions
{
    public static class CurrencyExtension
    {
        public static string GetValue(this Currency currency)
        {
            switch (currency)
            {
                case Currency.VND:
                    return "VND";
                default:
                    return "VND";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNPayPackage.Enums;

namespace VNPayPackage.Extensions
{
    public static class TaxTypeExtension
    {
        public static string GetValue(this TaxType taxType)
        {
            switch (taxType)
            {
                case TaxType.Persion:
                    return "I";

                case TaxType.Organize:
                default:
                    return "O";
            }
        }
    }
}

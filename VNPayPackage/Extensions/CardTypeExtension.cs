using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNPayPackage.Enums;

namespace VNPayPackage.Extensions
{
    public static class CardTypeExtension
    {
        public static string GetValue(this CardType cardType)
        {
            switch (cardType)
            {
                case CardType.ATM:
                    return "ATM";
                case CardType.QRCODE:
                    return "QRCODE";
                default:
                    return "ATM";
            }
        }
    }
}

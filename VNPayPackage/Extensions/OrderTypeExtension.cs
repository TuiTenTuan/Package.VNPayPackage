using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNPayPackage.Enums;

namespace VNPayPackage.Extensions
{
    public static class OrderTypeExtension
    {
        public static string GetValue(this OrderType order)
        {
            switch (order)
            {
                case OrderType.TopUp:
                    return "topup";
                case OrderType.BillPayment:
                    return "billpayment";
                case OrderType.Fashion:
                    return "fashion";
                case OrderType.Other:
                default:
                    return "other";
            }
        }
    }
}

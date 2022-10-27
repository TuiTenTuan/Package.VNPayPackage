using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNPayPackage.Enums;

namespace VNPayPackage.Extensions
{
    public static class TransactionStatusExtension
    {
        public static string GetValue(this TransactionStatusCode transactionStatus)
        {
            switch(transactionStatus)
            {
                default:
                    return "Success";
            }
        }
    }
}

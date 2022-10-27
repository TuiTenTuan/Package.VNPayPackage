using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNPayPackage.Enums
{
    public enum TransactionStatusCode
    {
        Successfull = 0,
        NotComplete = 1,
        TransactionError = 2,
        IslandTransaction = 4,
        ProcessingTransaction = 5,
        RequestRefundSentToBank = 6,
        SuspectedOfFraud = 7,
        Refused = 9
    }
}

namespace VNPayPackage.Enums
{
    public enum ResponseCode
    {
        Success = 0,
        SucessfullSubtraction = 7,
        FailedNotRegistered = 9,
        FailedVerifyNotCorrect = 10,
        FailedTimeOut = 11,
        FailedLock = 12,
        FailedOTP = 13,
        FailedCancel = 24,
        FailedNotEnoughBalance = 51,
        FailedDaylyTransactionLimit = 65,
        BankUnderMaintenance = 75,
        FailedWrongPassword = 79,
        OtherError = 99
    }
}

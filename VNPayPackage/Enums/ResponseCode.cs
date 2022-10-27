namespace VNPayPackage.Enums
{
    public enum ResponseCode
    {
        Success = 0,
        IncorrectTmnCode = 2,
        IncorrectData = 3,
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
        OtherError = 99,
        InvalidSignal = 97,
        TransactionFailed  = 95,
        DuplicateRequest = 94,
        NotFound = 91
    }
}

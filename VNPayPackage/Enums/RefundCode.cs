namespace VNPayPackage.Enums
{
    public enum RefundCode
    {
        TotalAmountGreaterPrincipalAmount = 2,
        DataNotCorrect = 3,
        FullRefundNotAllowed = 4,
        OnlyPartalRefunds = 13,
        NotFound = 91,
        InvalidRefundAmount = 93,
        DuplicateRequest = 94,
        FailedVNPayRefuses = 95,
        InvalidSignature = 97,
        Timeout = 97,
        OtherError = 99
    }
}

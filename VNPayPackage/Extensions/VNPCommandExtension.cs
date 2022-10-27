using VNPayPackage.Enums;

namespace VNPayPackage.Extensions
{
    public static class VNPCommandExtension
    {
        public static string GetValue(this VNPCommand command)
        {
            switch (command)
            {
                case VNPCommand.Pay:
                    return "pay";
                case VNPCommand.Refund:
                    return "refund";
                case VNPCommand.CheckPayment:
                    return "querydr";
                default:
                    return "pay";
            }
        }
    }
}

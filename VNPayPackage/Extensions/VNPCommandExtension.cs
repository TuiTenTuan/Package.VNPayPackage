using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

                default:
                    return "pay";
            }
        }
    }
}

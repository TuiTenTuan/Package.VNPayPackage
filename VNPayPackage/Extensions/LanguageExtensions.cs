using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNPayPackage.Enums;

namespace VNPayPackage.Extensions
{
    public static class LanguageExtensions
    {
        public static string GetValue(this Language language)
        {
            switch (language)
            {
                case Language.Vietnamese:
                    return "vn";
                case Language.English:
                    return "en";
                default:
                    return "vn";
            }
        }
    }
}

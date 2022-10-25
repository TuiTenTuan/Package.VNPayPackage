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

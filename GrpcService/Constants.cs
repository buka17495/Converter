using System.Text.RegularExpressions;

namespace GrpcService
{
    public static partial class Constants
    {
        [GeneratedRegex("^(\\d{1,9})(,\\d{1,2})?$")]
        public static partial Regex CurrencyRegex();

        public const string Dollar = "dollar";
        public const string Dollars = "dollars";
        public const string Cent = "cent";
        public const string Cents = "cents";
        public const string Hundred = "hundred";
        public const string Thousand = "thousand";
        public const string Million = "million";
        public const string And = "and";

        public static readonly Dictionary<int, string> SimpleNumbers = new() {
            { 0, "zero" },
            { 1, "one" },
            { 2, "two" },
            { 3, "three" },
            { 4, "four" },
            { 5, "five" },
            { 6, "six" },
            { 7, "seven" },
            { 8, "eight" },
            { 9, "nine" },
        };

        public static readonly Dictionary<int, string> Dozens = new() {
            { 2, "twenty" },
            { 3, "thirty" },
            { 4, "forty" },
            { 5, "fifty" },
            { 6, "sixty" },
            { 7, "seventy" },
            { 8, "eighty" },
            { 9, "ninety" },
        };

        public static readonly Dictionary<int, string> SecondDozens = new() {
            { 10, "ten" },
            { 11, "eleven" },
            { 12, "twelve" },
            { 13, "thirteen" },
            { 14, "fourteen" },
            { 15, "fifteen" },
            { 16, "sixteen" },
            { 17, "seventeen" },
            { 18, "eighteen" },
            { 19, "nineteen" },
        };
    }
}

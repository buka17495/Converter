using System.Text.RegularExpressions;

namespace Converter
{
    public static partial class Constants
    {
        [GeneratedRegex("^(\\d{1,9})(,\\d{1,2})?$")]
        public static partial Regex CurrencyRegex();
    }
}

using System.Text;

namespace GrpcService
{
    public static class CurrencyHelper
    {
        private const int million = 1000000;
        private const int thousand = 1000;

        public static string Convert(string currency)
        {
            var split = currency.Split(',');
            var errorMessage = $"{currency} should be in format 111111111,11";
            if (!int.TryParse(split[0], out var whole))
                throw new ArgumentException(errorMessage);

            var millionSb = HandleThreeDigits(whole, million, Constants.Million);
            whole %= million;

            var thousandSb = HandleThreeDigits(whole, thousand, Constants.Thousand);
            whole %= thousand;

            var hundredSb = new StringBuilder();
            if (millionSb.Length == 0 && thousandSb.Length == 0 || whole > 0)
                hundredSb = Parse(whole);
            var result = millionSb.Append(thousandSb).Append(hundredSb);

            if (split.Length > 1)
            {
                if (!int.TryParse(split[1], out var fractional))
                    throw new ArgumentException(errorMessage);
                if (split[1].Length < 2)
                    fractional *= 10;
                var fractionalSb = Parse(fractional);
                if (fractionalSb.Length > 0)
                {
                    result.Append(result.ToString() == "one " ? Constants.Dollar : Constants.Dollars);
                    result.Append(' ').Append(Constants.And).Append(' ').Append(fractionalSb);
                    result.Append(fractionalSb.ToString() == "one " ? Constants.Cent : Constants.Cents);
                    return result.ToString();
                }
            }

            if (result.Length <= 0)
                return "";
            result.Append(result.ToString() == "one " ? Constants.Dollar : Constants.Dollars);
            return result.ToString();
        }

        private static StringBuilder HandleThreeDigits(int whole, int number, string text)
        {
            var sb = new StringBuilder();
            if (whole < number)
                return sb;

            sb = Parse(whole / number);
            if (sb.Length > 0)
                sb.Append(text).Append(' ');

            return sb;
        }

        public static bool Validate(string currency)
        {
            return Constants.CurrencyRegex().IsMatch(currency);
        }

        private static StringBuilder Parse(int whole)
        {
            var sb = new StringBuilder();
            var hundreds = whole / 100;
            if (hundreds > 0)
                sb.Append(Constants.SimpleNumbers[hundreds]).Append(' ').Append(Constants.Hundred).Append(' ');
            whole %= 100;
            switch (whole)
            {
                case >= 10 and < 20:
                    sb.Append(Constants.SecondDozens[whole]).Append(' ');
                    return sb;
                case < 10 and 0:
                {
                    if (sb.Length == 0)
                        sb.Append(Constants.SimpleNumbers[whole]).Append(' ');
                    return sb;
                }
                case < 10:
                    sb.Append(Constants.SimpleNumbers[whole]).Append(' ');
                    return sb;
                default:
                {
                    var dozens = whole / 10;
                    if (whole % 10 == 0)
                    {
                        sb.Append(Constants.Dozens[dozens]).Append(' ');
                        return sb;
                    }
                    sb.Append(Constants.Dozens[dozens]).Append('-');
                    sb.Append(Constants.SimpleNumbers[whole % 10]).Append(' ');
                    break;
                }
            }

            return sb;
        }
    }
}

using GrpcService;

namespace ConverterTests
{
    public class CurrencyHelperTests
    {
        [TestCase("0", "zero dollars")]
        [TestCase("1", "one dollar")]
        [TestCase("25,1", "twenty-five dollars and ten cents")]
        [TestCase("0,01", "zero dollars and one cent")]
        [TestCase("45100", "forty-five thousand one hundred dollars")]
        [TestCase("999999999,99", "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents")]
        [TestCase("0,00", "zero dollars and zero cents")]
        [TestCase("0,0", "zero dollars and zero cents")]
        [TestCase("0,10", "zero dollars and ten cents")]
        [TestCase("11,12", "eleven dollars and twelve cents")]
        [TestCase("1,01", "one dollar and one cent")]
        [TestCase("1000", "one thousand dollars")]
        [TestCase("1000000", "one million dollars")]
        public void Convert_Valid(string input, string expected)
        {
            var actual = CurrencyHelper.Convert(input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase("0", true)]
        [TestCase("1", true)]
        [TestCase("25,1", true)]
        [TestCase("0,01", true)]
        [TestCase("45100", true)]
        [TestCase("999999999,99", true)]
        [TestCase("0,00", true)]
        [TestCase("0,0", true)]
        [TestCase("0,10", true)]
        [TestCase("11,12", true)]
        [TestCase(".0", false)]
        [TestCase("0.000", false)]
        [TestCase("9999999999,99", false)]
        public void Validate_Valid(string input, bool expected)
        {
            var actual = CurrencyHelper.Validate(input);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
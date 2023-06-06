using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Converter
{
    public partial class MainWindow
    {
        private const string specialSymbol = "@";
        private bool IsInputAvailable = true;

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is not TextBox tb)
                return;

            var oldIndex = tb.CaretIndex;

            var textWithoutSpaces = tb.Text.Replace(" ", string.Empty);

            var validationResult = Validate(textWithoutSpaces);
            if (validationResult.IsValid)
                HandleValid(tb, oldIndex, textWithoutSpaces);
            else
                HandleNotValid(tb, textWithoutSpaces, validationResult);

            ConvertButton.IsEnabled = validationResult.IsValid;
        }

        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsInputAvailable)
                return;

            e.Handled = e.Key != Key.Back;
        }

        private static ValidationResult Validate(string text)
        {
            var regex = Constants.CurrencyRegex();

            try
            {
                return regex.IsMatch(text)
                    ? ValidationResult.ValidResult
                    : new ValidationResult(false, $"Please enter a currency in the format: 111 111 111,11");
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }
        }

        private static string AddSpaceBetweenEveryThreeCharacters(string input)
        {
            var result = string.Empty;
            var count = 0;

            if (string.IsNullOrEmpty(input))
                return result;

            var parts = input.Split(',');
            var wholeNumberPart = parts[0];
            var fractionalPart = parts.Length > 1
                ? "," + parts[1]
                : "";

            for (var i = wholeNumberPart.Length - 1; i >= 0; i--)
            {
                result = wholeNumberPart[i] + result;
                count++;

                if (count % 3 == 0 && i != 0)
                    result = " " + result;
            }

            return result + fractionalPart;
        }

        private void HandleValid(TextBox tb, int oldIndex, string textWithoutSpaces)
        {
            tb.ToolTip = null;
            tb.ClearValue(BackgroundProperty);
            ConvertButton.ToolTip = null;

            var formattedText = AddSpaceBetweenEveryThreeCharacters(textWithoutSpaces);

            if (tb.Text != formattedText)
                ChangeText(tb, oldIndex, textWithoutSpaces, formattedText);
            IsInputAvailable = true;
        }

        private void ChangeText(TextBox tb, int oldIndex, string textWithoutSpaces, string formattedText)
        {
            var currentText = tb.Text;

            tb.TextChanged -= TextBox_TextChanged;
            tb.Text = formattedText;

            if (textWithoutSpaces == currentText && textWithoutSpaces.Length == oldIndex)
                tb.CaretIndex = oldIndex + 1;
            else if (oldIndex > formattedText.Length - 1)
                tb.CaretIndex = oldIndex + (formattedText.Length - textWithoutSpaces.Length);
            else if (oldIndex > 0)
            {
                var specialText = currentText[..oldIndex] + specialSymbol;
                if (currentText.Length > oldIndex)
                    specialText += currentText[oldIndex..];
                var currentTextWithoutSpaces = specialText.Replace(" ", string.Empty);
                var formattedSpecialText = AddSpaceBetweenEveryThreeCharacters(currentTextWithoutSpaces);
                tb.CaretIndex = formattedSpecialText.IndexOf(specialSymbol);
            }

            tb.TextChanged += TextBox_TextChanged;
        }

        private void HandleNotValid(TextBox tb, string textWithoutSpaces, ValidationResult validationResult)
        {
            if (string.IsNullOrEmpty(textWithoutSpaces) || textWithoutSpaces.Last() == ',')
            {
                ConvertButton.ToolTip = validationResult.ErrorContent;
                IsInputAvailable = true;
            }
            else
            {
                tb.ToolTip = validationResult.ErrorContent;
                tb.Background = Brushes.Red;
                IsInputAvailable = false;
            }
        }
    }
}

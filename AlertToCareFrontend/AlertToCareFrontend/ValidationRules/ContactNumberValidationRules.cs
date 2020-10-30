using System.Globalization;
using System.Windows.Controls;

namespace AlertToCareFrontend.ValidationRules
{
    class ContactNumberValidationRules : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string stringValue = value as string;
            if(string.IsNullOrEmpty(stringValue))
            {
                return new ValidationResult(false, " * Required");
            }
            return IsEightDigitNumber(stringValue);
        }

        private ValidationResult IsEightDigitNumber(string value)
        {
            if (!int.TryParse(value, out _))
            {
                return new ValidationResult(false, "Contact number must have only digits!");
            }
            if (value.Length!=8)
            {
                return new ValidationResult(false, "Contact number must be of 8-digits!");
            }
            return new ValidationResult(true, "");
        }
    }
}

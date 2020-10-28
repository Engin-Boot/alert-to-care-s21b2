using System.Globalization;
using System.Windows.Controls;

namespace AlertToCareFrontend.ValidationRules
{
    class TotalNoOfBedsValidationRules : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string stringValue = value as string;
            if (int.TryParse(stringValue, out int totalNoOfBeds))
            {
                return totalNoOfBeds < 2 ? new ValidationResult(false, "Number of beds cannot be less than 2!") : new ValidationResult(true, "");
            }
            else
            {
                return new ValidationResult(false, "Number of beds must be a valid number!");
            }
        }
    }
}

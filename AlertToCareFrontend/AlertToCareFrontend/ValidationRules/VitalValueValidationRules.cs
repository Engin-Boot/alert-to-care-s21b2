using System.Globalization;
using System.Windows.Controls;

namespace AlertToCareFrontend.ValidationRules
{
    class VitalValueValidationRules : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string stringValue = value as string;
            
            if (string.IsNullOrEmpty(stringValue) || float.TryParse(stringValue, out _))
            {
                return new ValidationResult(true, "");
            }
            else
            {
                return new ValidationResult(false, "Vital value must be a number!");
            }
        }
    }
}

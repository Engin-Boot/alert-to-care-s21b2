using System.Globalization;
using System.Windows.Controls;

namespace AlertToCareFrontend.ValidationRules
{
    class IdValidationRules : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string id = value as string;
            return string.IsNullOrEmpty(id) ? new ValidationResult(false, "Id is required!") : new ValidationResult(true, "");
        }
    }
}

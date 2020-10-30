using System.Globalization;
using System.Windows.Controls;

namespace AlertToCareFrontend.ValidationRules
{
    class TotalNoOfBedsValidationRules : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string stringValue = value as string;
            
            if(string.IsNullOrEmpty(stringValue))
            {
                return new ValidationResult(false, " * Required");
            }

            return CheckIfAtleastOneBedIsSelected(stringValue);
        }

        private ValidationResult CheckIfAtleastOneBedIsSelected(string value)
        {
            if (int.TryParse(value, out int totalNoOfBeds))
            {
                return totalNoOfBeds < 1 ? new ValidationResult(false, "Number of beds cannot be less than 1!") : new ValidationResult(true, "");
            }
            else
            {
                return new ValidationResult(false, "Number of beds must be a valid number!");
            }
        }
    }
}

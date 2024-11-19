using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Controls;

namespace WpfApp1
{
    public class NumericValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (int.TryParse((string)value, out int result))
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "Будь ласка, введіть коректне число.");
        }
    }
}


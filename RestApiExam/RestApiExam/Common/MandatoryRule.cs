using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RestApiExam.Common
{
    public class MandatoryRule : ValidationRule
    {
        public string Name { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty((string)value))
            {
                return new ValidationResult(false, $"{Name} is mandatory");
            }

            return new ValidationResult(true, null);
        }
    }

}

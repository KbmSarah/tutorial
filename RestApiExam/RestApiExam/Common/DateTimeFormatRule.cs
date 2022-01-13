using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RestApiExam.Common
{
    class DateTimeFormatRule : ValidationRule
    {
        public string Name { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime res;

            if (!DateTime.TryParseExact((string)value, "yyyyMMddHHmmss", null, DateTimeStyles.AdjustToUniversal, out res))
            {
                return new ValidationResult(false, $"{Name} is not in a correct Date format.");
            }

            return new ValidationResult(true, null);
        }
    }
}

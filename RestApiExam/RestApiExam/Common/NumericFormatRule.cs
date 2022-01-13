using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RestApiExam.Common
{
    class NumericFormatRule : ValidationRule
    {
        public string Name { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int res;
            if(!int.TryParse((string)value, out res))
                return new ValidationResult(false, $"{Name} is not in a correct numeric format.");

            return new ValidationResult(true, null);
        }
    }
}

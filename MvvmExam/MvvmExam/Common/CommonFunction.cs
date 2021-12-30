using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MvvmExam.Common
{
    public class MustFillRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string text = value as string;
            int res;

            if(text == string.Empty)
            {
                return new ValidationResult(false, "Empty! please, fill the blank");
            }
            if(int.TryParse(text,out res))
            {
                return new ValidationResult(false, "Invalid Format!");
            }
            return ValidationResult.ValidResult;
        }
    }
}

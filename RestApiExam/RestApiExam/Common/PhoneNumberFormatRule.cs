using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RestApiExam.Common
{
    class PhoneNumberFormatRule : ValidationRule
    {
        public string Name { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string phoneNumber = value as string;

            // 핸드폰 번호 정규식
            Regex regex = new Regex(@"01[016789][0-9]{7,8}");

            Match match = regex.Match(phoneNumber);
            if (!match.Success)
            {
                return new ValidationResult(false, $"{Name} is not in a correct Vehicle registration plates format.");
            }

            return new ValidationResult(true, null);
        }
    }
}

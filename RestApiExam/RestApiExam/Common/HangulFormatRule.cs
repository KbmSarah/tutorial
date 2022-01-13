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
    class HangulFormatRule : ValidationRule
    {
        public string Name { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string userName = value as string;
            
            // 한글 정규식
            Regex regex = new Regex(@"[가-힣]+$");

            Match match = regex.Match(userName);
            if (!match.Success)
            {
                return new ValidationResult(false, $"{Name} is not in a correct Hangul format.");
            }

            return new ValidationResult(true, null);
        }
    }
}

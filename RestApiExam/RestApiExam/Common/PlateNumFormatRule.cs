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
    class PlateNumFormatRule : ValidationRule
    {
        public string Name { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string plateNum = value as string;

            // 차량번호는 7자리
            if(plateNum.Length != 7)
            {
                return new ValidationResult(false, $"{Name} is not in a correct Vehicle registration plates format.");
            }

            // 차량번호 정규식
            Regex regex = new Regex(@"[0-9]{2}[가-힣]{1}[0-9]{4}");

            Match match = regex.Match(plateNum);
            if (!match.Success)
            {
                return new ValidationResult(false, $"{Name} is not in a correct Vehicle registration plates format.");
            }

            return new ValidationResult(true, null);
        }
    }
}

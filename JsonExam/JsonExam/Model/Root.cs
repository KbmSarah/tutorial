using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonExam.Model
{
    public class Root
    {
        public string result_code { get; set; }
        public string result_msg { get; set; }
        public List<InOutModel> list { get; set; }
    }
}

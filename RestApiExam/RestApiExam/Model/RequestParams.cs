using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiExam.Model
{
    public class RequestParams
    {
        public string park_id { get; set; }
        public int page { get; set; }
        public string plate_num { get; set; }
        public object dt_time { get; set; }
        public object dt_to { get; set; }
    }
}

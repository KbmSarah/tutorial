using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonExam.Model
{
    public class InOutModel
    {
        public string entry_id { get; set; }
        public string plate_num { get; set; }
        public string ticket_type { get; set; }
        public string pay_stat { get; set; }
        public string in_time { get; set; }
        public string out_time { get; set; }
        public object pay_time { get; set; }
        public string in_image_path { get; set; }
        public string out_image_path { get; set; }
        public string in_lpr_name { get; set; }
        public string out_lpr_name { get; set; }
        public string pay_station_name { get; set; }
    }
}

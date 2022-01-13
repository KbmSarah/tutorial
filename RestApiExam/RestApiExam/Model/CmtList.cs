using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiExam.Model
{
    public class CmtList
    {
        public bool IsSelected { get; set; } = false;
        public string cmt_id { get; set; }
        public string cmt_code { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string plate_num { get; set; }
        public string user_name { get; set; }
        public string phone { get; set; }
        public int? fee { get; set; }
        public string memo { get; set; }
    }

    public class CmtNotify :ViewModelBase, IDisposable
    {
        private bool IsDisposed;
        public CmtNotify()
        {

        }
        public CmtNotify(CmtList list)
        {
            this.IsSelected = list.IsSelected;
            cmt_id = list.cmt_id;
            cmt_code = list.cmt_code;
            start_date = list.start_date;
            end_date = list.end_date;
            plate_num = list.plate_num;
            user_name = list.user_name;
            phone = list.phone;
            fee = list.fee;
            memo = list.memo;
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set { base.SetValue(ref _isSelected, value); }
        }


        public string cmt_id { get; set; }
        public string cmt_code { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string plate_num { get; set; }
        public string user_name { get; set; }
        public string phone { get; set; }
        public int? fee { get; set; }
        public string memo { get; set; }

        public void Dispose()
        {
            if(!IsDisposed)
            {

            }
        }
    }
}

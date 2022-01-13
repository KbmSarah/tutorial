using System;
using DevExpress.Mvvm;
using RestApiExam.Model;
using System.Windows.Controls;
using System.Globalization;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.Text;

namespace RestApiExam.ViewModels
{
    public class RegistratorViewModel : ViewModelBase, IDisposable
    {
        #region Members
        Action<string> RestRequest = null;
        private string _cmtId = "1111";
        private string _cmtCode;
        private string _startDate;
        private string _endDate;
        private string _plateNum;
        private string _userName;
        private string _phone;
        private int? _fee;
        private string _memo;
        private ICommand _cmdRestRequest;
        private bool IsDisposed;
        public delegate void EventHandler(string param);
        public event EventHandler ButtonClickedEvent;
        #endregion

        #region Properties
        public string CmtId
        {
            get { return _cmtId; }
            set { base.SetValue(ref _cmtId , value); }
        }
        public string CmtCode
        {
            get { return _cmtCode; }
            set { base.SetValue(ref _cmtCode, value); }
        }
        public string StartDate
        {
            get { return _startDate; }
            set { base.SetValue(ref _startDate , value); }
        }
        public string EndDate
        {
            get { return _endDate; }
            set { base.SetValue(ref _endDate ,value); }
        }
        public string PlateNum
        {
            get { return _plateNum; }
            set { base.SetValue(ref _plateNum, value); }
        }
        public string UserName
        {
            get { return _userName; }
            set { base.SetValue(ref _userName ,value); }
        }
        public string Phone
        {
            get { return _phone; }
            set { base.SetValue(ref _phone, value); }
        }
        public int? Fee
        {
            get { return _fee; }
            set { base.SetValue(ref _fee ,value); }
        }
        public string Memo
        {
            get { return _memo; }
            set { base.SetValue(ref _memo, value); }
        }
        public ICommand CmdRestRequest
        {
            get
            {
                if (_cmdRestRequest == null)
                    _cmdRestRequest = new DelegateCommand<string>(ExecCmdRestRequest, CanExecCmdRestRequest);

                return _cmdRestRequest;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Set textbox display with SelectedItem from parent view.
        /// </summary>
        /// <param name="SelectedItem">Selected item from DataGrid(parent view)</param>
        public void SetList(CmtNotify SelectedItem)
        {
            if (SelectedItem != null)
            {
                CmtId = SelectedItem.cmt_id;
                CmtCode = SelectedItem.cmt_code;
                StartDate = SelectedItem.start_date;
                EndDate = SelectedItem.end_date;
                PlateNum = SelectedItem.plate_num;
                UserName = SelectedItem.user_name;
                Phone = SelectedItem.phone;
                Fee = SelectedItem.fee;
                Memo = SelectedItem.memo;
            }
        }

        /// <summary>
        /// Get textbox display & return
        /// </summary>
        /// <returns>Modified Item</returns>
        public CmtNotify GetList()
        {
            CmtNotify NewOrModifiedItem = new CmtNotify()
            {
                cmt_id = CmtId,
                cmt_code = CmtCode,
                start_date = StartDate,
                end_date = EndDate,
                plate_num = PlateNum,
                user_name = UserName,
                phone = Phone,
                fee = Fee,
                memo = Memo
            };

            return NewOrModifiedItem;
        }

        /// <summary>
        /// Notify to subscriber that event occured when button clicked.
        /// </summary>
        /// <param name="param">dml(update,insert,delete)</param>
        private void ExecCmdRestRequest(string param)
        {
            ButtonClickedEvent(param);
        }

        /// <summary>
        /// If one of textboxes is null or empty then disable button.
        /// Because when Application started or clear button clicked, it doesn't execute Validation check.
        /// </summary>
        /// <param name="param">it's no use</param>
        /// <returns></returns>
        private bool CanExecCmdRestRequest(string param)
        {
            return !string.IsNullOrEmpty(CmtId)
                && !string.IsNullOrEmpty(CmtCode)
                && !string.IsNullOrEmpty(StartDate)
                && !string.IsNullOrEmpty(EndDate)
                && !string.IsNullOrEmpty(PlateNum)
                && !string.IsNullOrEmpty(UserName)
                && !string.IsNullOrEmpty(Phone)
                && Fee.HasValue;
        }

        public void Dispose()
        {
            if(!IsDisposed)
            {
                this.IsDisposed = true;
                ButtonClickedEvent = null;
            }
        }
        #endregion

    }
}
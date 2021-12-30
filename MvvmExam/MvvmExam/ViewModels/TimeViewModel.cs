using System;
using DevExpress.Mvvm;
using MvvmExam.Common;
using System.Windows.Input;
using DevExpress.Xpf.Editors;

namespace MvvmExam.ViewModels
{
    public class TimeViewModel : ViewModelBase
    {
        #region Construction
        public TimeViewModel()
        {
            MyDateTime = DateTime.Now;
            TimePicked = MyDateTime.ToString();
        }
        #endregion

        #region Members
        private DateTime _myDateTime;
        private string _timePicked;
        #endregion

        #region Properties
        public DateTime MyDateTime
        {
            get { return _myDateTime; }
            set { base.SetValue(ref _myDateTime, value); }
        }
        public string TimePicked
        {
            get { return _timePicked; }
            set { base.SetValue(ref _timePicked, value); }
        }
        #endregion

        #region Commands
        private ICommand _CmdDateTimeChanged;
        public ICommand CmdDateTimeChanged
        {
            get
            {
                if (_CmdDateTimeChanged == null)
                    _CmdDateTimeChanged = new DelegateCommand<TimePickerDateTimeChangedEventArgs>(ExecCmdDateTimeChanged);

                return _CmdDateTimeChanged;
            }
        }

        private void ExecCmdDateTimeChanged(TimePickerDateTimeChangedEventArgs args)
        {
            MyDateTime = args.NewValue;
            TimePicked = MyDateTime.ToString();
        }

        #endregion
    }
}